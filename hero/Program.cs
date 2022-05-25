using System;

namespace hero
{
    public class Rand
    {
        public int Run(int min, int max)
        {
            int range = (max - min) + 1;
            Random rng = new Random();
            return min + rng.Next() % range;
        }
    }
    public class Hero
    {
        public string Name;
        private int Strength;
        private int Dexterity;
        private int Intelligence;
        public double HP;
        public double MP;
        public double Stamina;

        private void Init(int strength = 10, int dexterity = 10, int intelligence = 10)
        {
            this.Strength = strength;
            this.Dexterity = dexterity;
            this.Intelligence = intelligence;
            Stamina = 20 + (2 * dexterity);
            HP = 50 + strength;
            MP = 10 + (3 * intelligence);
        }

        public int GetStrength() { return this.Strength; }
        public int GetDexterity() { return this.Dexterity; }
        public int GetIntelligence() { return this.Intelligence; }

        public void UpStrength() { this.Strength += 5; this.HP += 5; }
        public void UpDexterity() { this.Dexterity += 5; this.Stamina += 5; }
        public void UpIntelligence() { this.Intelligence += 5; this.MP += (3 * this.Intelligence); }

        public Hero(string name, string myclass)
        {
            Name = name;
            switch (myclass)
            {
                case "warior": Init(15, 5, 5); break;
                case "assassin": Init(10, 15, 0); break;
                case "sorcerer": Init(0, 5, 20); break;
                default: Init(); break;
            }
        }

        public void Attack(Hero enemy)
        {
            Rand rand = new Rand();
            double damage = Strength * rand.Run(5, 10) / 10;

            if (rand.Run(0, 100) > enemy.GetDexterity())
            {
                Console.WriteLine("Bang!");
                enemy.HP -= damage;
            }
            else Console.WriteLine("Dodge!");
        }

        public void LevelUp()
        {
            Console.Write("  1:Strength, 2:Dexterity, 3:Intelligence ... ");
            int opt = int.Parse(Console.ReadLine());

            switch (opt)
            {
                case 1: UpStrength(); break;
                case 2: UpDexterity(); break;
                case 3: UpIntelligence(); break;
            }

            Console.WriteLine();
        }

        public void Spell(Hero enemy)
        {
            Console.WriteLine("Choose your spell....");
            int magic = int.Parse(Console.ReadLine());
            double damage = Intelligence * 0.7;

            switch (magic)
            {
                case 1:
                    if (MP < 20)
                    {
                        Console.WriteLine("Nie możesz tego zrobić, masz za mało many");
                        break;
                    }
                    Console.WriteLine("Wybrałeś kule ognia!");
                    enemy.HP -= damage;
                    MP -= 20;
                    break;
                case 2:
                    if (MP < 30)
                    {
                        Console.WriteLine("Nie możesz tego zrobić, masz za mało many");
                        break;
                    }
                    Console.WriteLine("Wybrałeś leczenie!");
                    HP += 20;
                    MP -= 30;
                    break;
                
            }
            

        }

        // TODO: Per-round (regeneration...)
    }

    class Program
    {
        static void Main(string[] args)
        {


            int tour = 1;

            Hero hero1 = new Hero("Edward Białykij", "sorcerer");
            Console.WriteLine(hero1.Name + " Str:{0} Dex:{1} Int:{2} HP:{3} MP:{4} ST:{5}", hero1.GetStrength(), hero1.GetDexterity(), hero1.GetIntelligence(), hero1.HP, hero1.MP, hero1.Stamina);

            Hero hero2 = new Hero("Wataszka Stefan", "assassin");
            Console.WriteLine(hero2.Name + " Str:{0} Dex:{1} Int:{2} HP:{3} MP:{4} ST:{5}", hero2.GetStrength(), hero2.GetDexterity(), hero2.GetIntelligence(), hero2.HP, hero2.MP, hero2.Stamina);

            Console.WriteLine();

            while (hero1.HP > 0 && hero2.HP > 0)
            {
                if (tour == 1) Console.WriteLine("Your Turn: " + hero1.Name);
                else Console.WriteLine("Your Turn: " + hero2.Name);

                Console.Write("1:Attack, 2:Spell, 3:LevelUp ... ");
                int opt = int.Parse(Console.ReadLine());

                switch (opt)
                {
                    case 1:
                        if (tour == 1) hero1.Attack(hero2);
                        else hero2.Attack(hero1);
                        break;

                    case 2:
                        if (tour == 1) hero1.Spell(hero2);
                        else hero2.Spell(hero1);
                        break;

                    case 3:
                        if (tour == 1) hero1.LevelUp();
                        else hero2.LevelUp();
                        break;
                }

                Console.WriteLine(hero1.Name + " Str:{0} Dex:{1} Int:{2} HP:{3} MP:{4} ST:{5}", hero1.GetStrength(), hero1.GetDexterity(), hero1.GetIntelligence(), hero1.HP, hero1.MP, hero1.Stamina);
                Console.WriteLine(hero2.Name + " Str:{0} Dex:{1} Int:{2} HP:{3} MP:{4} ST:{5}", hero2.GetStrength(), hero2.GetDexterity(), hero2.GetIntelligence(), hero2.HP, hero2.MP, hero2.Stamina);
                Console.WriteLine();

                tour++;
                if (tour > 2) tour = 1;
            }
            // TODO: Win
        }
    }
}
