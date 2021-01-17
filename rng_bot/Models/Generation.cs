using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace rng_bot.Models
{
    class Generation
    {
        public int Low { get; private set; }
        public int High { get; private set; }

        public Generation(int low, int high)
        {
            Low = low;
            High = high;
        }
    }
}
