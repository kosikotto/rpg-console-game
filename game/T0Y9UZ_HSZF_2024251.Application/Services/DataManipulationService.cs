using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T0Y9UZ_HSZF_2024251.Model.Exceptions;
using T0Y9UZ_HSZF_2024251.Application.contracts;
using T0Y9UZ_HSZF_2024251.Model.Entities;
using T0Y9UZ_HSZF_2024251.Model.Types;
using T0Y9UZ_HSZF_2024251.Persistence.MsSql;
using T0Y9UZ_HSZF_2024251.Persistence.MsSql.Contracts;
using T0Y9UZ_HSZF_2024251.Persistence.MsSql.Dataproviders;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace T0Y9UZ_HSZF_2024251.Application.services
{


    public class DataManipulationService : IDataManipulationService
    {
        public Data data { get; private set; }
        public IHeroRepositry heroDataProvider { get; private set; }
        public ITaskRepository gameTaskDataProvider { get; private set; }
        public IMonsterRepository monsterDataProvider { get; private set; }
        public IStorageRepository storageDataProvider { get; private set; }
        public IDaysRepository daysDataProvider { get; private set; }
        public IFileHandlerService fileHandler { get; private set; }

        public WitcherDbContext ctx { get; private set; }

        private Random randomDmg;
        public int currentMonsterHealth { get; private set; }
        public bool heroAbilitySuitable { get; private set; }
        private int maxDmgByHero;
        private int abilityDmgByHero;
        private int abilityCooldown;

        public List<string> abilities { get; private set; }
        public int currentAbilityIndex { get; private set; }
        public bool IsGameRunning { get; private set; }
        public int DaysLimit { get; private set; }

        public DataManipulationService(
            IHeroRepositry heroDataProvider,
            ITaskRepository gameTaskDataProvider,
            IMonsterRepository monsterDataProvider,
            IStorageRepository storageDataProvider,
            IDaysRepository daysDataProvider,
            IFileHandlerService fileHandler)
        {

            this.heroDataProvider = heroDataProvider;
            this.gameTaskDataProvider = gameTaskDataProvider;
            this.monsterDataProvider = monsterDataProvider;
            this.storageDataProvider = storageDataProvider;
            this.daysDataProvider = daysDataProvider;
            this.fileHandler = fileHandler;

            this.randomDmg = new Random();
            this.heroAbilitySuitable = false;

            this.data = new Data();
            this.abilities = new List<string> { "combat", "alchemy", "sorcery", "healing", "tracking" };
            this.currentAbilityIndex = -1;
            this.IsGameRunning = true;
            this.DaysLimit = 100;
        }

        //FŐMENÜRENDSZER
        public void CreatingNewGame(string file)
        {
            ctx = new WitcherDbContext();

            heroDataProvider = new HeroDataProvider(ctx);
            gameTaskDataProvider = new GameTaskDataProvider(ctx);
            monsterDataProvider = new MonsterDataProvider(ctx);
            storageDataProvider = new StorageDataProvider(ctx);
            daysDataProvider = new DaysDataProvider(ctx);
            ctx.SaveChanges();

            try
            {
                data = fileHandler.JsonReader(file);
            } catch(FileNotFoundException e)
            {
                throw new LoadingFileNotFound($"\nNem található a '{file}' fájl.\n\n {e}");
            }

            Load(data);
        }
        public void Load(Data data)
        {
            foreach (var item in data.Heroes)
            {
                heroDataProvider.Add(item);
            }

            foreach (var item in data.Monsters)
            {
                monsterDataProvider.Add(item);
            }

            foreach (var item in data.Tasks)
            {
                gameTaskDataProvider.Add(item);
            }

            foreach (var item in data.Resources)
            {
                storageDataProvider.Add(item);
            }

            DaysPassed days = new DaysPassed();
            days.currentDay = data.Days;

            daysDataProvider.Add(days);
            ctx.SaveChanges();
        }
        public List<Hero> GetEveryHeroData()
        {
            return heroDataProvider.GetAll();
        }
        public string CheckNewGameAbilityIndex(List<Hero> tmp)
        {
            string returnString = "";
            if (currentAbilityIndex != -1)
            {
                string currentAbility = abilities[currentAbilityIndex];
                var filteredHeroes = tmp.Where(h => h.Abilities.Contains(currentAbility)).ToList();

                returnString += ($"Heroes with the ability '{currentAbility}':");
                if (filteredHeroes.Any())
                {
                    foreach (var hero in filteredHeroes)
                    {
                        returnString += ($"  -> {hero.Name}");
                    }
                }
                else
                {
                    returnString += ("  -> No heroes with this ability.");
                }
            }

            return returnString;
        }
        public void CheckNewGameAbilityIndexIncrease()
        {
            currentAbilityIndex++;
            if (currentAbilityIndex == 5)
            {
                currentAbilityIndex = 0;
            }
        }
        public void CheckNewGameAbilityIndexReset()
        {
            currentAbilityIndex = -1;
        }
        public void TeamBuildUpRemoveHero(ICollection<Hero> heroes, Hero hero)
        {
            heroes.Remove(hero);
        }
        public void TeamBuildUpAddHero(ICollection<Hero> heroes, Hero hero)
        {
            heroes.Add(hero);
        }
        public void TeamBuildCompleted(List<Hero> tmp, ICollection<Hero> heroes)
        {
            foreach (var item in tmp)
            {
                heroDataProvider.Delete(item);
                data.Heroes.Remove(item);
            }

            foreach (var item in heroes)
            {
                heroDataProvider.Add(new Hero()
                {
                    Name = item.Name,
                    Status = item.Status,
                    HealthStatus = item.HealthStatus,
                    Health = item.Health,
                    Hunger = item.Hunger,
                    Thirst = item.Thirst,
                    Fatigue = item.Fatigue,
                    Abilities = item.Abilities,
                    Resources = item.Resources,
                    DaysLeft = item.DaysLeft,
                    Tasks = item.Tasks,
                    DefeatedMonsters = item.DefeatedMonsters,
                    CompletedTasks = item.CompletedTasks
                });

                data.Heroes.Add(item);
            }
        }
        public string[] ReadFilesData()
        {
            string path = "save/save_files";
            string[] files = Directory.GetFiles(path);
            return files;
        }
        public string GetFilesName(string file)
        {
            return Path.GetFileName(file);
        }
        public void SaveGame(int idx)
        {
            OverrideSave("save/save_files", $"GameSave_0{idx}");
            data.Heroes = GetEveryHeroData();
            data.Days = GetCurrentDay();

            fileHandler.JsonWriter(data, Path.Combine("save/save_files", $"GameSave_0{idx} - {DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}-{DateTime.Now.Hour:D2}-{DateTime.Now.Minute:D2}-{DateTime.Now.Second:D2}.json"));
        }
        public void OverrideSave(string path, string fileName)
        {
            string[] files = Directory.GetFiles(path);
            foreach (var item in files)
            {
                if (Path.GetFileName(item).Split("-")[0].Contains(fileName))
                {
                    File.Delete(item);
                }
            }
        }
        public string ExitGame()
        {
            IsGameRunning = false;
            return $"Exiting game...";
        }



        //INNENTŐL JÖN MAGA A JÁTÉKMENET
        //ELOSZTÓ MENÜ
        public string HeroOnAdventure(Hero hero)
        {
            return $"Hero: {hero.Name} " +
                            $" -> Status: {hero.Status}(Days: {hero.DaysLeft}) " +
                            $" -> Health: {hero.HealthStatus}({hero.Health}) " +
                            $" -> Hunger: {hero.Hunger} " +
                            $" -> Thirst: {hero.Thirst} " +
                            $" -> Fatigue: {hero.Fatigue} ";
        }
        public string HeroFinishedAdventure(Hero hero)
        {
            return $"Hero: {hero.Name} " +
                                    $" -> Status: {hero.Status} " +
                                    $" -> Health: {hero.HealthStatus}({hero.Health}) " +
                                    $" -> Hunger: {hero.Hunger} " +
                                    $" -> Thirst: {hero.Thirst} " +
                                    $" -> Fatigue: {hero.Fatigue} ";
        }
        public string HeroTaskWasTaken(Hero hero)
        {
            return $"\nThe '{hero.Tasks[0].Name}' task was successfully completed!\n" +
                                    $"'{hero.Name}' suffered: \n" +
                                    $" -> Health: {hero.Tasks[0].AffectedStatus.Health}\n" +
                                    $" -> Hunger: {hero.Tasks[0].AffectedStatus.Hunger}\n" +
                                    $" -> Thirst: {hero.Tasks[0].AffectedStatus.Thirst}\n" +
                                    $" -> Fatigue: {hero.Tasks[0].AffectedStatus.Fatigue}";
        }
        public void HeroCompletedTask(Hero hero)
        {
            hero.Status = HeroStatus.Active;
            hero.Hunger = Math.Max(hero.Hunger + hero.Tasks[0].AffectedStatus.Hunger, 0);
            hero.Thirst = Math.Max(hero.Thirst + hero.Tasks[0].AffectedStatus.Thirst, 0);
            hero.Fatigue = Math.Max(hero.Fatigue + hero.Tasks[0].AffectedStatus.Fatigue, 0);
        }
        public void HeroPassedAwayDuringAdventure(Hero hero)
        {
            hero.Status = HeroStatus.Dead;
            hero.HealthStatus = HealthStatus.Ill;

            hero.Resources.Food = 0;
            hero.Resources.Water = 0;
            hero.Resources.Weapons = 0;
            hero.Resources.AlchemyIngredients = 0;
        }
        public string GetBasicHeroInfos()
        {
            string formattedString = "";
            var tmp = GetEveryHeroData();
            foreach (var item in tmp)
            {
                if (item.Status == HeroStatus.Adventure)
                {
                    if (item.DaysLeft > 0)
                    {
                        formattedString += HeroOnAdventure(item);
                    }

                    else
                    {
                        if (item.DaysLeft == 0)
                        {
                            item.Health = Math.Clamp(item.Health + item.Tasks[0].AffectedStatus.Health, 0, 100);

                            if (item.Health > 0)
                            {
                                HeroCompletedTask(item);

                                if (item.Health >= 80)
                                {
                                    item.HealthStatus = HealthStatus.Healthy;
                                }

                                if (item.Health >= 50 && item.Health < 80)
                                {
                                    item.HealthStatus = HealthStatus.Injured;
                                }

                                if (item.Health < 50)
                                {
                                    item.HealthStatus = HealthStatus.Ill;
                                }

                                formattedString += HeroFinishedAdventure(item);


                                formattedString += HeroTaskWasTaken(item);


                                item.CompletedTasks.Add(item.Tasks[0].Name);

                                item.Tasks.Remove(item.Tasks[0]);
                            }

                            else
                            {
                                HeroPassedAwayDuringAdventure(item);

                                formattedString += HeroFinishedAdventure(item);

                                formattedString += ($"\nThe '{item.Tasks[0].Name}' task failed, '{item.Name}' has died.");
                            }
                        }
                    }
                }

                else if (item.Status == HeroStatus.Recovery && item.Hunger == 100 || item.Thirst == 100)
                {
                    formattedString += HeroOnAdventure(item);
                }

                else
                {
                    formattedString += HeroFinishedAdventure(item);
                }
                heroDataProvider.Update(item);
                formattedString += "\n";
            }

            return formattedString;
        }

        public List<string> FormatHerosOnMission()
        {
            return heroDataProvider.GetFormattedHerosOnMissionData();
        }




        //HŐS MANIPULÁLÁS
        public string SendToRecovery(Hero hero)
        {
            string pihenoreKuldKiszed = "";
            if (hero.Status != HeroStatus.Dead)
            {
                if (hero.Status == HeroStatus.Active)
                {
                    pihenoreKuldKiszed = "Send to recovery";
                }
                else if (hero.Status == HeroStatus.Recovery)
                {
                    pihenoreKuldKiszed = "Remove from recovery";
                }
                else
                {
                    pihenoreKuldKiszed = "Not available";
                }
            }
            return pihenoreKuldKiszed;
        }
        public GameTask GetTaskFromHero(Hero hero)
        {
            return hero.Tasks[0];
        }
        public void AbortMission(Hero hero, GameTask task)
        {
            if (hero.Status == HeroStatus.Adventure)
            {
                hero.Status = HeroStatus.Active;

                hero.DaysLeft = 0;

                hero.Resources.Food += (int)Math.Round(task.RequiredResources.Food / 2.0);
                hero.Resources.Water += (int)Math.Round(task.RequiredResources.Water / 2.0);
                hero.Resources.Weapons += (int)Math.Round(task.RequiredResources.Weapons / 2.0);

                hero.Hunger += 3;
                hero.Thirst += 3;
                hero.Fatigue += 3;

                hero.Tasks.Remove(task);
                heroDataProvider.Update(hero);
            }
        }
        public void Eat(Hero hero)
        {
            if (hero.Resources.Food > 0 && hero.Hunger > 0)
            {
                hero.Resources.Food--;
                hero.Hunger = Math.Max(hero.Hunger - 5, 0);
                heroDataProvider.Update(hero);
            }
        }
        public void Drink(Hero hero)
        {
            if (hero.Resources.Water > 0 && hero.Thirst > 0)
            {
                hero.Resources.Water--;
                hero.Thirst = Math.Max(hero.Thirst - 5, 0);
                heroDataProvider.Update(hero);
            }
        }
        public void Recovery(Hero hero)
        {
            if (hero.Status == HeroStatus.Active)
            {
                hero.Status = HeroStatus.Recovery;
                heroDataProvider.Update(hero);
            }

            else if (hero.Status == HeroStatus.Recovery)
            {
                hero.Status = HeroStatus.Active;
                ResetHeroDaysLeft(hero);
                heroDataProvider.Update(hero);
            }
        }
        public void ResetHeroDaysLeft(Hero hero)
        {
            if (hero.Hunger < 100 && hero.Thirst < 100)
            {
                hero.DaysLeft = 0;
            }
        }
        public void FoodPutGetIntoFromMainStorage(Hero hero, Resources storage, string pluszOrMinusz)
        {
            if (pluszOrMinusz == "-")
            {
                hero.Resources.Food--;
                storage.Food++;
                heroDataProvider.Update(hero);
                storageDataProvider.Update(storage);
            }

            if (pluszOrMinusz == "+")
            {
                hero.Resources.Food++;
                storage.Food--;
                heroDataProvider.Update(hero);
                storageDataProvider.Update(storage);
            }

        }
        public void WaterPutGetIntoFromMainStorage(Hero hero, Resources storage, string pluszOrMinusz)
        {
            if (pluszOrMinusz == "-")
            {
                hero.Resources.Water--;
                storage.Water++;
                heroDataProvider.Update(hero);
                storageDataProvider.Update(storage);
            }

            if (pluszOrMinusz == "+")
            {
                hero.Resources.Water++;
                storage.Water--;
                heroDataProvider.Update(hero);
                storageDataProvider.Update(storage);
            }
        }
        public void AlchemyIngredientPutGetIntoFromMainStorage(Hero hero, Resources storage, string pluszOrMinusz)
        {
            if (pluszOrMinusz == "-")
            {
                hero.Resources.AlchemyIngredients--;
                storage.AlchemyIngredients++;
                heroDataProvider.Update(hero);
                storageDataProvider.Update(storage);
            }

            if (pluszOrMinusz == "+")
            {
                hero.Resources.AlchemyIngredients++;
                storage.AlchemyIngredients--;
                heroDataProvider.Update(hero);
                storageDataProvider.Update(storage);
            }
        }
        public void WeaponPutGetIntoFromMainStorage(Hero hero, Resources storage, string pluszOrMinusz)
        {
            if (pluszOrMinusz == "-")
            {
                hero.Resources.Weapons--;
                storage.Weapons++;
                heroDataProvider.Update(hero);
                storageDataProvider.Update(storage);
            }

            if (pluszOrMinusz == "+")
            {
                hero.Resources.Weapons++;
                storage.Weapons--;
                heroDataProvider.Update(hero);
                storageDataProvider.Update(storage);
            }
        }




        //TASKRA KÜLDÉS

        //EZ
        public List<GameTask> GetEveryTaskData()
        {
            return gameTaskDataProvider.GetAll();
        }
        public List<int> GenerateRandomTasks()
        {
            List<int> randomTaskIndices = new List<int>();

            var heroes = heroDataProvider.GetAll();
            var tasks = gameTaskDataProvider.GetAll();

            var takenTaskIds = heroes
                .SelectMany(hero => hero.Tasks)
                .Select(task => task.Id)
                .ToHashSet();

            var availableTaskIndices = tasks
                .Select((task, index) => new { task, index })
                .Where(x => !takenTaskIds.Contains(x.task.Id))
                .Select(x => x.index)
                .ToList();

            while (randomTaskIndices.Count < 3)
            {
                var randomIndex = randomDmg.Next(availableTaskIndices.Count);
                var selectedTaskIndex = availableTaskIndices[randomIndex];

                if (!randomTaskIndices.Contains(selectedTaskIndex))
                {
                    randomTaskIndices.Add(selectedTaskIndex);
                }
            }

            return randomTaskIndices;
        }
        public string CheckIfHeroFits(Hero hero, GameTask task)
        {
            if (hero.Status == HeroStatus.Active)
            {
                if (hero.Resources.Food >= task.RequiredResources.Food &&
                    hero.Resources.Water >= task.RequiredResources.Water &&
                    hero.Resources.Weapons >= task.RequiredResources.Weapons)
                {
                    return "Available -> Recommended";
                }
                else
                {
                    return "Available -> Does not meet requirements";
                }
            }

            if (hero.Status == HeroStatus.Adventure)
            {
                return "Unavailable -> On an adventure";
            }

            if (hero.Status == HeroStatus.Recovery)
            {
                return "Unavailable -> Resting";
            }

            if (hero.Status == HeroStatus.Dead)
            {
                return "Unavailable -> Dead";
            }

            return "Unavailable";
        }
        public bool CheckIfHeroFitsForTask(HeroStatus status, string ifFits)
        {
            if (status == HeroStatus.Active && ifFits.Equals("Available -> Recommended"))
            {
                return true;
            }

            return false;
        }
        public string SuccessToSendToMission(Hero hero, GameTask task)
        {
            return $"'{hero.Name}' has been successfully sent to the '{task.Name}' mission.";
        }
        public void SentToMission(Hero hero, GameTask task)
        {
            hero.Status = HeroStatus.Adventure;
            hero.Tasks.Add(task);
            hero.DaysLeft = task.Duration;

            hero.Resources.Food -= task.RequiredResources.Food;
            hero.Resources.Water -= task.RequiredResources.Water;
            hero.Resources.Weapons -= task.RequiredResources.Weapons;
            heroDataProvider.Update(hero);
        }



        //SZÖRNYVADÁSZAT
        public List<int> GenerateRandomMonsters()
        {
            List<int> randomMonsters = new List<int>();

            while (randomMonsters.Count < 3)
            {
                bool talalat = false;
                int generated = randomDmg.Next(0, 10);
                foreach (var item in randomMonsters)
                {
                    if (item == generated)
                    {
                        talalat = true;
                        break;
                    }
                }

                if (!talalat)
                {
                    randomMonsters.Add(generated);
                }
            }

            return randomMonsters;
        }
        public List<Monster> GetEveryMonstersData()
        {
            return monsterDataProvider.GetAll();
        }
        public string CheckIfHeroFitsForCombat(Hero hero, Monster monster)
        {
            if (hero.Status == HeroStatus.Adventure)
            {
                return "On an adventure, cannot fight";
            }

            if (hero.Status == HeroStatus.Recovery)
            {
                return "Resting, cannot fight";
            }

            if (hero.Status == HeroStatus.Dead)
            {
                return "Dead, cannot fight";
            }

            if (hero.Status == HeroStatus.Active)
            {
                foreach (var ability in hero.Abilities)
                {
                    if (ability == monster.RequiredAbility)
                    {
                        return "Recommended";
                    }
                }

                SetHeroAbilitySuitable(false);
                return "Not recommended, ability is not suitable";
            }

            return "Unknown status, cannot fight";
        }
        public void SetSpecificHeroAbility(Hero hero, Monster monster)
        {
            if (hero.Abilities.Contains(monster.RequiredAbility))
            {
                abilityCooldown = 0;
                SetHeroAbilitySuitable(true);
            }

            else
            {
                SetHeroAbilitySuitable(false);
            }
        }
        public void SetHeroAbilitySuitable(bool ability)
        {
            heroAbilitySuitable = ability;
        }
        public bool GetHeroAbilitySuitable()
        {
            return heroAbilitySuitable;
        }
        public int GetAbilityCooldown()
        {
            return abilityCooldown;
        }
        public void SetCooldown()
        {
            abilityCooldown = randomDmg.Next(1, 6);
        }
        public void DecreaseCooldown()
        {
            if (GetAbilityCooldown() > 0)
            {
                abilityCooldown--;
            }
        }
        public int GenerateMonsterHealth(Monster monster)
        {
            switch (monster.Difficulty)
            {
                case 1: currentMonsterHealth = 65; break;
                case 2: currentMonsterHealth = 100; break;
                case 3: currentMonsterHealth = 135; break;
                case 4: currentMonsterHealth = 170; break;
                default: currentMonsterHealth = 100; break;
            }

            return currentMonsterHealth;
        }
        public int GetMonsterHealth()
        {
            return currentMonsterHealth;
        }
        public string EatCombat(Hero hero)
        {
            if (hero.Resources.Food > 0)
            {
                if (hero.Health < 100)
                {
                    HeroAteDuringCombat(hero);
                    return $"\n'{hero.Name}' ate. Health: {hero.Health}";
                }
                else
                {
                    return $"\nThe hero's health is 100%, they cannot eat.";
                }
            }

            else
            {
                return $"\nNo food left!";
            }
        }
        public void HeroAteDuringCombat(Hero hero)
        {
            hero.Health = Math.Min(hero.Health + 5, 100);
            hero.Resources.Food--;
            heroDataProvider.Update(hero);
        }
        public string DrinkCombat(Hero hero)
        {
            if (hero.Resources.Water > 0)
            {
                if (hero.Health < 100)
                {
                    HeroDrankDuringCombat(hero);
                    return $"\n'{hero.Name}' drank. Health: {hero.Health}";
                }
                else
                {
                    return $"\nThe hero's health is 100%, they cannot drink.";
                }
            }

            else
            {
                return $"\nNo water left!";
            }
        }
        public void HeroDrankDuringCombat(Hero hero)
        {
            hero.Health = Math.Min(hero.Health + 3, 100);
            hero.Resources.Water--;
            heroDataProvider.Update(hero);
        }
        public void HeroDamageToMonster(Hero hero, Monster monster)
        {
            if (hero.Abilities.Contains(monster.RequiredAbility))
            {
                maxDmgByHero = 17;
                abilityDmgByHero = 20;
            }

            else
            {
                maxDmgByHero = 10;
                abilityDmgByHero = 0;
            }
        }
        public string CheckAbility()
        {
            if (GetHeroAbilitySuitable())
            {
                int cooldown = GetAbilityCooldown();
                if (cooldown > 0)
                {
                    return $"3. Ability not available for '{cooldown} rounds'";
                }

                else
                {
                    return $"3. Use Ability";
                }
            }

            else
            {
                return "3. Ability not available";
            }
        }
        public string UseAbility(Hero hero, Monster monster)
        {
            if (GetHeroAbilitySuitable())
            {
                if (GetAbilityCooldown() == 0)
                {
                    int dmgWithAbility = randomDmg.Next(14, abilityDmgByHero);
                    currentMonsterHealth -= dmgWithAbility;
                    SetCooldown();
                    return $"\n'{hero.Name}' used special ability: '{dmgWithAbility}' damage dealt.";
                }

                else
                {
                    return $"\nAbility not available: '{GetAbilityCooldown()}' rounds remaining.";
                }
            }

            else
            {
                return $"\nAbility not available, you can't use ability against '{monster.Name}'";
            }
        }
        public string Attack(Hero hero, Monster monster)
        {
            int heroDamage = randomDmg.Next(0, maxDmgByHero);
            int monsterDamage = randomDmg.Next(0, 17);

            currentMonsterHealth = Math.Max(currentMonsterHealth - heroDamage, 0);
            hero.Health = Math.Max(hero.Health - monsterDamage, 0);

            return $"\n'{hero.Name}' dealt '{heroDamage}' damage to '{monster.Name}'! '{monster.Name}' Health: '{GetMonsterHealth()}'\n" +
                $"'{monster.Name}' dealt '{monsterDamage}' damage to '{hero.Name}'! '{hero.Name}' Health: '{hero.Health}'";
        }
        public bool IsBattleFinished(Hero hero, Monster monster)
        {
            return hero.Health <= 0 || currentMonsterHealth <= 0;
        }
        public void UpdateHeroStatus(Hero hero, Monster monster)
        {
            if (hero.Health > 0)
            {
                hero.DefeatedMonsters.Add(monster.Name);
            }

            if (hero.Health >= 80)
            {
                hero.HealthStatus = HealthStatus.Healthy;
            }

            if (hero.Health >= 50 && hero.Health < 80)
            {
                hero.HealthStatus = HealthStatus.Injured;
            }

            if (hero.Health < 50)
            {
                hero.HealthStatus = HealthStatus.Ill;
            }

            heroDataProvider.Update(hero);
        }
        public void HandleBattleOutcome(Hero hero, Monster monster)
        {
            if (hero.Health > 0)
            {
                hero.Resources.Weapons += monster.Loot.Weapons;
                hero.Resources.Food += monster.Loot.Food;
                hero.Resources.Water += monster.Loot.Water;
                hero.Resources.AlchemyIngredients += monster.Loot.AlchemyIngredients;
            }
            else
            {
                hero.Resources.Weapons = 0;
                hero.Resources.Food = 0;
                hero.Resources.Water = 0;
                hero.Resources.AlchemyIngredients = 0;
                hero.Status = HeroStatus.Dead;
            }
        }



        //STORAGE
        public Resources GetMainStorage()
        {
            return storageDataProvider.GetMainStorage();
        }
        public List<Resources> GetEveryResourcesData()
        {
            return storageDataProvider.GetAll();
        }



        //NAP LOGIKA
        public void SetIsGameRunning(bool gameStatus)
        {
            IsGameRunning = gameStatus;
        }
        public bool CheckIfGameIsRunning()
        {
            return IsGameRunning;
        }
        public void NextDay()
        {
            var tmp = GetCurrentDay();
            var heroes = GetEveryHeroData();
            daysDataProvider.Update(new DaysPassed() { currentDay = tmp + 1 });

            UpdateHeroesDaily(heroes, tmp);
            HandleAutoCare(heroes);
            HandleRecovery(heroes);
        }
        public void UpdateHeroesDaily(List<Hero> heroes, int currentDay)
        {
            foreach (var hero in heroes)
            {
                fileHandler.CreateDailyReport(currentDay, hero);
                if (hero.Status == HeroStatus.Active)
                {
                    hero.Hunger = Math.Min(hero.Hunger + 5, 100);
                    hero.Thirst = Math.Min(hero.Thirst + 5, 100);
                    hero.Fatigue = Math.Min(hero.Fatigue + 5, 100);
                }

                if (hero.Status == HeroStatus.Adventure && hero.DaysLeft > 0)
                {
                    hero.DaysLeft = Math.Max(hero.DaysLeft - 1, 0);
                }
                heroDataProvider.Update(hero);
            }
        }
        public void HandleAutoCare(ICollection<Hero> heroes)
        {
            foreach (var hero in heroes)
            {
                if (hero.Status == HeroStatus.Active &&
                    (hero.Hunger == 100 || hero.Thirst == 100 || hero.Fatigue == 100))
                {
                    hero.Status = HeroStatus.Recovery;
                }

                if (hero.Status == HeroStatus.Recovery &&
                    hero.Hunger == 0 && hero.Thirst == 0 && hero.Fatigue == 0)
                {
                    hero.Status = HeroStatus.Active;
                }

                heroDataProvider.Update(hero);
            }
        }
        public void HandleRecovery(ICollection<Hero> heroes)
        {
            foreach (var hero in heroes)
            {
                if (hero.Status == HeroStatus.Recovery)
                {
                    hero.Fatigue = Math.Max(hero.Fatigue - randomDmg.Next(0, 6), 0);
                    hero.Health = Math.Min(hero.Health + randomDmg.Next(0, 4), 100);

                    if (hero.Thirst == 100 || hero.Hunger == 100)
                    {
                        hero.DaysLeft++;
                        if (hero.DaysLeft >= 3)
                        {
                            MarkHeroAsDeceased(hero);
                        }
                    }
                    else
                    {
                        hero.DaysLeft = 0;
                        heroDataProvider.Update(hero);
                    }
                }
            }
        }
        public void MarkHeroAsDeceased(Hero hero)
        {
            hero.Status = HeroStatus.Dead;
            hero.DaysLeft = 0;
            hero.Resources.Food = 0;
            hero.Resources.Water = 0;
            hero.Resources.Weapons = 0;
            hero.Resources.AlchemyIngredients = 0;

            heroDataProvider.Update(hero);
        }
        public bool DaysLimitReached()
        {
            if (GetCurrentDay() == GetDaysLimit())
            {
                return true;
            }
            return false;
        }
        public bool CheckIfEveryHeroPassedAway()
        {
            foreach (var item in GetEveryHeroData())
            {
                if (item.Status != HeroStatus.Dead)
                {
                    return false;
                }
            }

            return true;
        }
        public void CheckIfGameFinished()
        {
            if (DaysLimitReached())
            {
                SetIsGameRunning(false);
            }
            else if (CheckIfEveryHeroPassedAway())
            {
                SetIsGameRunning(false);
            }
            else
            {
                SetIsGameRunning(true);
            }
        }
        public int GetDaysLimit()
        {
            return DaysLimit;
        }
        public int GetCurrentDay()
        {
            return daysDataProvider.GetCurrentDay();
        }
    }
}