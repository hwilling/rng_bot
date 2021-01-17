using System;
using System.Collections.Generic;
using System.Text;

namespace rng_bot.Modules
{
    class RollResult
    {
        public int Total { get; private set; }
        public List<int> Results { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public RollResult()
        {
            Total = 0;
            Results = new List<int>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public void AddResult(int value)
        {
            Results.Add(value);
            Total += value;
        }
    }
}
