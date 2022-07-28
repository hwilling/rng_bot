using System.IO;
using Newtonsoft.Json;

namespace rng_bot.Controllers
{
    public class JsonController
    {
        private readonly string _customCollectionDir = "";

        /// <summary>
        /// Save current state of given object as a JSON file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="saveLocation"></param>
        /// <param name="saveObject"></param>
        public void SaveData<T>(string saveLocation, T saveObject)
        {
            var serializer = new JsonSerializer();

            using (var sw = new StreamWriter($"{_customCollectionDir}/{saveLocation}"))
            using (var jw = new JsonTextWriter(sw))
            {
                serializer.Serialize(jw, saveObject);
            }
        }

        /// <summary>
        /// Reads saved JSON data and converts it to the given object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileLocation"></param>
        /// <returns></returns>
        public T LoadData<T>(string fileLocation) => JsonConvert.DeserializeObject<T>(new StreamReader($"{_customCollectionDir}/{fileLocation}").ReadToEnd());
    }
}
