using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T0Y9UZ_HSZF_2024251.Application.contracts;
using T0Y9UZ_HSZF_2024251.Application.services;
using T0Y9UZ_HSZF_2024251.Model.Entities;
using T0Y9UZ_HSZF_2024251.Model.Types;
using T0Y9UZ_HSZF_2024251.Persistence.MsSql;
using T0Y9UZ_HSZF_2024251.Persistence.MsSql.Contracts;
using T0Y9UZ_HSZF_2024251.Persistence.MsSql.Dataproviders;

namespace T0Y9UZ_HSZF_2024251.Test
{
    public class FakeTaskDataprovider : ITaskRepository
    {
        private List<Task> tasks = new List<Task>();
        List<GameTask> ITaskRepository.GetAll()
        {
            return new List<GameTask>();
        }
        public void Add(GameTask GameTask)
        {
            
        }
    }

    public class FakeMonsterRepository : IMonsterRepository
    {
        public void Add(Monster monster)
        {

        }

        public List<Monster> GetAll()
        {
            return new List<Monster>();
        }
    }

    public class FakeStorageRepository : IStorageRepository
    {
        public void Add(Resources resources)
        {
            
        }

        public List<Resources> GetAll()
        {
            return new List<Resources>();
        }

        public Resources GetMainStorage()
        {
            return new Resources();
        }

        public void Update(Resources resources)
        {
            
        }
    }

    public class FakeFileHandlerService : IFileHandlerService
    {
        public void CreateDailyReport(int day, Hero hero)
        {
            
        }

        public Data JsonReader(string path)
        {
            return new Data();
        }

        public void JsonWriter(Data gameData, string path)
        {
            
        }
    }

    public class FakeDataProvider : IHeroRepositry, IDaysRepository
    {
        public List<Hero> GetAll()
        {
            return new List<Hero>()
        {
            new Hero()
            {
                Id = 1,
                Name = "TestHero1",
                Health = 98,
                HealthStatus = HealthStatus.Healthy,
                Status = HeroStatus.Active,
                Resources = new Resources()
                {
                    Id = 0,
                    Food = 10,
                    Water = 10,
                    Weapons = 10,
                    AlchemyIngredients = 10
                },
                Hunger = 95,
                Thirst = 95,
                Fatigue = 80,
            },
            new Hero()
            {
                Id = 2,
                Name = "TestHero2",
                Health = 67,
                HealthStatus = HealthStatus.Injured,
                Status = HeroStatus.Active,
                Resources = new Resources()
                {
                    Id = 1,
                    Food = 20,
                    Water = 12,
                    Weapons = 30,
                    AlchemyIngredients = 5
                },
                Hunger = 30,
                Thirst = 80,
                Fatigue = 85,
            }
        };
        }

        public void Add(Hero hero)
        {
            
        }

        public void Update(Hero hero)
        {

        }

        public void Delete(Hero hero)
        {
            
        }

        public void Add(DaysPassed daysPassed)
        {
            
        }

        List<DaysPassed> IDaysRepository.GetAll()
        {
            return new List<DaysPassed>()
        {
            new DaysPassed
            {
                Id = 1,
                currentDay = 100
            }
        };
        }

        public void Update(DaysPassed daysPassed)
        {
            
        }

        public List<Hero> GetHerosOnMissionData()
        {
            return new List<Hero>();
        }

        public List<string> GetFormattedHerosOnMissionData()
        {
            return new List<string>();
        }

        public int GetCurrentDay()
        {
            return new int();
        }
    }

    [TestFixture]
    public class Test
    {
        IHeroRepositry heroRepository;
        IDaysRepository daysRepository;
        IDataManipulationService dataManipulationService;

        ITaskRepository fakeTaskProvider = new FakeTaskDataprovider();
        IMonsterRepository fakeMonsterProvider = new FakeMonsterRepository();
        IStorageRepository fakeStorageProvider = new FakeStorageRepository();
        IFileHandlerService fakeFileHandler = new FakeFileHandlerService();

        [SetUp]
        public void Init()
        {
            var fakeProvider = new FakeDataProvider();
            heroRepository = fakeProvider;
            daysRepository = fakeProvider;
            dataManipulationService = new DataManipulationService
                (
                heroRepository, fakeTaskProvider, fakeMonsterProvider, fakeStorageProvider, daysRepository, fakeFileHandler
                );
        }

        [Test]
        public void EatReducesHunger()
        {
            var hero = heroRepository.GetAll().Where(x => x.Name == "TestHero1").FirstOrDefault();

            dataManipulationService.Eat(hero);

            Assert.That(hero.Hunger, Is.EqualTo(90));
            Assert.That(hero.Resources.Food, Is.EqualTo(9));
        }

        [Test]
        public void DrinkReducesThirst()
        {
            var hero = heroRepository.GetAll().Where(x => x.Name == "TestHero1").FirstOrDefault();

            dataManipulationService.Drink(hero);

            Assert.That(hero.Thirst, Is.EqualTo(90));
            Assert.That(hero.Resources.Water, Is.EqualTo(9));
        }

        [Test]
        public void CannotEatIfFoodIsZero()
        {
            var hero = heroRepository.GetAll().Where(x => x.Name == "TestHero1").FirstOrDefault();
            hero.Resources.Food = 0;

            dataManipulationService.Eat(hero);

            Assert.That(hero.Hunger, Is.EqualTo(95));
            Assert.That(hero.Resources.Food, Is.EqualTo(0));
        }

        [Test]
        public void CannotDrinkIfWaterIsZero()
        {
            var hero = heroRepository.GetAll().Where(x => x.Name == "TestHero1").FirstOrDefault();
            hero.Resources.Water = 0;

            dataManipulationService.Drink(hero);

            Assert.That(hero.Thirst, Is.EqualTo(95));
            Assert.That(hero.Resources.Water, Is.EqualTo(0));
        }

        [Test]
        public void EatCombatNoFoodRemains()
        {
            var hero = heroRepository.GetAll().Where(x => x.Name == "TestHero1").FirstOrDefault();
            hero.Resources.Food = 0;

            dataManipulationService.EatCombat(hero);

            Assert.That(hero.Health, Is.EqualTo(98));
            Assert.That(hero.Resources.Food, Is.EqualTo(0));
        }

        [Test]
        public void DrinkCombatNoWaterRemains()
        {
            var hero = heroRepository.GetAll().Where(x => x.Name == "TestHero1").FirstOrDefault();
            hero.Resources.Water = 0;

            dataManipulationService.DrinkCombat(hero);

            Assert.That(hero.Health, Is.EqualTo(98));
            Assert.That(hero.Resources.Water, Is.EqualTo(0));
        }

        [Test]
        public void EatCombatHealthChanges()
        {
            var hero = heroRepository.GetAll().Where(x => x.Name == "TestHero1").FirstOrDefault();

            dataManipulationService.EatCombat(hero);

            Assert.That(hero.Health, Is.EqualTo(100));
            Assert.That(hero.Resources.Food, Is.EqualTo(9));
        }

        [Test]
        public void DrinkCombatHealthChanges()
        {
            var hero = heroRepository.GetAll().Where(x => x.Name == "TestHero1").FirstOrDefault();

            dataManipulationService.DrinkCombat(hero);

            Assert.That(hero.Health, Is.EqualTo(100));
            Assert.That(hero.Resources.Water, Is.EqualTo(9));
        }

        [Test]
        public void IsHeroDead()
        {
            var hero = heroRepository.GetAll().Where(x => x.Name == "TestHero1").FirstOrDefault();
            dataManipulationService.MarkHeroAsDeceased(hero);
            Assert.That(hero.Status, Is.EqualTo(HeroStatus.Dead));
        }

        [Test]
        public void TaskCompleted()
        {
            var hero = heroRepository.GetAll().Where(x => x.Name == "TestHero1").FirstOrDefault();
            dataManipulationService.Recovery(hero);
            Assert.That(hero.Status, Is.EqualTo(HeroStatus.Recovery));
        }
    }
}
