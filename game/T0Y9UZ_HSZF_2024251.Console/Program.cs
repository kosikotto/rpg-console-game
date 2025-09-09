
namespace T0Y9UZ_HSZF_2024251.Console;
using System;
using T0Y9UZ_HSZF_2024251.Application.contracts;
using T0Y9UZ_HSZF_2024251.Application.services;
using T0Y9UZ_HSZF_2024251.Console.Contracts;
using T0Y9UZ_HSZF_2024251.Console.Services;
using T0Y9UZ_HSZF_2024251.Model.Entities;
using T0Y9UZ_HSZF_2024251.Model.Types;
using T0Y9UZ_HSZF_2024251.Persistence.MsSql;
using T0Y9UZ_HSZF_2024251.Persistence.MsSql.Contracts;
using T0Y9UZ_HSZF_2024251.Persistence.MsSql.Dataproviders;

public class Program
{
    static void Main(string[] args)
    {
        WitcherDbContext witcherDbContext = new WitcherDbContext();

        IHeroRepositry heroDataProvider = new HeroDataProvider(witcherDbContext);
        ITaskRepository gameTaskDataProvider = new GameTaskDataProvider(witcherDbContext);
        IMonsterRepository monsterDataProvider = new MonsterDataProvider(witcherDbContext);
        IStorageRepository storageDataProvider = new StorageDataProvider(witcherDbContext);
        IDaysRepository daysDataProvider = new DaysDataProvider(witcherDbContext);
        IFileHandlerService fileHandler = new FileHandlerService();

        IDataManipulationService dataManipulation = new DataManipulationService(
            heroDataProvider,
            gameTaskDataProvider,
            monsterDataProvider,
            storageDataProvider,
            daysDataProvider,
            fileHandler);

        IConsoleRepository consoleRepository = new ConsoleManipulation(dataManipulation);

        consoleRepository.SplashScreen();
    }
}