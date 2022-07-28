using Discord;
using Discord.Commands;
using rng_bot.Controllers;
using rng_bot.Extensions;
using rng_bot.Services;
using System.Linq;
using System.Threading.Tasks;

namespace rng_bot.Modules
{
    [Group("RNG")]
    public class RngModule : ModuleBase<SocketCommandContext>
    {
        public CommandHandler commandHandler { get; set; }

        private readonly CommandService _service;

        public RngModule(CommandService service) => _service = service;

        /// <summary>
        /// Shows command help dialog
        /// </summary>
        /// <returns></returns>
        [Command("help")]
        [Summary("If you don't know what this does you need help.")]
        public async Task Help()
        {
            EmbedBuilder embedBuilder = new EmbedBuilder();

            _service.Commands.ToList()
                .ForEach(x => embedBuilder.AddField(x.Name, x.Summary ?? "No description available\n"));

            await ReplyAsync("Here's a list of commands and their description: ", false, embedBuilder.Build());
        }

        /// <summary>
        /// Parses a string and makes a dice roll/series of dice rolls based on the input
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [Command("roll")]
        [Summary("creates a result of rolling [x]d[y]\nex: to get a result between 2 and 8 !RNG roll 2d4")]
        public async Task Roll(string command="")
        {
            if (command.IsNullOrEmpty())
            {
                await ReplyAsync("No command entered, type \"help roll\" for details on command usage.");
                return;
            }

            //TODO: roll based on command
            var cp = new CommandParser();

            var results = cp.ParseDieRoll(command);

            if (results.Count > 0)
            {
                commandHandler.prevRollCommand = command;
                var rg = new RandomGen(commandHandler.rand);

                int total = 0;

                foreach (var result in results)
                {
                    total += rg.RollDice(result.NumDice, result.MaxDieVal).Total;
                }

                await ReplyAsync($"{total}");
            }
            else
            {
                await ReplyAsync("Invalid command, type \"help roll\" for details on command usage.");
            }
        }

        /// <summary>
        /// Rerolls based on the last roll input
        /// </summary>
        /// <returns></returns>
        [Command("re")]
        [Summary("re-rolls the last valid roll command")]
        public async Task ReRoll()
        {
            if (commandHandler.prevRollCommand.IsNullOrEmpty())
            {
                await ReplyAsync("No previous roll available.");
                return;
            }

            var cp = new CommandParser();
            var results = cp.ParseDieRoll(commandHandler.prevRollCommand);

            if (results.Count > 0)
            {
                var rg = new RandomGen(commandHandler.rand);

                int total = 0;

                foreach (var result in results)
                {
                    total += rg.RollDice(result.NumDice, result.MaxDieVal).Total;
                }

                await ReplyAsync($"{total}");
            }
            else
            {
                await ReplyAsync("Invalid command, type \"help roll\" for details on command usage.");
            }
        }

        /// <summary>
        /// Retrieve a randomly generated number between the high and low points
        /// </summary>
        /// <param name="low"></param>
        /// <param name="high"></param>
        /// <returns></returns>
        [Command("gen")]
        [Summary("generates a number between x and y\nex: to generate a number between 3 and 7 !RNG gen 3 7")]
        public async Task Generate(string low, string high)
        {
            if (low.IsNullOrEmpty() || high.IsNullOrEmpty())
            {
                await ReplyAsync("No command entered, type \"help gen\" for details on command usage.");
                return;
            }

            //TODO: generate a random number based on command input
            var cp = new CommandParser();
            var parsedCommand = cp.ParseGenCommand(low, high);

            var rg = new RandomGen(commandHandler.rand);
            var result = rg.GenerateNum(parsedCommand.Low, parsedCommand.High);

            await ReplyAsync($"{result}");
        }
    }
}
