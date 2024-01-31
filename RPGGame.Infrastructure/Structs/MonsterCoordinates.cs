namespace RPGGame.Infrastructure.Structs
{
    public struct MonsterCoordinates
    {
        public MonsterCoordinates(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }

        public int Row { get; set; }
        public int Column { get; set; }
    }
}
