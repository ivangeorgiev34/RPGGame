using RPGGame.Contracts;
using RPGGame.Enums;

namespace RPGGame
{
    public class ConsoleScreenManager : IScreenManager
    {
        public void ShowMainMenu()
        {
            Console.WriteLine("Welcome!");
            Console.WriteLine("Press any key to play!");

            var key = Console.ReadKey(true);

            if (key.Key.ToString() != "")
            {
                Program.currentScreen = Screen.CharacterSelect;
            }
        }
    }
}
