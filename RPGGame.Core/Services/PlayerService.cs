using RPGGame.Core.Contracts.Services;
using RPGGame.Models;

namespace RPGGame.Core.Services
{
    public class PlayerService : IPlayerService
    {
        public void AddPointsToPlayer(int remainingPoints, int remainingPointsConsoleRowPosition, int remainingPointsConsoleColPosition)
        {
            var heroProperties = new string[3] { "Strenght", "Agility", "Intelligence" };

            for (int index = 0; index < heroProperties.Length; index++)
            {
                if (remainingPoints == 0)
                {
                    return;
                }

                var currentProperty = heroProperties[index];

                Console.Write($"Add to {currentProperty}: ");

                var points = int.Parse(Console.ReadKey().KeyChar.ToString());

                if (remainingPoints < points)
                {
                    //reseting the cursor position
                    Console.SetCursorPosition(0, remainingPointsConsoleRowPosition + index + 2);

                    throw new InvalidOperationException("Error: You can't add more points than you already have!");
                }

                AddPointsToPlayerProperty(points, currentProperty, heroProperties);

                remainingPoints -= points;

                Console.SetCursorPosition(remainingPointsConsoleColPosition, remainingPointsConsoleRowPosition);

                Console.Write(remainingPoints);

                //reseting the cursor position
                Console.SetCursorPosition(0, remainingPointsConsoleRowPosition + index + 2);

            }
        }

        public void AssignHeroToPlayer(char characterNumber)
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

        private void AddPointsToPlayerProperty(int points, string currentProperty, string[] heroProperties)
        {
            if (Program.player != null)
            {
                if (currentProperty == heroProperties[0])
                {
                    Program.player.Strenght += points;
                }
                else if (currentProperty == heroProperties[1])
                {
                    Program.player.Agility += points;
                }
                else if (currentProperty == heroProperties[2])
                {
                    Program.player.Intelligence += points;
                }
            }
        }
    }
}
