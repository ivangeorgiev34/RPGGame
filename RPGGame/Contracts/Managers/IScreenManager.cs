namespace RPGGame.Contracts.Managers
{
    public interface IScreenManager
    {
        public void ShowMainMenu();

        public void ShowCharacterSelect();

        public void ShowInGame();

        public void ResetPlayerPosition();

        public void ResetMonsters();

        public void OutsidePlayingBoardError();

        public void MonsterOnDesiredSpotError();
    }
}
