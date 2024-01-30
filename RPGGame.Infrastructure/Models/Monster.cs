namespace RPGGame.Infrastructure.Models
{
    public class Monster : Creature
    {

        private int strenght;
        private int agility;
        private int intelligence;

        public Monster(int strenght, int agility, int intelligence)
        {
            this.Strenght = strenght;
            this.Agility = agility;
            this.Intelligence = intelligence;
        }

        public override int Strenght
        {
            get { return strenght; }
            set
            {
                if (value < 1 || value > 3)
                {
                    throw new ArgumentException("Strenght must be between 1 and 3");
                }

                strenght = value;
            }
        }
        public override int Agility
        {
            get { return agility; }
            set
            {
                if (value < 1 || value > 3)
                {
                    throw new ArgumentException("Agility must be between 1 and 3");
                }

                agility = value;
            }
        }

        public override int Intelligence
        {
            get { return intelligence; }
            set
            {
                if (value < 1 || value > 3)
                {
                    throw new ArgumentException("Intelligence must be between 1 and 3");
                }

                intelligence = value;
            }
        }

        public override int Range { get; set; } = 1;

        public override char Symbol => '◙';
    }
}
