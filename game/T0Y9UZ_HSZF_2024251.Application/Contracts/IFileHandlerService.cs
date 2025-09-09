using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T0Y9UZ_HSZF_2024251.Model.Entities;

namespace T0Y9UZ_HSZF_2024251.Application.contracts
{
    public interface IFileHandlerService
    {
        Data JsonReader(string path);
        void JsonWriter(Data gameData, string path);
        void CreateDailyReport(int day, Hero hero);
    }
}
