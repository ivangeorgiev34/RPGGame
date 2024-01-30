namespace RPGGame.Infrastructure.Models
{
    public class Mage : Creature
    {
        public override char Symbol => '*';

        public override int Strenght { get; set; } = 2;
        public override int Agility { get; set; } = 1;
        public override int Intelligence { get; set; } = 3;
        public override int Range { get; set; } = 3;
    }
}
