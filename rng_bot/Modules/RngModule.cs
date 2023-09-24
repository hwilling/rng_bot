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

        private readonly CommandService _service;
        public CommandHandler CommandHandler { get; set; }

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

            foreach (var command in _service.Commands)
            {
                var description = command.Summary.IsNullOrEmpty() ? command.Summary : "No description available\n";
                embedBuilder.AddField(command.Name, description);
            }

            //_service.Commands.ToList()
            //    .ForEach(x => embedBuilder.AddField(x.Name, x.Summary ?? "No description available\n"));

            await ReplyAsync("Here's a list of commands and their description: ", false, embedBuilder.Build());
        }

        /// <summary>
        /// Parses a string and makes a dice roll/series of dice rolls based on the input
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [Command("roll")]
        [Summary("creates a result of rolling [x]d[y]\nex: to get a result between 2 and 8 !RNG roll 2d4")]
        public async Task Roll(string command="", string operation="", string amount="")
        {
            if (command.IsNullOrEmpty())
            {
                await ReplyAsync("No command entered, type \"help roll\" for details on command usage.");
                return;
            }

            var cp = new CommandParser();

            var results = cp.ParseDieRoll(command);

            if (results.Count > 0)
            {
                var total = await HandleDiceRoll();
                await IncludeAmount(total);

                return;
            }

            await ReplyAsync("Invalid command, type \"help roll\" for details on command usage.");

            #region Local Functions
            async Task<int> HandleDiceRoll()
            {
                CommandHandler.prevRollCommand = command;
                var rg = new RandomGen(CommandHandler.Rand);

                int total = 0;

                foreach (var result in results)
                {
                    total += rg.RollDice(result.NumDice, result.MaxDieVal).Total;
                }

                if (operation.IsNullOrEmpty())
                {
                    await ReplyAsync($"{total}");
                    return total;
                }

                return total;
            }

            async Task IncludeAmount(int total)
            {
                int.TryParse(amount, out int amountVal);

                if (operation == "+")
                {
                    total += amountVal;
                    await ReplyAsync($"{total}");
                    return;
                }
                else if (operation == "-")
                {
                    total -= amountVal;
                    await ReplyAsync($"{total}");
                    return;
                }

                return;
            }
            #endregion
        }

        /// <summary>
        /// Rerolls based on the last roll input
        /// </summary>
        /// <returns></returns>
        [Command("re")]
        [Summary("re-rolls the last valid roll command")]
        public async Task ReRoll()
        {
            if (CommandHandler.prevRollCommand.IsNullOrEmpty())
            {
                await ReplyAsync("No previous roll available.");
                return;
            }

            var cp = new CommandParser();
            var results = cp.ParseDieRoll(CommandHandler.prevRollCommand);

            if (results.Count > 0)
            {
                await HandleReroll();
                return;
            }

            await ReplyAsync("Invalid command, type \"help roll\" for details on command usage.");

            #region Local Functions
            async Task HandleReroll()
            {
                var rg = new RandomGen(CommandHandler.Rand);

                int total = 0;

                foreach (var result in results)
                {
                    total += rg.RollDice(result.NumDice, result.MaxDieVal).Total;
                }

                await ReplyAsync($"{total}");
                return;
            }
            #endregion
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

            var cp = new CommandParser();
            var parsedCommand = cp.ParseGenCommand(low, high);

            var rg = new RandomGen(CommandHandler.Rand);
            var result = rg.GenerateNum(parsedCommand.Low, parsedCommand.High);

            await ReplyAsync($"{result}");
        }
    }
}
