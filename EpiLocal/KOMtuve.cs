using Discord.Commands;
using System.Threading.Tasks;

namespace EpiLocal
{
    class KOMtuve : ModuleBase
    {
        [Command("local")]
        async Task local(params string[] args)
        {
            await ReplyAsync("le local est ouvert");
        }
    }
}
