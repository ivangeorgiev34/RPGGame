namespace RPGGame.Infrastructure.Models
{
    public class Archer : Creature
    {
        public override char Symbol => '#';

        public override int Strenght { get; set; } = 2;
        public override int Agility { get; set; } = 4;
        public override int Intelligence { get; set; } = 0;
        public override int Range { get; set; } = 2;
    }
}
