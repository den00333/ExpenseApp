using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace ExpenseApp
{
    internal class FileFunc
    {
        
        public static ctg initializeData()
        {
            String path = "Resources/category.json";
            String jsonData = File.ReadAllText(path);
            ctg catGs = JsonConvert.DeserializeObject<ctg>(jsonData);

            return catGs;
        }

        public static void updatingDataThroughList(List<String> newItems) {
            if (newItems.Count != 0)
            {
                String path = "Resources/category.json";
                ctg category = initializeData();
                foreach (var item in newItems)
                {
                    category.LCategory.Add(item);
                }

                String updatedJson = JsonConvert.SerializeObject(category, Formatting.Indented);
                File.WriteAllText(path, updatedJson);
            }
        }

        public static void deletingDataThroughList(List<String> unwatedItems)
        {
            if (unwatedItems.Count != 0)
            {
                String path = "Resources/category.json";
                ctg category = initializeData();
                foreach (var item in unwatedItems)
                {
                    category.LCategory.Remove(item);
                }
                String updatedJson = JsonConvert.SerializeObject(category, Formatting.Indented);
                File.WriteAllText(path, updatedJson);
            }
        }
    }
}
