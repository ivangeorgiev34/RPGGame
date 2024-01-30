using RPGGame.Core.Contracts;

namespace RPGGame.Core.Services
{
    public class PlayerService : IPlayerService
    {

        public void AddPointsToPlayerProperty(int points, string currentProperty, string[] heroProperties)
        {
            if (Program.player != null)
            {
                if (currentProperty == heroProperties[0])
                {
                    Program.player.Strenght = points;
                }
                else if (currentProperty == heroProperties[1])
                {
                    Program.player.Agility = points;
                }
                else if (currentProperty == heroProperties[2])
                {
                    Program.player.Intelligence = points;
                }
            }
        }
    }
}
