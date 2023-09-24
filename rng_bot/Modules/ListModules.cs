using Discord;
using Discord.Commands;
using rng_bot.Controllers;
using rng_bot.Models;
using rng_bot.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rng_bot.Modules
{
    [Group("list")]
    public class ListModules : ModuleBase<SocketCommandContext>
    {

        private readonly CommandService _service;
        private JsonController _jsonController;
        public CommandHandler CommandHandler { get; set; }

        public ListModules(CommandService service)
        {
            _service = service;
            _jsonController = new JsonController();
        }

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
            if (listName == null)
            {
                return;
            }

            CommandHandler.CustomCollectionController.CreateCollection(listName);
            _jsonController.SaveData("CustomLists.json", CommandHandler.CustomCollectionController.RandomItemCollections);
        }

        [Command("add")]
        [Summary("")]
        public async Task Add(string label="", string listName="") 
        {
            if (string.IsNullOrEmpty(label))
            {
                return;
            }

            CommandHandler.CustomCollectionController.AddItemToList(listName, label);
            await ReplyAsync($"{label} has been added to {listName}");
            _jsonController.SaveData("CustomLists.json", CommandHandler.CustomCollectionController.RandomItemCollections);
        }

        [Command("delete-item")]
        [Summary("")]
        public async Task DeleteItem(string listName="", string itemName="")
        {
            if (string.IsNullOrEmpty(listName) || string.IsNullOrEmpty(itemName))
            {
                return;
            }

            CommandHandler.CustomCollectionController.DeleteItem(listName, itemName);
            _jsonController.SaveData("CustomLists.json", CommandHandler.CustomCollectionController.RandomItemCollections);
        }

        [Command("delete-list")]
        [Summary("")]
        public async Task DeleteList(string listName)
        {
            if (string.IsNullOrEmpty(listName))
            {
                return;
            }

            CommandHandler.CustomCollectionController.DeleteCollection(listName);
            _jsonController.SaveData("CustomLists.json", CommandHandler.CustomCollectionController.RandomItemCollections);
        }

        [Command("pick")]
        [Summary("")]
        public async Task Pick(string listName)
        {
            if (string.IsNullOrEmpty(listName))
            {
                return;
            }

            var selection = CommandHandler.CustomCollectionController.SelectRandomItem(listName, new RandomGen(CommandHandler.Rand));
            await ReplyAsync($"{selection.FormatResult()}");
        }

        [Command("show-all")]
        [Summary("")]
        public async Task ShowAll(string listName="") 
        {
            if (string.IsNullOrEmpty(listName))
            {
                return;
            }

            var selectedItems = CommandHandler.CustomCollectionController.GetItemList(listName);
            var message = "";

            var value = 1;
            foreach (var selectedItem in selectedItems.Items)
            {
                message += $"{value} - {selectedItem.Name}\n";
                value++;
            }

            await ReplyAsync($"{message}");
        }

        // TODO: Implement
        /*[Command("move")]
        [Summary("")]
        public async Task Move(string labelName="", int newIndex=-1) 
        {
        }

        [Command("swap")]
        [Summary("")]
        public async Task Swap(int indexOrig=-1, int indexDest=-1)
        {
        }*/

        [Command("rename")]
        [Summary("")]
        public async Task RenameList(string currentLabel="")
        {
            if (string.IsNullOrEmpty(currentLabel))
            {
                return;
            }
        }

        [Command("rename-item")]
        [Summary("")]
        public async Task RenameItem(string currentName="", string newName="")
        {
            if (string.IsNullOrEmpty(currentName) || currentName == newName)
            {
                return;
            }

            _jsonController.SaveData("CustomLists.json", CommandHandler.CustomCollectionController.RandomItemCollections);
        }
    }
}
