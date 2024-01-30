namespace RPGGame.Core.Contracts.Services
{
    public interface IPlayerService
    {
        public void AddPointsToPlayer(int remainingPoints, int remainingPointsConsoleRowPosition, int remainingPointsConsoleColPosition);

        public void AssignHeroToPlayer(char characterNumber);
    }
}
