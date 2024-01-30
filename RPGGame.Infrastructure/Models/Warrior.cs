namespace RPGGame.Infrastructure.Models
{
    public class Warrior : Creature
    {
        public override char Symbol => '@';

        public override int Strenght { get; set; } = 3;
        public override int Agility { get; set; } = 3;
        public override int Intelligence { get; set; } = 0;
        public override int Range { get; set; } = 1;
    }
}
