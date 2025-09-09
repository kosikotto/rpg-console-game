using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T0Y9UZ_HSZF_2024251.Model.Entities;
using T0Y9UZ_HSZF_2024251.Persistence.MsSql.Contracts;

namespace T0Y9UZ_HSZF_2024251.Persistence.MsSql.Dataproviders
{
    public class MonsterDataProvider : IMonsterRepository
    {
        WitcherDbContext witcherDbContext { get; set; }
        public MonsterDataProvider(WitcherDbContext witcherDbContext)
        {
            this.witcherDbContext = witcherDbContext;
        }
        public void Add(Monster monster)
        {
            witcherDbContext.Add(monster);
            witcherDbContext.SaveChanges();
        }
        public List<Monster> GetAll()
        {
            return witcherDbContext.MonsterTable.ToList();
        }
    }
}
