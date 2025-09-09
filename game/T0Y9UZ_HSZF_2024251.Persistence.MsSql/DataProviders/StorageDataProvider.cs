using Microsoft.Identity.Client.Extensions.Msal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T0Y9UZ_HSZF_2024251.Model.Entities;
using T0Y9UZ_HSZF_2024251.Persistence.MsSql.Contracts;

namespace T0Y9UZ_HSZF_2024251.Persistence.MsSql.Dataproviders
{
    public class StorageDataProvider : IStorageRepository
    {
        WitcherDbContext witcherDbContext;

        public StorageDataProvider(WitcherDbContext witcherDbContext)
        {
            this.witcherDbContext = witcherDbContext;
        }
        public void Add(Resources resources)
        {
            witcherDbContext.Add(resources);
            witcherDbContext.SaveChanges();
        }

        public void Update(Resources resources)
        {
            var tmp = witcherDbContext.ResourcesTable.Where(x => x.Id == resources.Id).FirstOrDefault();
            if (tmp != null)
            {
                tmp.Weapons = resources.Weapons;
                tmp.Water = resources.Water;
                tmp.AlchemyIngredients = resources.AlchemyIngredients;
                tmp.Food = resources.Food;
            }
            witcherDbContext.SaveChanges();
        }

        public List<Resources> GetAll()
        {
            return witcherDbContext.ResourcesTable.ToList();
        }

        public Resources GetMainStorage()
        {
            return witcherDbContext.ResourcesTable.OrderByDescending(x => x.Id).FirstOrDefault();
        }
    }
}
