namespace RPGGame.Core.Contracts
{
    public interface IPlayerService
    {
        public void AddPointsToPlayerProperty(int points, string currentProperty, string[] heroProperties);

    }
}
