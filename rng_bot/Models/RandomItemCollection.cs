using System;
using System.Collections.Generic;
using System.Text;

namespace rng_bot.Models
{
    public class RandomItemCollection
    {
        private IList<RandomItem> _items;
        public IList<RandomItem> Items { get => _items; }
        public int Count { get => _items.Count; }
        public string Name { get; private set; }
        public string Owner { get; private set; }

        public void CreateList(string name , string owner, IList<RandomItem> items)
        {
            _items = items;
            Name = name;
            Owner = owner;
        }

        public void AddItem(RandomItem item) => _items.Add(item);

        public void RemoveItem(RandomItem item) => _items.Remove(item);

        public void Rename(string name) => Name = name;

        public void SetNewOwner(string owner) => Owner = owner;
    }
}
