using RPGGame.Contracts.Managers;
using RPGGame.Contracts.Services;
using RPGGame.Infrastructure.Enums;
using RPGGame.Infrastructure.Exceptions;
using RPGGame.Infrastructure.Models;
using RPGGame.Services;

namespace RPGGame
{
    public class Program
    {
        public static Screen currentScreen = Screen.MainMenu;
        private static IPlayerService playerService = new PlayerService();
        private static IScreenManager screenManager = new ConsoleScreenManager(playerService);
        public static Creature? player;

        static void Main(string[] args)
        {
            while (currentScreen != Screen.Exit)
            {
                screenManager.ShowMainMenu();
                try
                {
                    screenManager.ShowCharacterSelect();

                    screenManager.ShowInGame();
                }
                catch (Exception ioe)
                    when (ioe is ArgumentException
                || ioe is InvalidOperationException
                || ioe is InvalidInputException)
                {
                    Console.WriteLine(ioe.Message);
                    Console.WriteLine("Press any key to restart the game!");

                    var key = Console.ReadKey(true).Key.ToString();

                    if (key != "")
                    {
                        currentScreen = Screen.MainMenu;
                    }

                }
            }

            Console.WriteLine("Game over!");
        }
    }
}
