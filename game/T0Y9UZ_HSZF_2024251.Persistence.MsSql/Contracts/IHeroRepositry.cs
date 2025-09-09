using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T0Y9UZ_HSZF_2024251.Model.Entities;

namespace T0Y9UZ_HSZF_2024251.Persistence.MsSql.Contracts
{
    public interface IHeroRepositry
    {
        List<Hero> GetAll();
        void Add(Hero hero);
        void Update(Hero hero);
        void Delete(Hero hero);
        List<Hero> GetHerosOnMissionData();
        List<string> GetFormattedHerosOnMissionData();
    }
}
