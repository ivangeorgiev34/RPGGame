namespace RPGGame.Infrastructure.Models
{
    public abstract class Creature
    {
        public abstract int Strenght { get; set; }
        public abstract int Agility { get; set; }
        public abstract int Intelligence { get; set; }
        public abstract int Range { get; set; }
        public int Health { get; set; }
        public int Mana { get; set; }
        public int Damage { get; set; }

        public abstract char Symbol { get; }

        public void Setup()
        {
            this.Health = this.Strenght * 5;
            this.Mana = this.Intelligence * 3;
            this.Damage = this.Agility * 2;

        }
    }
}
