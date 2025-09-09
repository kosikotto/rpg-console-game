using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using T0Y9UZ_HSZF_2024251.Application.contracts;
using T0Y9UZ_HSZF_2024251.Model.Entities;

namespace T0Y9UZ_HSZF_2024251.Application.services
{
    public class FileHandlerService : IFileHandlerService
    {
        public Data JsonReader(string path)
        {
            string filePath = path;

            string jsonString = File.ReadAllText(filePath);

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            Data gameData = JsonSerializer.Deserialize<Data>(jsonString);

            return gameData;
        }

        public void JsonWriter(Data gameData, string path)
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true
            };

            string jsonString = JsonSerializer.Serialize(gameData, options);

            File.WriteAllText(path, jsonString);
        }

        public void CreateDailyReport(int day, Hero hero)
        {
            string yearDirectory = DateTime.Now.Year.ToString();
            string monthDayDirectory = Path.Combine(yearDirectory, $"{DateTime.Now.Month:D2}-{DateTime.Now.Day:D2}");
            Directory.CreateDirectory(monthDayDirectory);

            XDocument xdoc = new XDocument();

            XElement root = new XElement($"Day_{day}_Report");
            root.Add(new XElement("HeroName", hero.Name));
            root.Add(new XElement("Status", hero.Status.ToString()));
            root.Add(new XElement("HealthStatus", hero.HealthStatus));
            root.Add(new XElement("Health", hero.Health));
            root.Add(new XElement("Hunger", hero.Hunger));
            root.Add(new XElement("Thirst", hero.Thirst));
            root.Add(new XElement("Fatigue", hero.Fatigue));

            XElement abilitiesElement = new XElement("Abilities");
            foreach (var ability in hero.Abilities)
            {
                abilitiesElement.Add(new XElement("Ability", ability));
            }
            root.Add(abilitiesElement);

            XElement defeatedMonstersElement = new XElement("DefeatedMonsters");
            if (hero.DefeatedMonsters != null && hero.DefeatedMonsters.Count > 0)
            {
                foreach (var monster in hero.DefeatedMonsters)
                {
                    defeatedMonstersElement.Add(new XElement("Monster", monster));
                }
            }
            else
            {
                defeatedMonstersElement.Add(new XElement("Monster", "-"));
            }
            root.Add(defeatedMonstersElement);

            XElement completedTasksElement = new XElement("CompletedTasks");
            if (hero.CompletedTasks != null && hero.CompletedTasks.Count > 0)
            {
                foreach (var task in hero.CompletedTasks)
                {
                    completedTasksElement.Add(new XElement("Task", task));
                }
            }
            else
            {
                completedTasksElement.Add(new XElement("Task", "-"));
            }
            root.Add(completedTasksElement);

            xdoc.Add(root);

            string fileName = $"{hero.Name}_Day_{day}_Report.xml";
            string filePath = Path.Combine(monthDayDirectory, fileName);
            xdoc.Save(filePath);

            if (hero.DefeatedMonsters.Count > 0)
            {
                hero.DefeatedMonsters.Clear();
            }

            if (hero.CompletedTasks.Count > 0)
            {
                hero.CompletedTasks.Clear();
            }
        }
    }
}
