using Discord;
using Discord.Commands;
using rng_bot.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rng_bot.Modules
{
    [Group("list")]
    public class ListModules : ModuleBase<SocketCommandContext>
    {
        public CommandHandler CommandHandler { get; set; }

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

        [Command("create")]
        [Summary("")]
        public async Task Create(string listName="") 
        {
            List<string> listNames = new List<string>();

            if (listName == null || listNames.Contains(listName))
            {
                return;
            }
        }

        [Command("add")]
        [Summary("")]
        public async Task Add(string label="", int val=-1) 
        {
            if (string.IsNullOrEmpty(label)) return;
        }

        [Command("remove")]
        [Summary("")]
        public async Task RemoveLabel(string label="") 
        {
        }

        [Command("remove")]
        [Summary("")]
        public async Task RemoveIndex(int index=-1) 
        {
        }

        [Command("show")]
        [Summary("")]
        public async Task Show(string listName="") 
        {
        }

        [Command("move")]
        [Summary("")]
        public async Task Move(string labelName="", int newIndex=-1) 
        {
        }

        [Command("swap")]
        [Summary("")]
        public async Task Swap(int indexOrig=-1, int indexDest=-1)
        {
        }

        [Command("delete")]
        [Summary("")]
        public async Task Delete(int index=-1) 
        {
        }

        [Command("relabel")]
        [Summary("")]
        public async Task Relabel(int index=-1, string currentLabel="", string newLabel="")
        {
            if (string.IsNullOrEmpty(currentLabel) || currentLabel == newLabel)
            {
                return;
            }
        }
    }
}
