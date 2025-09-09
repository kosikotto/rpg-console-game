using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T0Y9UZ_HSZF_2024251.Model.Entities;
using T0Y9UZ_HSZF_2024251.Persistence.MsSql.Contracts;

namespace T0Y9UZ_HSZF_2024251.Persistence.MsSql.Dataproviders
{
    public class DaysDataProvider : IDaysRepository
    {
        WitcherDbContext witcherDbContext;

        public DaysDataProvider(WitcherDbContext witcherDbContext)
        {
            this.witcherDbContext = witcherDbContext;
        }

        public void Add(DaysPassed daysPassed)
        {
            witcherDbContext.Add(daysPassed);
            witcherDbContext.SaveChanges();
        }

        public void Update(DaysPassed daysPassed)
        {
            var tmp = witcherDbContext.DaysTable.Where(x => x.Id == 1).FirstOrDefault();
            if (tmp != null)
            {
                tmp.currentDay = daysPassed.currentDay;
            }
            witcherDbContext.SaveChanges();
        }

        public List<DaysPassed> GetAll()
        {
            return witcherDbContext.DaysTable.ToList();
        }

        public int GetCurrentDay()
        {
            return witcherDbContext.DaysTable.Where(x => x.Id == 1).Select(x => x.currentDay).FirstOrDefault();
        }

    }
}