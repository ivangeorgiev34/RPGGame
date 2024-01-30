using RPGGame.Contracts;
using RPGGame.Enums;
using RPGGame.Models;

namespace RPGGame
{
    internal class Program
    {
        public static Screen currentScreen = Screen.MainMenu;
        private static IScreenManager screenManager = new ConsoleScreenManager();
        private static Creature? player;

        static void Main(string[] args)
        {
            screenManager.ShowMainMenu();

        }
    }
}
