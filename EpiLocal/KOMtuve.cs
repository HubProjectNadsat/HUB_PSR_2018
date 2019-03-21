using Discord.Commands;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace EpiLocal
{
    class KOMtuve : ModuleBase
    {
        [Command("Help"), Alias("--help", "/help", "-h", "/h")]
        public async Task PrintHelp(params string[] args)
        {
            await ReplyAsync("Appel moi avec /l or l or local pour savoir si le local est ouvert" + Environment.NewLine + "`Ex: @Epilocal /l`");
        }

        [Command("local"), Alias("/l", "l")]
        async Task local(params string[] args)
        {
            string PyProg = "C:\\Users\\Benoit\\Desktop\\HUB_PSR_2018\\PSR_script\\local_detection2.py";

            Process proc = new Process();
            proc.StartInfo.FileName = "C:\\Python27\\python.exe";
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.UseShellExecute = false;

            proc.StartInfo.Arguments = string.Concat(PyProg);
            proc.Start();

            StreamReader sReader = proc.StandardOutput;
            string output = sReader.ReadToEnd().Trim('\n', '\r');
            Console.Write(output);

            if (output == "Le local est ouvert!")
                await ReplyAsync("le local est ouvert");
            else if (output == "Le local est ferme!")
                await ReplyAsync("le local est fermé");
            else if (output == "Les cameras sont inaccessible")
                await ReplyAsync("Les cameras sont inaccessible pour cause de problème technique");
            else
                await ReplyAsync("dunno");
        }

//        [Command("", RunMode = RunMode.Async), Priority(-1)]
//        public async Task DefaultMsg(params string[] args)
//        {
//            await PrintHelp(args);
//        }
    }
}
