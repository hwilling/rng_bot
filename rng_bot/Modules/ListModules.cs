using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using rng_bot.Controllers;
using rng_bot.Extensions;
using rng_bot.Services;
using rng_bot.Models;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace rng_bot.Modules
{
    [Group("list")]
    public class ListModules : ModuleBase<SocketCommandContext>
    {
        public CommandHandler commandHandler { get; set; }

        private readonly CommandService _service;

        public ListModules(CommandService service) => _service = service;

        [Command("help")]
        [Summary("If you don't know what this does you need help.")]
        public async Task Help()
        {
            EmbedBuilder embedBuilder = new EmbedBuilder();

            _service.Commands.ToList()
                .ForEach(x => embedBuilder.AddField(x.Name, x.Summary ?? "No description available\n"));

            await ReplyAsync("Here's a list of commands and their description: ", false, embedBuilder.Build());
        }

        public async Task Create(string listName="") 
        {
        }

        public async Task Add(string label="", int val=-1) 
        {
        }

        public async Task RemoveLabel(string label="") 
        {
        }

        public async Task RemoveIndex(int index=-1) 
        {
        }

        public async Task Show(string listName="") 
        {
        }

        public async Task Move(string labelName="", int newIndex=-1) 
        {
        }

        public async Task Swap(int indexOrig=-1, int indexDest=-1)
        {
        }

        public async Task Delete(int index=-1) 
        {
        }

        public async Task Relabel(int index=-1, string label="")
        {

        }
    }
}
