using Discord.Commands;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace EpiLocal
{
    class KOMtuve : ModuleBase
    {
        Random rnd = new Random();

        [Command("local")]
        async Task local(params string[] args)
        {
            string PyProg = "D:\\HUB\\test.py";
            int dice = rnd.Next();

            Process proc = new Process();
            proc.StartInfo.FileName = "C:\\Python\\python.exe";
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.UseShellExecute = false;

            proc.StartInfo.Arguments = string.Concat(PyProg);
            proc.Start();

            StreamReader sReader = proc.StandardOutput;
            string output = sReader.ReadToEnd();
            Console.Write(output);

            if (output.Equals("0"))
                await ReplyAsync("le local est ouvert");
            else if (output.Equals("1"))
                await ReplyAsync("le local est fermé");
            else
                await ReplyAsync("dunno");
        }
    }
}
