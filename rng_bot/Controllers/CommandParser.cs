using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using rng_bot.Models;

namespace rng_bot.Controllers
{
    class CommandParser
    {
        private const string _dieRollReg = @"[0-9]{1,}[dD][0-9]{1,}";
        private const string _dieSplitPattern = "d";
        private const int _numDiceIndex = 0;
        private const int _maxValIndex = 1;

        public CommandParser() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool IsValidDieRoll(string input) => new Regex(_dieRollReg).IsMatch(input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<DieRoll> ParseDieRoll(string input)
        {
            Regex regex = new Regex(_dieRollReg);
            MatchCollection matches = regex.Matches(input);

            List<DieRoll> diceRolls = new List<DieRoll>();

            matches.ToList().ForEach(x => DieRollAction(x, diceRolls));

            return diceRolls;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="match"></param>
        /// <param name="diceRolls"></param>
        private void DieRollAction(Match match, List<DieRoll> diceRolls)
        {
            var diceVals = Regex.Split(match.Value, _dieSplitPattern, RegexOptions.IgnoreCase);

            int.TryParse(diceVals[_numDiceIndex], out int numDice);
            int.TryParse(diceVals[_maxValIndex], out int maxDieVal);

            diceRolls.Add(new DieRoll(numDice, maxDieVal));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="low"></param>
        /// <param name="high"></param>
        /// <returns></returns>
        public Generation ParseGenCommand(string low, string high)
        {
            int.TryParse(low, out int lowVal);
            int.TryParse(high, out int highVal);

            return new Generation(lowVal, highVal);
        }
    }
}
