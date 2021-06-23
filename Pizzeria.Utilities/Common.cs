using Newtonsoft.Json;
using System;
using System.IO;

namespace Pizzeria.Utilities
{
    public class Common
    {
        public static string GetUniqueString()
        {
            return Guid.NewGuid().ToString();
        }

        public static void SaveJsonToFile<T>(string path, string fileName, string fileExtension, T data)
        {
            using (StreamWriter file = File.CreateText(path + fileName + fileExtension))
            {
                JsonSerializer serializer = new JsonSerializer();
                //serialize object directly into file stream
                serializer.Serialize(file, data);
            }
        }

        public static T GetJsonFromFile<T>(string path, string fileName, string fileExtension)
        {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(path + fileName + fileExtension));
        }
    }
}
