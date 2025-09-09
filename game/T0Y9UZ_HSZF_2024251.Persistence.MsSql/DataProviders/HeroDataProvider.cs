using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T0Y9UZ_HSZF_2024251.Model.Entities;
using T0Y9UZ_HSZF_2024251.Model.Types;
using T0Y9UZ_HSZF_2024251.Persistence.MsSql.Contracts;

namespace T0Y9UZ_HSZF_2024251.Persistence.MsSql.Dataproviders
{
    public class HeroDataProvider : IHeroRepositry
    {
        WitcherDbContext witcherDbContext;

        public HeroDataProvider(WitcherDbContext witcherDbContext)
        {
            this.witcherDbContext = witcherDbContext;
        }
        public void Add(Hero hero)
        {
            witcherDbContext.Add(hero);
            witcherDbContext.SaveChanges();
        }
        public void Update(Hero hero)
        {

            var tmp = witcherDbContext.HeroTable.Where(x => x.Name == hero.Name).FirstOrDefault();
            if (tmp != null)
            {
                tmp.Name = hero.Name;
                tmp.Status = hero.Status;
                tmp.HealthStatus = hero.HealthStatus;
                tmp.Health = hero.Health;
                tmp.Hunger = hero.Hunger;
                tmp.Thirst = hero.Thirst;
                tmp.Fatigue = hero.Fatigue;
                tmp.Abilities = hero.Abilities;
                tmp.Resources = hero.Resources;
                tmp.DaysLeft = hero.DaysLeft;
                tmp.Tasks = hero.Tasks;
                tmp.DefeatedMonsters = hero.DefeatedMonsters;
                tmp.CompletedTasks = hero.CompletedTasks;
            }
            witcherDbContext.SaveChanges();
        }
        public void Delete(Hero hero)
        {
            witcherDbContext.Remove(hero);
            witcherDbContext.SaveChanges();
        }
        public List<Hero> GetAll()
        {
            return witcherDbContext.HeroTable.ToList();
        }
        public List<Hero> GetHerosOnMissionData()
        {
            return witcherDbContext.HeroTable.Where(x => x.Status == HeroStatus.Adventure).ToList();
        }
        public List<string> GetFormattedHerosOnMissionData()
        {
            return witcherDbContext.HeroTable
                .Where(hero => hero.Status == HeroStatus.Adventure)
                .Select(hero => $"'{hero.Name}' is currently on the '{hero.Tasks[0].Name}' quest.\n*********")
                .ToList();
        }
    }
}
