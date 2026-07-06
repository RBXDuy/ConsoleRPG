using System;

namespace ConsoleGame
{
    public abstract class Enemy
    {
        public string Name { get; protected set; }
        public int HP { get; protected set; }
        public int MaxHP { get; protected set; }
        public int AttackPower { get; protected set; }
        public int ExperienceReward { get; protected set; }

        public bool IsAlive => HP > 0;

        public virtual int Attack()
        {
            var rnd = new Random();
            return AttackPower + rnd.Next(0, 3);
        }

        public virtual void TakeDamage(int amount)
        {
            HP -= amount;
            if (HP < 0) HP = 0;
        }
    }

    public class Goblin : Enemy
    {
        public Goblin()
        {
            Name = "Goblin";
            MaxHP = 12;
            HP = MaxHP;
            AttackPower = 4;
            ExperienceReward = 10;
        }
    }

    public class GreatGoblin : Enemy
    {
        public GreatGoblin()
        {
            Name = "Great Goblin";
            MaxHP = 20;
            HP = MaxHP;
            AttackPower = 6;
            ExperienceReward = 20;
        }
    }
    public class GoblinLord : Enemy
    {
        public GoblinLord()
        {
            Name = "Goblin Lord";
            MaxHP = 50;
            HP = MaxHP;
            AttackPower = 12;
            ExperienceReward = 50;
        }
    }
}