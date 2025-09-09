using Microsoft.Identity.Client.Extensions.Msal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T0Y9UZ_HSZF_2024251.Application.contracts;
using T0Y9UZ_HSZF_2024251.Application.services;
using T0Y9UZ_HSZF_2024251.Console.Contracts;
using T0Y9UZ_HSZF_2024251.Model.Entities;
using T0Y9UZ_HSZF_2024251.Model.Types;
using T0Y9UZ_HSZF_2024251.Persistence.MsSql.Contracts;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace T0Y9UZ_HSZF_2024251.Console.Services
{
    public class ConsoleManipulation : IConsoleRepository
    {
        public IDataManipulationService dataManipulation;
        public ConsoleManipulation(IDataManipulationService dataManipulation)
        {
            this.dataManipulation = dataManipulation;
        }

        //FŐMENÜRENDSZER
        public void SplashScreen()
        {
            System.Console.ForegroundColor = ConsoleColor.DarkYellow;
            System.Console.WriteLine("***************************************");
            System.Console.WriteLine("*                                     *");
            System.Console.WriteLine("*     The Witcher: Console Kingdoms   *");
            System.Console.WriteLine("*                                     *");
            System.Console.WriteLine("*       Powered by Console Engine     *");
            System.Console.WriteLine("*                                     *");
            System.Console.WriteLine("*       A TibySoft Production         *");
            System.Console.WriteLine("*                                     *");
            System.Console.WriteLine("*        2024 TibySoft Studios        *");
            System.Console.WriteLine("*    Inspired by Andrzej Sapkowski    *");
            System.Console.WriteLine("*                                     *");
            System.Console.WriteLine("***************************************");
            System.Console.ResetColor();

            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine("*         Press Enter to Begin        *");
            System.Console.ResetColor();

            while (System.Console.ReadKey(true).Key != ConsoleKey.Enter)
            {

            }

            ShowMainMenu();
        }
        public void ShowMainMenu()
        {
            System.Console.Clear();
            System.Console.WriteLine($"The Witcher: Console Kingdom\n\n" +
                $"Main Menu\n" +
                $"*********\n\n" +
                $"1. New Game\n" +
                $"2. Load Game\n" +
                $"0. Exit Game\n\n" +
                $"Choose an option using the 'listed' numbers on your keyboard");

            switch (System.Console.ReadKey(true).Key)
            {
                case ConsoleKey.D1:
                    dataManipulation.CreatingNewGame(Path.Combine(Path.Combine("save","default"), "new_game.json"));
                    NewGame("", new List<Hero>());
                    break;
                case ConsoleKey.D2:
                    MainMenuLoadGame();
                    break;
                case ConsoleKey.D0:
                    System.Console.Clear();
                    System.Console.WriteLine(dataManipulation.ExitGame());
                    break;
                default: ShowMainMenu(); break;
            }
        }
        public void MainMenuLoadGame()
        {
            System.Console.Clear();
            System.Console.WriteLine($"Load Game\n" +
                $"*********\n");

            var files = dataManipulation.ReadFilesData();

            if (files.Length == 0)
            {
                for (int i = 1; i <= 5; i++)
                {
                    System.Console.WriteLine($" -> {i}. Empty");
                }

                System.Console.WriteLine("\n0. Back to Main Menu");

                while (System.Console.ReadKey(true).Key != ConsoleKey.D0)
                {

                }
                ShowMainMenu();
            }

            else
            {
                for (int i = 1; i <= files.Length; i++)
                {
                    System.Console.WriteLine($" -> {i}. {dataManipulation.GetFilesName(files[i - 1])}");
                }

                for (int i = files.Length + 1; i <= 5; i++)
                {
                    System.Console.WriteLine($" -> {i}. Empty");
                }

                System.Console.WriteLine($"\n0. Back to Main Menu");

                if (files.Length == 5)
                {
                    switch (System.Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.D1:
                            dataManipulation.CreatingNewGame(files[0]);
                            ShowBasicGameInfos();
                            break;

                        case ConsoleKey.D2:
                            dataManipulation.CreatingNewGame(files[1]);
                            ShowBasicGameInfos();
                            break;

                        case ConsoleKey.D3:
                            dataManipulation.CreatingNewGame(files[2]);
                            ShowBasicGameInfos();
                            break;

                        case ConsoleKey.D4:
                            dataManipulation.CreatingNewGame(files[3]);
                            ShowBasicGameInfos();
                            break;

                        case ConsoleKey.D5:
                            dataManipulation.CreatingNewGame(files[4]);
                            ShowBasicGameInfos();
                            break;

                        case ConsoleKey.D0:
                            ShowMainMenu();
                            break;
                        default: MainMenuLoadGame(); break;
                    }
                }
                if (files.Length == 4)
                {
                    switch (System.Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.D1:
                            dataManipulation.CreatingNewGame(files[0]);
                            ShowBasicGameInfos();
                            break;

                        case ConsoleKey.D2:
                            dataManipulation.CreatingNewGame(files[1]);
                            ShowBasicGameInfos();
                            break;

                        case ConsoleKey.D3:
                            dataManipulation.CreatingNewGame(files[2]);
                            ShowBasicGameInfos();
                            break;

                        case ConsoleKey.D4:
                            dataManipulation.CreatingNewGame(files[3]);
                            ShowBasicGameInfos();
                            break;

                        case ConsoleKey.D0:
                            ShowMainMenu();
                            break;
                        default: MainMenuLoadGame(); break;
                    }
                }
                if (files.Length == 3)
                {
                    switch (System.Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.D1:
                            dataManipulation.CreatingNewGame(files[0]);
                            ShowBasicGameInfos();
                            break;

                        case ConsoleKey.D2:
                            dataManipulation.CreatingNewGame(files[1]);
                            ShowBasicGameInfos();
                            break;

                        case ConsoleKey.D3:
                            dataManipulation.CreatingNewGame(files[2]);
                            ShowBasicGameInfos();
                            break;

                        case ConsoleKey.D0:
                            ShowMainMenu();
                            break;
                        default: MainMenuLoadGame(); break;
                    }
                }
                if (files.Length == 2)
                {
                    switch (System.Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.D1:
                            dataManipulation.CreatingNewGame(files[0]);
                            ShowBasicGameInfos();
                            break;

                        case ConsoleKey.D2:
                            dataManipulation.CreatingNewGame(files[1]);
                            ShowBasicGameInfos();
                            break;

                        case ConsoleKey.D0:
                            ShowMainMenu();
                            break;
                        default: MainMenuLoadGame(); break;
                    }
                }
                if (files.Length == 1)
                {
                    switch (System.Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.D1:
                            dataManipulation.CreatingNewGame(files[0]);
                            ShowBasicGameInfos();
                            break;

                        case ConsoleKey.D0:
                            ShowMainMenu();
                            break;
                        default: MainMenuLoadGame(); break;
                    }
                }

            }
        }
        public void NewGame(string message, ICollection<Hero> heroes)
        {
            System.Console.Clear();
            System.Console.WriteLine($"Building a team of 4 members\n*********\n\n" +
                $"Currently, your team has '{heroes.Count}' members.");
            if (heroes.Count != 0)
            {
                System.Console.WriteLine("Heroes in your team:");
                foreach (var item in heroes)
                {
                    System.Console.WriteLine($"  -> {item.Name}");
                }
            }

            System.Console.WriteLine("");

            var tmp = dataManipulation.GetEveryHeroData();
            for (int i = 0; i < tmp.Count; i++)
            {
                System.Console.WriteLine($"{i + 1}. {tmp.ElementAt(i).Name}");
            }

            System.Console.WriteLine($"\n7. Reset filter\n" +
                $"8. Filter heroes by ability\n" +
                $"9. Continue\n" +
                $"\n0. Back\n");

            if (message.Length != 0)
            {
                System.Console.WriteLine($"{message}");
            }

            var tmp2 = dataManipulation.CheckNewGameAbilityIndex(tmp);
            if (tmp2 != "")
            {
                System.Console.WriteLine(tmp2);
            }

            System.Console.WriteLine("Press the number key of the character to add or remove them.");

            switch (System.Console.ReadKey(true).Key)
            {
                case ConsoleKey.D1:
                    if (heroes.Contains(tmp.ElementAt(0)))
                    {
                        dataManipulation.TeamBuildUpRemoveHero(heroes, tmp.ElementAt(0));
                        NewGame($"'{tmp.ElementAt(0).Name}' has been removed from the team.\n", heroes);
                    }

                    else
                    {
                        dataManipulation.TeamBuildUpAddHero(heroes, tmp.ElementAt(0));
                        NewGame($"'{tmp.ElementAt(0).Name}' has been added to the team.\n", heroes);
                    }
                    break;
                case ConsoleKey.D2:
                    if (heroes.Contains(tmp.ElementAt(1)))
                    {
                        dataManipulation.TeamBuildUpRemoveHero(heroes, tmp.ElementAt(1));
                        NewGame($"'{tmp.ElementAt(1).Name}' has been removed from the team.\n", heroes);
                    }

                    else
                    {
                        dataManipulation.TeamBuildUpAddHero(heroes, tmp.ElementAt(1));
                        NewGame($"'{tmp.ElementAt(1).Name}' has been added to the team.\n", heroes);
                    }
                    break;
                case ConsoleKey.D3:
                    if (heroes.Contains(tmp.ElementAt(2)))
                    {
                        dataManipulation.TeamBuildUpRemoveHero(heroes, tmp.ElementAt(2));
                        NewGame($"'{tmp.ElementAt(2).Name}' has been removed from the team.\n", heroes);
                    }

                    else
                    {
                        dataManipulation.TeamBuildUpAddHero(heroes, tmp.ElementAt(2));
                        NewGame($"'{tmp.ElementAt(2).Name}' has been added to the team.\n", heroes);
                    }
                    break;
                case ConsoleKey.D4:
                    if (heroes.Contains(tmp.ElementAt(3)))
                    {
                        dataManipulation.TeamBuildUpRemoveHero(heroes, tmp.ElementAt(3));
                        NewGame($"'{tmp.ElementAt(3).Name}' has been removed from the team.\n", heroes);
                    }

                    else
                    {
                        dataManipulation.TeamBuildUpAddHero(heroes, tmp.ElementAt(3));
                        NewGame($"'{tmp.ElementAt(3).Name}' has been added to the team.\n", heroes);
                    }
                    break;
                case ConsoleKey.D5:
                    if (heroes.Contains(tmp.ElementAt(4)))
                    {
                        dataManipulation.TeamBuildUpRemoveHero(heroes, tmp.ElementAt(4));
                        NewGame($"'{tmp.ElementAt(4).Name}' has been removed from the team.\n", heroes);
                    }

                    else
                    {
                        dataManipulation.TeamBuildUpAddHero(heroes, tmp.ElementAt(4));
                        NewGame($"'{tmp.ElementAt(4).Name}' has been added to the team.\n", heroes);
                    }
                    break;
                case ConsoleKey.D7:
                    dataManipulation.CheckNewGameAbilityIndexReset();
                    NewGame("", heroes);
                    break;
                case ConsoleKey.D8:
                    dataManipulation.CheckNewGameAbilityIndexIncrease();
                    NewGame("", heroes);
                    break;
                case ConsoleKey.D9:
                    if (heroes.Count == 4)
                    {
                        dataManipulation.TeamBuildCompleted(tmp, heroes);
                        ShowBasicGameInfos();
                    }

                    else
                    {
                        if (heroes.Count > 4)
                        {
                            NewGame("You have too many heroes in your team.\n", heroes);
                        }

                        else
                        {
                            NewGame("You have too few heroes in your team.\n", heroes);
                        }

                    }
                    break;
                case ConsoleKey.D0:
                    ShowMainMenu();
                    break;
                default:
                    NewGame("", heroes);
                    break;
            }
        }



        //INNENTŐL JÖN MAGA A JÁTÉKMENET
        //ELOSZTÓ MENÜ
        public void ShowBasicGameInfos()
        {
            ShowBasicHeroInfos();
            DisplayOptions();
        }
        public void ShowBasicHeroInfos()
        {
            System.Console.Clear();
            System.Console.WriteLine($"HEROES & OPTIONS\n" +
                $"*********\n\n" +
                $"{dataManipulation.GetBasicHeroInfos()}\n" +
                $"*********");
        }
        public void DisplayOptions()
        {
            var tmp = dataManipulation.FormatHerosOnMission();
            foreach (var item in tmp)
            {
                System.Console.WriteLine(item);
            }
            

            System.Console.WriteLine($"1. List Characters\n" +
                $"2. List Tasks\n" +
                $"3. Monster Hunting\n" +
                $"4. List Inventory\n\n" +
                $"9. Next Day\n\n" +
                $"Current Day: {dataManipulation.GetCurrentDay()}\n\n" +
                $"Press the 'Escape' button to pause the game");


            switch (System.Console.ReadKey(true).Key)
            {
                case ConsoleKey.D1:
                    ShowHeroesListOptions();
                    break;
                case ConsoleKey.D2:
                    ShowTasks();
                    break;
                case ConsoleKey.D3:
                    ShowAvailableMonsters();
                    break;
                case ConsoleKey.D4:
                    ShowResources();
                    break;
                case ConsoleKey.D9:
                    dataManipulation.NextDay();
                    dataManipulation.CheckIfGameFinished();
                    if (dataManipulation.CheckIfGameIsRunning())
                    {
                        ShowBasicGameInfos();
                    }

                    else
                    {
                        if (dataManipulation.DaysLimitReached())
                        {
                            ShowGameCompleted();
                        }

                        if (dataManipulation.CheckIfEveryHeroPassedAway())
                        {
                            ShowGameOver();
                        }
                    }
                    break;
                case ConsoleKey.Escape:
                    PauseGame();
                    return;
                default: ShowBasicGameInfos(); break;
            }
        }



        //HŐSÖK MEGJELENÍTÉSE
        public void ShowHeroesListOptions()
        {
            System.Console.Clear();
            System.Console.WriteLine($"LIST HEROES\n*********\n\n" +
                $"1. List all heroes\n" +
                $"2. Filter by abilities\n\n" +
                $"0. Back");

            switch (System.Console.ReadKey(true).Key)
            {
                case ConsoleKey.D1:
                    ListHeroes();
                    break;
                case ConsoleKey.D2:
                    FilterHeroesByAbility();
                    break;
                case ConsoleKey.D0:
                    ShowBasicGameInfos();
                    break;
                default:
                    ShowHeroesListOptions();
                    break;
            }
        }
        public void FilterHeroesByAbility()
        {
            System.Console.Clear();
            System.Console.WriteLine("AVAILABLE ABILITIES\n*********\n");

            System.Console.WriteLine($"1. combat\n" +
                $"2. alchemy\n" +
                $"3. sorcery\n" +
                $"4. healing\n" +
                $"5. tracking\n\n" +
                $"0. Back");

            switch (System.Console.ReadKey(true).Key)
            {
                case ConsoleKey.D1:
                    ShowFilteredHeroesByAbility("combat");
                    break;
                case ConsoleKey.D2:
                    ShowFilteredHeroesByAbility("alchemy");
                    break;
                case ConsoleKey.D3:
                    ShowFilteredHeroesByAbility("sorcery");
                    break;
                case ConsoleKey.D4:
                    ShowFilteredHeroesByAbility("healing");
                    break;
                case ConsoleKey.D5:
                    ShowFilteredHeroesByAbility("tracking");
                    break;
                case ConsoleKey.D0:
                    ShowHeroesListOptions();
                    break;
                default:
                    FilterHeroesByAbility();
                    break;
            }
        }
        public void ShowFilteredHeroesByAbility(string ability)
        {
            System.Console.Clear();
            System.Console.WriteLine($"HEROES WITH THE FOLLOWING ABILITY: {ability}\n" +
                $"*********\n");

            var tmp = dataManipulation.GetEveryHeroData();

            foreach (var item in tmp)
            {
                if (item.Abilities.Contains(ability))
                {
                    System.Console.WriteLine($" -> {item.Name}");
                }
            }

            System.Console.WriteLine($"\n0. Back"); ;

            switch (System.Console.ReadKey(true).Key)
            {
                case ConsoleKey.D0:
                    FilterHeroesByAbility();
                    break;
                default:
                    ShowFilteredHeroesByAbility(ability);
                    break;
            }
        }
        public void ListHeroes()
        {
            bool isRunning = true;
            while (isRunning)
            {
                System.Console.Clear();
                System.Console.WriteLine("HEROES\n*********\n");
                var tmp = dataManipulation.GetEveryHeroData();
                for (int i = 0; i < tmp.Count; i++)
                {
                    var item = tmp.ElementAt(i);
                    System.Console.WriteLine($"{i + 1}. {item.Name}");
                }

                System.Console.WriteLine("\n0. Back");

                switch (System.Console.ReadKey(true).Key)
                {
                    case ConsoleKey.D0:
                        isRunning = false;
                        ShowHeroesListOptions();
                        break;
                    case ConsoleKey.D1:
                        if (tmp.Count >= 1) ShowHeroInfos(tmp, 0);
                        break;
                    case ConsoleKey.D2:
                        if (tmp.Count >= 2) ShowHeroInfos(tmp, 1);
                        break;
                    case ConsoleKey.D3:
                        if (tmp.Count >= 3) ShowHeroInfos(tmp, 2);
                        break;
                    case ConsoleKey.D4:
                        if (tmp.Count >= 4) ShowHeroInfos(tmp, 3);
                        break;
                }
            }
        }
        public void ShowHeroInfos(ICollection<Hero> data, int idxOfHero)
        {
            bool isRunning = true;
            while (isRunning)
            {
                System.Console.Clear();
                System.Console.WriteLine("HERO ATTRIBUTES\n*********\n");

                var item = data.ElementAt(idxOfHero);

                System.Console.WriteLine($"Hero: {item.Name}\n" +
                        $" -> STATUS: {item.Status}\n\n" +
                        $" -> Health: {item.HealthStatus}\n" +
                        $" -> Hunger: {item.Hunger}\n" +
                        $" -> Thirst: {item.Thirst}\n" +
                        $" -> Fatigue: {item.Fatigue}\n" +
                        $"\n -> Abilities: ");

                foreach (var abilities in item.Abilities)
                {
                    System.Console.WriteLine($"  --> {abilities}");
                }

                System.Console.WriteLine($"\n -> Resources: ");
                System.Console.WriteLine($"  --> Food: {item.Resources.Food}\n" +
                    $"  --> Water: {item.Resources.Water}\n" +
                    $"  --> Alchemy Ingredients: {item.Resources.AlchemyIngredients}\n" +
                    $"  --> Weapons: {item.Resources.Weapons}\n");

                if (item.Status == HeroStatus.Adventure)
                {
                    System.Console.WriteLine($"4. Abort mission");
                }

                System.Console.WriteLine($"5. Eat\n" +
                    $"6. Drink\n" +
                    $"7. {dataManipulation.SendToRecovery(item)}\n" +
                    $"8. Place into storage\n" +
                    $"9. Retrieve from storage\n\n" +
                    $"0. Back");

                switch (System.Console.ReadKey(true).Key)
                {
                    case ConsoleKey.D0:
                        isRunning = false;
                        break;
                    case ConsoleKey.D4:
                        if (item.Status == HeroStatus.Adventure)
                        {
                            var tmpMission = dataManipulation.GetTaskFromHero(item);
                            dataManipulation.AbortMission(item, tmpMission);
                        }
                        break;
                    case ConsoleKey.D5:
                        dataManipulation.Eat(item);
                        break;
                    case ConsoleKey.D6:
                        dataManipulation.Drink(item);
                        break;
                    case ConsoleKey.D7:
                        if (item.Status == HeroStatus.Active || item.Status == HeroStatus.Recovery)
                        {
                            dataManipulation.Recovery(item);
                        }
                        break;
                    case ConsoleKey.D8:
                        PlaceIntoStorage(item, data, idxOfHero);
                        break;
                    case ConsoleKey.D9:
                        GetFromStorage(item, data, idxOfHero);
                        break;
                }
            }
        }
        public void PlaceIntoStorage(Hero hero, ICollection<Hero> data, int idxOfHero)
        {
            bool isRunning = true;
            while (isRunning)
            {
                System.Console.Clear();
                var storage = dataManipulation.GetMainStorage();

                System.Console.WriteLine($"PLACE INTO STORAGE\n" +
                    $"*********\n\n" +
                    $"5. Add Food\n" +
                    $"6. Add Water\n" +
                    $"7. Add Alchemy Ingredients\n" +
                    $"8. Add Weapons\n\n" +
                    $"HERO'S BACKPACK\n" +
                    $"*********\n\n" +
                    $"Food: {hero.Resources.Food}\n" +
                    $"Water: {hero.Resources.Water}\n" +
                    $"Alchemy Ingredients: {hero.Resources.AlchemyIngredients}\n" +
                    $"Weapons: {hero.Resources.Weapons}\n\n" +
                    $"STORAGE CONTENT\n" +
                    $"*********\n\n" +
                    $"Food: {storage.Food}" +
                    $"\nWater: {storage.Water}" +
                    $"\nAlchemy Ingredients: {storage.AlchemyIngredients}" +
                    $"\nWeapons: {storage.Weapons}\n\n" +
                    $"0. Back");

                switch (System.Console.ReadKey(true).Key)
                {
                    case ConsoleKey.D0:
                        isRunning = false;
                        break;
                    case ConsoleKey.D5:
                        if (hero.Status == HeroStatus.Active && hero.Resources.Food > 0)
                        {
                            dataManipulation.FoodPutGetIntoFromMainStorage(hero, storage, "-");
                        }
                        break;
                    case ConsoleKey.D6:
                        if (hero.Status == HeroStatus.Active && hero.Resources.Water > 0)
                        {
                            dataManipulation.WaterPutGetIntoFromMainStorage(hero, storage, "-");
                        }
                        break;
                    case ConsoleKey.D7:
                        if (hero.Status == HeroStatus.Active && hero.Resources.AlchemyIngredients > 0)
                        {
                            dataManipulation.AlchemyIngredientPutGetIntoFromMainStorage(hero, storage, "-");
                        }
                        break;
                    case ConsoleKey.D8:
                        if (hero.Status == HeroStatus.Active && hero.Resources.Weapons > 0)
                        {
                            dataManipulation.WeaponPutGetIntoFromMainStorage(hero, storage, "-");
                        }
                        break;
                }
            }
        }
        public void GetFromStorage(Hero hero, ICollection<Hero> data, int idxOfHero)
        {
            bool isRunning = true;
            while (isRunning)
            {
                System.Console.Clear();
                var storage = dataManipulation.GetMainStorage();

                System.Console.WriteLine("TAKE FROM STORAGE\n" +
                    "*********\n\n" +
                    $"5. Take Food\n" +
                    $"6. Take Water\n" +
                    $"7. Take Alchemy Ingredients\n" +
                    $"8. Take Weapons\n\n" +
                    "HERO'S BACKPACK\n" +
                    "*********\n\n" +
                    $"Food: {hero.Resources.Food}\n" +
                    $"Water: {hero.Resources.Water}\n" +
                    $"Alchemy Ingredients: {hero.Resources.AlchemyIngredients}\n" +
                    $"Weapons: {hero.Resources.Weapons}\n\n" +
                    "STORAGE CONTENT\n" +
                    "*********\n\n" +
                    $"Food: {storage.Food}\n" +
                    $"Water: {storage.Water}\n" +
                    $"Alchemy Ingredients: {storage.AlchemyIngredients}\n" +
                    $"Weapons: {storage.Weapons}\n\n" +
                    "0. Back");

                switch (System.Console.ReadKey(true).Key)
                {
                    case ConsoleKey.D0:
                        isRunning = false;
                        break;
                    case ConsoleKey.D5:
                        if (hero.Status == HeroStatus.Active && storage.Food > 0)
                        {
                            dataManipulation.FoodPutGetIntoFromMainStorage(hero, storage, "+");
                        }
                        break;
                    case ConsoleKey.D6:
                        if (hero.Status == HeroStatus.Active && storage.Water > 0)
                        {
                            dataManipulation.WaterPutGetIntoFromMainStorage(hero, storage, "+");
                        }
                        break;
                    case ConsoleKey.D7:
                        if (hero.Status == HeroStatus.Active && storage.AlchemyIngredients > 0)
                        {
                            dataManipulation.AlchemyIngredientPutGetIntoFromMainStorage(hero, storage, "+");
                        }
                        break;
                    case ConsoleKey.D8:
                        if (hero.Status == HeroStatus.Active && storage.Weapons > 0)
                        {
                            dataManipulation.WeaponPutGetIntoFromMainStorage(hero, storage, "+");
                        }
                        break;
                }
            }
        }



        //TASKOK MEGJLENÍTÉSE
        public void ShowTasks()
        {
            System.Console.Clear();
            System.Console.WriteLine($"TASKS\n*********\n");
            var tmp = dataManipulation.GetEveryTaskData();
            List<int> randomTasks = dataManipulation.GenerateRandomTasks();

            for (int i = 0; i < randomTasks.Count; i++)
            {
                var item = tmp[randomTasks[i]];

                System.Console.WriteLine($"{i + 1}. {item.Name}");
            }

            System.Console.WriteLine($"\n0. Back");
            switch (System.Console.ReadKey(true).Key)
            {
                case ConsoleKey.D0:
                    ShowBasicGameInfos();
                    break;
                case ConsoleKey.D1:
                    ShowTaskInfo(tmp[randomTasks[0]]);
                    break;
                case ConsoleKey.D2:
                    ShowTaskInfo(tmp[randomTasks[1]]);
                    break;
                case ConsoleKey.D3:
                    ShowTaskInfo(tmp[randomTasks[2]]);
                    break;
                default:
                    ShowTasks();
                    break;
            }
        }
        public void ShowTaskInfo(GameTask task)
        {
            System.Console.Clear();

            System.Console.WriteLine($"MISSION: '{task.Name}'\n" +
                $"*********\n");

            System.Console.WriteLine($" -> Duration: {task.Duration}\n" +
                $" -> Required resources: \n" +
                $"  --> Food: {task.RequiredResources.Food}\n" +
                $"  --> Water: {task.RequiredResources.Water}\n" +
                $"  --> Weapons: {task.RequiredResources.Weapons}\n" +
                $" -> Attribute changes: \n" +
                $"  --> Health: {task.AffectedStatus.Health}\n" +
                $"  --> Thirst: {task.AffectedStatus.Thirst}\n" +
                $"  --> Hunger: {task.AffectedStatus.Hunger}\n" +
                $"  --> Fatigue: {task.AffectedStatus.Fatigue}" +
                $"\n\n9. Accept" +
                $"\n\n0. Back");
            ;

            switch (System.Console.ReadKey(true).Key)
            {
                case ConsoleKey.D0:
                    ShowTasks();
                    break;
                case ConsoleKey.D9:
                    ChooseHeroForTask(task);
                    break;
                default:
                    ShowTaskInfo(task);
                    break;
            }
        }
        public void ChooseHeroForTask(GameTask task)
        {
            System.Console.Clear();
            System.Console.WriteLine($"CHOOSE A HERO\n" +
                $"Mission: {task.Name}\n" +
                $"*********\n");
            var tmp = dataManipulation.GetEveryHeroData();
            for (int i = 0; i < tmp.Count; i++)
            {
                var item = tmp[i];

                System.Console.WriteLine($"{i + 1}. {item.Name} -> {dataManipulation.CheckIfHeroFits(item, task)}");
            }

            System.Console.WriteLine("\n0. Back");

            ;
            switch (System.Console.ReadKey(true).Key)
            {
                case ConsoleKey.D0:
                    ShowTaskInfo(task);
                    break;
                case ConsoleKey.D1:
                    if (dataManipulation.CheckIfHeroFitsForTask(tmp[0].Status, dataManipulation.CheckIfHeroFits(tmp[0], task)))
                    {
                        dataManipulation.SentToMission(tmp[0], task);
                        ShowBasicGameInfos(dataManipulation.SuccessToSendToMission(tmp[0], task));
                    }

                    else
                    {
                        ChooseHeroForTask(task);
                    }

                    break;
                case ConsoleKey.D2:
                    if (dataManipulation.CheckIfHeroFitsForTask(tmp[1].Status, dataManipulation.CheckIfHeroFits(tmp[1], task)))
                    {
                        dataManipulation.SentToMission(tmp[1], task);
                        ShowBasicGameInfos(dataManipulation.SuccessToSendToMission(tmp[1], task));
                    }

                    else
                    {
                        ChooseHeroForTask(task);
                    }
                    break;
                case ConsoleKey.D3:
                    if (dataManipulation.CheckIfHeroFitsForTask(tmp[2].Status, dataManipulation.CheckIfHeroFits(tmp[2], task)))
                    {
                        dataManipulation.SentToMission(tmp[2], task);
                        ShowBasicGameInfos(dataManipulation.SuccessToSendToMission(tmp[2], task));
                    }

                    else
                    {
                        ChooseHeroForTask(task);
                    }
                    break;
                case ConsoleKey.D4:
                    if (dataManipulation.CheckIfHeroFitsForTask(tmp[3].Status, dataManipulation.CheckIfHeroFits(tmp[3], task)))
                    {
                        dataManipulation.SentToMission(tmp[3], task);
                        ShowBasicGameInfos(dataManipulation.SuccessToSendToMission(tmp[3], task));
                    }

                    else
                    {
                        ChooseHeroForTask(task);
                    }
                    break;
                default:
                    ChooseHeroForTask(task);
                    break;
            }
        }
        public void ShowBasicGameInfos(string kuldetes)
        {
            ShowBasicHeroInfos();
            System.Console.WriteLine($"{kuldetes}\n*********");
            DisplayOptions();
        }



        //SZÖRNYEK MEGJELENÍTÉSE
        public void ShowAvailableMonsters()
        {
            List<int> randomMonsters = dataManipulation.GenerateRandomMonsters();

            System.Console.Clear();
            System.Console.WriteLine($"MONSTERS\n*********\n");
            var tmp = dataManipulation.GetEveryMonstersData();

            for (int i = 0; i < randomMonsters.Count; i++)
            {
                System.Console.WriteLine($"{i + 1}. {tmp[randomMonsters[i]].Name}");
            }

            System.Console.WriteLine($"\n0. Back");

            switch (System.Console.ReadKey(true).Key)
            {
                case ConsoleKey.D0:
                    ShowBasicGameInfos();
                    break;
                case ConsoleKey.D1:
                    ChooseHeroForCombat(tmp[randomMonsters[0]], randomMonsters[0]);
                    break;
                case ConsoleKey.D2:
                    ChooseHeroForCombat(tmp[randomMonsters[1]], randomMonsters[1]);
                    break;
                case ConsoleKey.D3:
                    ChooseHeroForCombat(tmp[randomMonsters[2]], randomMonsters[2]);
                    break;
                default:
                    ShowAvailableMonsters();
                    break;
            }
        }
        public void ChooseHeroForCombat(Monster monster, int idxOfTask)
        {
            dataManipulation.GenerateMonsterHealth(monster);

            System.Console.Clear();
            System.Console.WriteLine($"MONSTER: '{monster.Name}'\n" +
                $"*********\n\n" +
                $"Difficulty: {monster.Difficulty}\n" +
                $"Required Ability: {monster.RequiredAbility}\n" +
                $"Loot:\n" +
                $"   -> Weapons: {monster.Loot.Weapons}\n" +
                $"   -> Food: {monster.Loot.Food}\n" +
                $"   -> Water: {monster.Loot.Water}\n" +
                $"\n*********\n" +
                $"CHOOSE A HERO\n" +
                $"*********\n");

            var tmp = dataManipulation.GetEveryHeroData();

            for (int i = 0; i < tmp.Count; i++)
            {
                var item = tmp[i];

                System.Console.WriteLine($"{i + 1}. {item.Name} -> {dataManipulation.CheckIfHeroFitsForCombat(item, monster)}");
            }

            System.Console.WriteLine($"\n0. Back");

            switch (System.Console.ReadKey(true).Key)
            {
                case ConsoleKey.D0:
                    ShowAvailableMonsters();
                    break;
                case ConsoleKey.D1:
                    if (tmp[0].Status == HeroStatus.Active)
                    {
                        dataManipulation.SetSpecificHeroAbility(tmp[0], monster);
                        dataManipulation.HeroDamageToMonster(tmp[0], monster);
                        CombatConsole(tmp[0], monster);
                    }
                    else
                    {
                        ChooseHeroForCombat(monster, idxOfTask);
                    }
                    break;
                case ConsoleKey.D2:
                    if (tmp[1].Status == HeroStatus.Active)
                    {
                        dataManipulation.SetSpecificHeroAbility(tmp[1], monster);
                        dataManipulation.HeroDamageToMonster(tmp[1], monster);
                        CombatConsole(tmp[1], monster);
                    }
                    else
                    {
                        ChooseHeroForCombat(monster, idxOfTask);
                    }
                    break;
                case ConsoleKey.D3:
                    if (tmp[2].Status == HeroStatus.Active)
                    {
                        dataManipulation.SetSpecificHeroAbility(tmp[2], monster);
                        dataManipulation.HeroDamageToMonster(tmp[2], monster);
                        CombatConsole(tmp[2], monster);
                    }
                    else
                    {
                        ChooseHeroForCombat(monster, idxOfTask);
                    }
                    break;
                case ConsoleKey.D4:
                    if (tmp[3].Status == HeroStatus.Active)
                    {
                        dataManipulation.SetSpecificHeroAbility(tmp[3], monster);
                        dataManipulation.HeroDamageToMonster(tmp[3], monster);
                        CombatConsole(tmp[3], monster);
                    }
                    else
                    {
                        ChooseHeroForCombat(monster, idxOfTask);
                    }
                    break;
                default:
                    ChooseHeroForCombat(monster, idxOfTask);
                    break;
            }
        }
        public void CombatConsole(Hero hero, Monster monster)
        {
            int monsterHealth = dataManipulation.GetMonsterHealth();

            System.Console.Clear();
            System.Console.WriteLine($"'{hero.Name}' is fighting against '{monster.Name}'!\n" +
                    $"*********\n\n" +
                    $"Hero Health: {hero.Health}\n" +
                    $"Monster Health: {monsterHealth}\n\n" +
                    "What would you like to do?\n" +
                    "*********\n" +
                    $"1. Eat: '{hero.Resources.Food}' Food (+5 Health, -1 Food, -5 Hunger)\n" +
                    $"2. Drink: '{hero.Resources.Water}' Water (+3 Health, -1 Water, -5 Thirst)");

            System.Console.WriteLine($"{dataManipulation.CheckAbility()}\n" +
                $"\n0. Skip (Start next round)");

            // Felhasználói bemenet
            switch (System.Console.ReadKey(true).Key)
            {
                case ConsoleKey.D1:
                    System.Console.WriteLine(dataManipulation.EatCombat(hero));
                    System.Console.WriteLine("\nPress any key to continue...");
                    System.Console.ReadKey(true);
                    CombatConsole(hero, monster);
                    break;
                case ConsoleKey.D2:
                    System.Console.WriteLine(dataManipulation.DrinkCombat(hero));
                    System.Console.WriteLine("\nPress any key to continue...");
                    System.Console.ReadKey(true);
                    CombatConsole(hero, monster);
                    break;
                case ConsoleKey.D3:
                    System.Console.WriteLine(dataManipulation.UseAbility(hero, monster));
                    System.Console.WriteLine("\nPress any key to continue...");
                    System.Console.ReadKey(true);
                    if (dataManipulation.IsBattleFinished(hero, monster))
                    {
                        dataManipulation.HandleBattleOutcome(hero, monster);
                        BattleFinishedConsole(hero, monster);
                    }
                    else
                    {
                        CombatConsole(hero, monster);
                    }
                    break;
                case ConsoleKey.D0:
                    System.Console.WriteLine(dataManipulation.Attack(hero, monster));
                    dataManipulation.DecreaseCooldown();
                    System.Console.WriteLine("\nPress any key to continue...");
                    System.Console.ReadKey(true);
                    if (dataManipulation.IsBattleFinished(hero, monster))
                    {
                        dataManipulation.HandleBattleOutcome(hero, monster);
                        BattleFinishedConsole(hero, monster);
                    }
                    else
                    {
                        CombatConsole(hero, monster);
                    }
                    break;
                default:
                    CombatConsole(hero, monster);
                    break;
            }
        }
        public void BattleFinishedConsole(Hero hero, Monster monster)
        {
            System.Console.Clear();
            System.Console.WriteLine($"The game has ended.\n*********\n");

            if (hero.Health <= 0)
            {
                System.Console.WriteLine($"{monster.Name} defeated {hero.Name}!\n" +
                    $"\"The monster claims another victim...\"\n" +
                    $"Lost resources: {hero.Resources.Weapons} weapons, {hero.Resources.Food} food, {hero.Resources.Water} water, {hero.Resources.AlchemyIngredients} alchemy ingredients");
            }
            else
            {
                System.Console.WriteLine($"{hero.Name} defeated {monster.Name}!");
                System.Console.WriteLine($"Loot: +{monster.Loot.Weapons} weapons, +{monster.Loot.Food} food, +{monster.Loot.Water} water, +{monster.Loot.AlchemyIngredients} alchemy ingredients\n" +
                    $"{hero.Name}: \"It's done, no monsters left.\"");
            }

            dataManipulation.CheckIfGameFinished();

            System.Console.WriteLine("\n9. Back to Menu");

            switch (System.Console.ReadKey(true).Key)
            {
                case ConsoleKey.D9:
                    dataManipulation.UpdateHeroStatus(hero, monster);

                    if (dataManipulation.CheckIfGameIsRunning())
                    {
                        ShowBasicGameInfos();
                    }

                    else
                    {
                        ShowGameOver();
                    }

                    break;
                default:
                    BattleFinishedConsole(hero, monster);
                    break;
            }
        }



        //STORAGE MEGJELENÍTÉSE
        public void ShowResources()
        {
            System.Console.Clear();
            var tmp = dataManipulation.GetMainStorage();
            System.Console.WriteLine("RESOURCES:\n" +
                "*********\n\n" +
                $"Food: {tmp.Food}\n" +
                $"Water: {tmp.Water}\n" +
                $"Alchemy Ingredients: {tmp.AlchemyIngredients}\n" +
                $"Weapons: {tmp.Weapons}\n\n" +
                "0. Back");

            switch (System.Console.ReadKey(true).Key)
            {
                case ConsoleKey.D0:
                    ShowBasicGameInfos();
                    break;
                default:
                    ShowResources();
                    break;
            }
        }



        //JÁTÉK MEGÁLLÍTVA
        public void PauseGame()
        {
            System.Console.Clear();
            System.Console.WriteLine($"The Witcher: Console Kingdom\n\n" +
                $"Paused\n" +
                $"*********\n\n" +
                $"1. Continue\n" +
                $"2. Load Game\n" +
                $"3. Save Game\n" +
                $"0. Exit to Main Menu\n\n" +
                $"Choose an option using the 'listed' numbers on your keyboard");

            switch (System.Console.ReadKey(true).Key)
            {
                case ConsoleKey.D1:
                    ShowBasicGameInfos();
                    break;
                case ConsoleKey.D2:
                    PausedLoadGame();
                    break;
                case ConsoleKey.D3:
                    PausedSaveGame();
                    break;
                case ConsoleKey.D0:
                    ShowMainMenu();
                    break;
                default: PauseGame(); break;
            }
        }
        public void PausedLoadGame()
        {
            System.Console.Clear();
            System.Console.WriteLine($"Load Game\n*********\n");

            var files = dataManipulation.ReadFilesData();

            if (files.Length == 0)
            {
                for (int i = 1; i <= 5; i++)
                {
                    System.Console.WriteLine($" -> {i}. Empty");
                }

                System.Console.WriteLine($"\n0. Back to Main Menu");

                while (System.Console.ReadKey(true).Key != ConsoleKey.D0)
                {

                }

                PauseGame();
            }

            else
            {
                for (int i = 1; i <= files.Length; i++)
                {
                    System.Console.WriteLine($" -> {i}. {Path.GetFileName(files[i - 1])}");
                }

                for (int i = files.Length + 1; i <= 5; i++)
                {
                    System.Console.WriteLine($" -> {i}. Empty");
                }

                System.Console.WriteLine($"\n0. Back to Main Menu");

                if (files.Length == 5)
                {
                    switch (System.Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.D1:
                            dataManipulation.CreatingNewGame(files[0]);
                            ShowBasicGameInfos();
                            break;

                        case ConsoleKey.D2:
                            dataManipulation.CreatingNewGame(files[1]);
                            ShowBasicGameInfos();
                            break;

                        case ConsoleKey.D3:
                            dataManipulation.CreatingNewGame(files[2]);
                            ShowBasicGameInfos();
                            break;

                        case ConsoleKey.D4:
                            dataManipulation.CreatingNewGame(files[3]);
                            ShowBasicGameInfos();
                            break;

                        case ConsoleKey.D5:
                            dataManipulation.CreatingNewGame(files[4]);
                            ShowBasicGameInfos();
                            break;

                        case ConsoleKey.D0:
                            PauseGame();
                            break;
                        default: PausedLoadGame(); break;
                    }
                }
                if (files.Length == 4)
                {
                    switch (System.Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.D1:
                            dataManipulation.CreatingNewGame(files[0]);
                            ShowBasicGameInfos();
                            break;

                        case ConsoleKey.D2:
                            dataManipulation.CreatingNewGame(files[1]);
                            ShowBasicGameInfos();
                            break;

                        case ConsoleKey.D3:
                            dataManipulation.CreatingNewGame(files[2]);
                            ShowBasicGameInfos();
                            break;

                        case ConsoleKey.D4:
                            dataManipulation.CreatingNewGame(files[3]);
                            ShowBasicGameInfos();
                            break;

                        case ConsoleKey.D0:
                            PauseGame();
                            break;
                        default: PausedLoadGame(); break;
                    }
                }
                if (files.Length == 3)
                {
                    switch (System.Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.D1:
                            dataManipulation.CreatingNewGame(files[0]);
                            ShowBasicGameInfos();
                            break;

                        case ConsoleKey.D2:
                            dataManipulation.CreatingNewGame(files[1]);
                            ShowBasicGameInfos();
                            break;

                        case ConsoleKey.D3:
                            dataManipulation.CreatingNewGame(files[2]);
                            ShowBasicGameInfos();
                            break;

                        case ConsoleKey.D0:
                            PauseGame();
                            break;
                        default: PausedLoadGame(); break;
                    }
                }
                if (files.Length == 2)
                {
                    switch (System.Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.D1:
                            dataManipulation.CreatingNewGame(files[0]);
                            ShowBasicGameInfos();
                            break;

                        case ConsoleKey.D2:
                            dataManipulation.CreatingNewGame(files[1]);
                            ShowBasicGameInfos();
                            break;

                        case ConsoleKey.D0:
                            PauseGame();
                            break;
                        default: PausedLoadGame(); break;
                    }
                }
                if (files.Length == 1)
                {
                    switch (System.Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.D1:
                            dataManipulation.CreatingNewGame(files[0]);
                            ShowBasicGameInfos();
                            break;

                        case ConsoleKey.D0:
                            PauseGame();
                            break;
                        default: PausedLoadGame(); break;
                    }
                }
            }
        }
        public void PausedSaveGame()
        {
            System.Console.Clear();
            System.Console.WriteLine($"Save Game\n\"*********\n");

            var files = dataManipulation.ReadFilesData();

            if (files.Length == 0)
            {
                for (int i = 1; i <= 5; i++)
                {
                    System.Console.WriteLine($" -> {i}. Empty");
                }

                System.Console.WriteLine($"\n0. Back to Main Menu");
            }

            else
            {
                for (int i = 1; i <= files.Length; i++)
                {
                    System.Console.WriteLine($" -> {i}. {dataManipulation.GetFilesName(files[i - 1])}");
                }

                for (int i = files.Length + 1; i <= 5; i++)
                {
                    System.Console.WriteLine($" -> {i}. Empty");
                }

                System.Console.WriteLine($"\n0. Back to Main Menu");
            }

            switch (System.Console.ReadKey(true).Key)
            {
                case ConsoleKey.D1:
                    dataManipulation.SaveGame(1);
                    PausedSaveGame();
                    break;
                case ConsoleKey.D2:
                    dataManipulation.SaveGame(2);
                    PausedSaveGame();
                    break;
                case ConsoleKey.D3:
                    dataManipulation.SaveGame(3);
                    PausedSaveGame();
                    break;
                case ConsoleKey.D4:
                    dataManipulation.SaveGame(4);
                    PausedSaveGame();
                    break;
                case ConsoleKey.D5:
                    dataManipulation.SaveGame(5);
                    PausedSaveGame();
                    break;
                case ConsoleKey.D0:
                    PauseGame();
                    break;
                default: PausedSaveGame(); break;
            }
        }



        //JÁTÉK VÉGE
        public void ShowGameCompleted()
        {
            System.Console.Clear();
            System.Console.WriteLine($"You are victorious\n" +
                $"*********\n\n" +
                $"After enduring '{dataManipulation.GetDaysLimit()}' days of trials and tribulations, you've proven yourself as a true Witcher.\n" +
                $"Monsters vanquished, contracts fulfilled, and tales of your deeds spreading across the Continent.\n" +
                $"Your name will be sung in taverns, whispered by merchants, and feared by creatures of the night.\n" +
                $"\n[WARNING: Geralt's Approval System Engaged!]\n" +
                $"Congratulations, Witcher! You have conquered the challenges and forged your legend. Victory is yours!\n\n" +
                $"Press 'space' to return to the menu.");

            switch (System.Console.ReadKey(true).Key)
            {
                case ConsoleKey.Spacebar:
                    dataManipulation.SetIsGameRunning(true);
                    ShowMainMenu();
                    break;
                default:
                    ShowGameCompleted();
                    break;
            }
        }
        public void ShowGameOver()
        {
            System.Console.Clear();
            System.Console.WriteLine($"Game Over\n" +
                $"*********\n\n" +
                $"All heroes have perished.\n" +
                $"Days survived: {dataManipulation.GetCurrentDay()}\n\n" +
                $"Press 'space' to return to the menu.");

            switch (System.Console.ReadKey(true).Key)
            {
                case ConsoleKey.Spacebar:
                    dataManipulation.SetIsGameRunning(true);
                    ShowMainMenu();
                    break;
                default:
                    ShowGameOver();
                    break;
            }
        }
    }
}