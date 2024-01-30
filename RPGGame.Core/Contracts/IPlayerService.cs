namespace RPGGame.Core.Contracts
{
    public interface IPlayerService
    {
        public void AddPointsToPlayerProperty(int points, string currentProperty, string[] heroProperties);

        public void AddPointsToPlayer(int remainingPoints, int remainingPointsConsoleRowPosition, int remainingPointsConsoleColPosition);
    }
}
