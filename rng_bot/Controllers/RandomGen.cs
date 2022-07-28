using System;
using System.Collections.Generic;
using rng_bot.Models;

namespace rng_bot.Controllers
{
    public class RandomGen
    {
        private const int _dieMinVal = 1;
        private Random _rand;

        public RandomGen(Random rand) => _rand = rand;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public int GenerateNum(int min, int max) => _rand.Next(min, max);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numDice"></param>
        /// <param name="dieType"></param>
        /// <returns></returns>
        public RollResult RollDice(int numDice, int dieType)
        {
            var diceResults = new RollResult();

            for (int dieCount = 0; dieCount < numDice; dieCount++)
            {
                diceResults.AddResult(GenerateNum(_dieMinVal, dieType));
            }

            return diceResults;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public RandomItem GenerateListResult(IList<RandomItem> items) => items[GenerateNum(1, items.Count) - 1];
    }
}
