using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;
using System.IO;

namespace EpiLocal
{
    class Program
    {
        DiscordSocketClient Client;
        CommandService Command;

        static async Task Main(string[] args)
        {
            await new Program().MainAsync();

        }

        async Task MainAsync()
        {
            Client = new DiscordSocketClient(new DiscordSocketConfig() {
                LogLevel = Discord.LogSeverity.Verbose
            });
            Command = new CommandService();
            await Command.AddModuleAsync<KOMtuve>(null);
            Client.Log += Log;
            Command.Log += Log;
            Client.MessageReceived += Client_MessageReceived;
            await Client.LoginAsync(TokenType.Bot, File.ReadAllText("Token"));
            await Client.StartAsync();
            await Task.Delay(-1);
        }

        private async Task Client_MessageReceived(SocketMessage arg)
        {
            SocketUserMessage Message = arg as SocketUserMessage;
            if (Message == null || arg.Author.IsBot)
                return ;
            int pos = 0;
            if (Message.HasMentionPrefix(Client.CurrentUser, ref pos) || Message.HasStringPrefix("!", ref pos))
            {
                SocketCommandContext context = new SocketCommandContext(Client, Message);
                await Command.ExecuteAsync(context, pos, null);
            }
        }

        public static Task Log(LogMessage msg)
        {
            var cc = Console.ForegroundColor;
            switch (msg.Severity)
            {
                case LogSeverity.Critical:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case LogSeverity.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogSeverity.Warning:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case LogSeverity.Info:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case LogSeverity.Verbose:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case LogSeverity.Debug:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
            }
            Console.WriteLine(msg);
            Console.ForegroundColor = cc;
            return Task.CompletedTask;
        }
    }
}
