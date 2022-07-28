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
