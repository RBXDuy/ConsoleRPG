using System;

namespace ConsoleGame
{
    public class Player
    {
        public string Name { get; }
        public int Level { get; private set; } = 1;
        public int Experience { get; private set; }
        public int HP { get; private set; }
        public int MaxHP { get; private set; }
        public int AttackPower { get; private set; }
        public int Defense { get; private set; }
        public int Potions { get; private set; }

        public bool IsAlive => HP > 0;

        public Player(string name)
        {
            Name = name;
            Level = 1;
            MaxHP = 30;
            HP = MaxHP;
            AttackPower = 6;
            Defense = 1;
            Potions = 2;
            Experience = 0;
        }

        public int Attack()
        {
            // Simple damage formula with slight randomness
            var rnd = new Random();
            var dmg = AttackPower + rnd.Next(0, 4);
            return dmg;
        }

        public void TakeDamage(int amount)
        {
            var reduced = Math.Max(0, amount - Defense);
            HP -= reduced;
            if (HP < 0) HP = 0;
        }

        public void Rest()
        {
            var heal = Math.Max(1, MaxHP / 6);
            HP += heal;
            if (HP > MaxHP) HP = MaxHP;
        }

        public bool UsePotion()
        {
            if (Potions <= 0) return false;
            Potions--;
            var heal = Math.Max(5, MaxHP / 3);
            HP += heal;
            if (HP > MaxHP) HP = MaxHP;
            return true;
        }

        public void GainExperience(int amount)
        {
            Experience += amount;
            // Level up threshold: 20 * level
            var threshold = 20 * Level;
            while (Experience >= threshold)
            {
                Experience -= threshold;
                LevelUp();
                threshold = 20 * Level;
            }
        }

        private void LevelUp()
        {
            Level++;
            MaxHP += 8;
            HP = MaxHP;
            AttackPower += 2;
            Defense += 1;
            Potions += 1; // reward
            Console.WriteLine($"You leveled up! You are now level {Level}.");
        }

        public void PrintStatus()
        {
            Console.WriteLine($"{Name} - Level {Level}");
            Console.WriteLine($"HP: {HP}/{MaxHP}  Attack: {AttackPower}  Defense: {Defense}");
            Console.WriteLine($"Potions: {Potions}  EXP: {Experience}");
        }
    }
}
