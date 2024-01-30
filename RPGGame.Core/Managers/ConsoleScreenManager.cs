using RPGGame.Contracts;
using RPGGame.Enums;

namespace RPGGame
{
    public class ConsoleScreenManager : IScreenManager
    {
        public void ShowCharacterSelect()
        {
            Console.Clear();

            Console.WriteLine("Choose character type:");
            Console.WriteLine("Options:");
            Console.WriteLine("1) Warrior");
            Console.WriteLine("2) Archer");
            Console.WriteLine("3) Mage");
            Console.WriteLine("Your pick: ");

            var characterNumber = Console.ReadKey(true).KeyChar;

            AssignHeroToPlayer(characterNumber);

            Console.WriteLine("Would you like to buff up your stats before starting?                    (Limit: 3 points total)");
            Console.Write("Response (Y\\N): ");

            var response = char.ToUpper(Console.ReadKey(true).KeyChar);

            if (response == 'Y')
            {
                int remainingPoints = 3;

                var remainingPointsText = $"Remaining Points: {remainingPoints}";

                Console.WriteLine(remainingPointsText);

                //gets the coordinates of the remaining points number, that is displayed in the console
                var pointsConsoleRowPosition = Console.CursorTop - 1;
                var pointsConsoleColPosition = Console.CursorLeft + remainingPointsText.Length - 1;

                AddPointsToPlayer(remainingPoints, pointsConsoleRowPosition, pointsConsoleColPosition);
            }
            else if (response != 'N')
            {
                throw new InvalidOperationException("Error: You cannot type symbols that are different than Y or N!");
            }
        }

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

    }
}
