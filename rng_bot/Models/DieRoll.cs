using System;
using System.Collections.Generic;
using System.Text;

namespace rng_bot.Models
{
    class DieRoll
    {
        public int NumDice { get; set; }
        public int MaxDieVal { get; set; }

        /// <summary>
        /// Object
        /// </summary>
        /// <param name="numDice"></param>
        /// <param name="maxDieVal"></param>
        public DieRoll(int numDice, int maxDieVal)
        {
            NumDice = numDice;
            MaxDieVal = maxDieVal;
        }
    }
}
