using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T0Y9UZ_HSZF_2024251.Model.Entities;
using T0Y9UZ_HSZF_2024251.Persistence.MsSql.Contracts;

namespace T0Y9UZ_HSZF_2024251.Persistence.MsSql.Dataproviders
{
    public class GameTaskDataProvider : ITaskRepository
    {
        WitcherDbContext witcherDbContext;
        public GameTaskDataProvider(WitcherDbContext witcherDbContext)
        {
            this.witcherDbContext = witcherDbContext;
        }
        public void Add(GameTask GameTask)
        {
            witcherDbContext.Add(GameTask);
            witcherDbContext.SaveChanges();
        }

        public List<GameTask> GetAll()
        {
            return witcherDbContext.GameTaskTable.ToList();
        }
    }
}
