namespace RPGGame.Core.Contracts
{
    public interface IPlayerService
    {
        public void AddPointsToPlayer(int remainingPoints, int remainingPointsConsoleRowPosition, int remainingPointsConsoleColPosition);

        public void AssignHeroToPlayer(char characterNumber);
    }
}
