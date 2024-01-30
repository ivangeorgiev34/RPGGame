using RPGGame.Contracts;
using RPGGame.Enums;

namespace RPGGame
{
    public class ConsoleScreenManager : IScreenManager
    {
        public void ShowMainMenu()
        {
            Console.Clear();

            Console.WriteLine("Welcome!");
            Console.WriteLine("Press any key to play!");

            var key = Console.ReadKey(true);

            if (key.Key.ToString() != "")
            {
                Program.currentScreen = Screen.CharacterSelect;
            }
        }

        private void AssignHeroToPlayer(char characterNumber)
        {
            if (characterNumber == '1')
            {
                Program.player = new Warrior();
            }
            else if (characterNumber == '2')
            {
                Program.player = new Archer();
            }
            else if (characterNumber == '3')
            {
                Program.player = new Mage();
            }
            else
            {
                throw new InvalidOperationException("Error: You can only choose between the numbers 1, 2, and 3");
            }
        }
    }
}
