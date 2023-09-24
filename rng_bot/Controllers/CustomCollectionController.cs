using rng_bot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rng_bot.Controllers
{
    public class CustomCollectionController
    {
        private IList<RandomItemCollection> _randomItemCollections = new List<RandomItemCollection>();

        public IList<RandomItemCollection> RandomItemCollections { get { return _randomItemCollections; } set { _randomItemCollections = value; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="randomItemCollectionName"></param>
        /// <param name="itemName"></param>
        public void AddItemToList(string randomItemCollectionName, string itemName)
        {
            var selectedCollection = _randomItemCollections.First(x => x.Name == randomItemCollectionName);
            
            if (!selectedCollection.ItemExists(itemName))
            {
                selectedCollection.AddItem(new RandomItem { Id = new Guid(), Name = itemName, WeightValue = 1 });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collectionName"></param>
        public void CreateCollection(string collectionName)
        {
            if (_randomItemCollections.FirstOrDefault(x => x.Name == collectionName) == null)
            {
                _randomItemCollections.Add( new RandomItemCollection(collectionName, "", new List<RandomItem>()));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collectionName"></param>
        public void DeleteCollection(string collectionName) => _randomItemCollections.Remove(_randomItemCollections.FirstOrDefault(x => x.Name == collectionName));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="itemName"></param>
        public void DeleteItem(string collectionName, string itemName)
        {
            var collection = _randomItemCollections.FirstOrDefault(x => x.Name == collectionName);
            
            if (collection.ItemExists(itemName))
            {
                collection.RemoveItem(collection.Items.FirstOrDefault(x => x.Name == itemName));
            }
        }

        public RandomItemCollection GetItemList(string listName) => _randomItemCollections.FirstOrDefault(x => x.Name == listName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="randomItemCollectionName"></param>
        /// <param name="rand"></param>
        /// <returns></returns>
        public RandomItem SelectRandomItem(string randomItemCollectionName, RandomGen rand)
        {
            var selectedCollection = _randomItemCollections.First(x => x.Name == randomItemCollectionName);

            return rand.GenerateListResult(selectedCollection.Items);
        }
    }
}
