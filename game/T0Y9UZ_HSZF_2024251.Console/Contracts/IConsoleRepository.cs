using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T0Y9UZ_HSZF_2024251.Model.Entities;

namespace T0Y9UZ_HSZF_2024251.Console.Contracts
{
    public interface IConsoleRepository
    {
        void SplashScreen();
        void ShowMainMenu();
        void MainMenuLoadGame();
        void NewGame(string message, ICollection<Hero> heroes);
        public void ShowBasicGameInfos();
        public void ShowBasicHeroInfos();
        public void DisplayOptions();
        public void ShowHeroesListOptions();
        public void FilterHeroesByAbility();
        public void ShowFilteredHeroesByAbility(string ability);
        public void ListHeroes();
        public void ShowHeroInfos(ICollection<Hero> data, int idxOfHero);
        public void PlaceIntoStorage(Hero hero, ICollection<Hero> data, int idxOfHero);
        public void GetFromStorage(Hero hero, ICollection<Hero> data, int idxOfHero);
        public void ShowTasks();
        public void ShowTaskInfo(GameTask task);
        public void ChooseHeroForTask(GameTask task);
        public void ShowBasicGameInfos(string kuldetes);
        public void ShowAvailableMonsters();
        public void ChooseHeroForCombat(Monster monster, int idxOfTask);
        public void CombatConsole(Hero hero, Monster monster);
        public void BattleFinishedConsole(Hero hero, Monster monster);
        public void ShowResources();
        public void PauseGame();
        public void PausedLoadGame();
        public void PausedSaveGame();
        public void ShowGameCompleted();
        public void ShowGameOver();
    }
}
