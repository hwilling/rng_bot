using System;

namespace rng_bot.Models
{
    public class RandomItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal WeightValue { get; set; } = 1;

        public string FormatResult()
        {
            return $"{Name}";
        }
    }
}
