using System;

namespace ConsoleGame
{
    // Main game loop + simple world actions
    public class Game
    {
        private Player _player;
        private Random _rng = new Random();

        public Game()
        {
            Console.Write("Enter your name: ");
            var name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name)) name = "Hero";
            _player = new Player(name);
        }

        public void Run()
        {
            PrintWelcome();

            while (_player.IsAlive)
            {
                ShowMenu();
                var input = Console.ReadLine()?.Trim().ToLower();
                Console.WriteLine();

                switch (input)
                {
                    case "1":
                    case "explore":
                        Explore();
                        break;
                    case "2":
                    case "rest":
                        Rest();
                        break;
                    case "3":
                    case "stats":
                        _player.PrintStatus();
                        break;
                    case "4":
                    case "quit":
                        Console.WriteLine("Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Unknown command.");
                        break;
                }

                if (!_player.IsAlive)
                {
                    Console.WriteLine("You have fallen. Game over.");
                }
            }
        }

        private void PrintWelcome()
        {
            Console.WriteLine("Welcome to the simple OOP RPG!");
            Console.WriteLine("You can explore, fight monsters, rest and use items.");
            Console.WriteLine();
        }

        private void ShowMenu()
        {
            Console.WriteLine();
            Console.WriteLine("What will you do?");
            Console.WriteLine("1) Explore");
            Console.WriteLine("2) Rest (+heal)");
            Console.WriteLine("3) Stats");
            Console.WriteLine("4) Quit");
            Console.Write("Choice: ");
        }

        private void Explore()
        {
            Console.WriteLine("You venture forth...");
            // 60% nothing, 40% encounter
            var roll = _rng.NextDouble();
            if (roll < 0.4)
            {
                var enemy = CreateRandomEnemy();
                Console.WriteLine($"A wild {enemy.Name} appears!");
                Combat(enemy);
            }
            else
            {
                Console.WriteLine("No enemies found.");
            }
        }

        private Enemy CreateRandomEnemy()
        {
            // For simplicity, only one enemy type; could expand easily
            int enemyType = _rng.Next(1, 10); // 1 or 2
            if (enemyType <= 7)
            {
                return new Goblin();
            }
            else if (enemyType <= 9)
            {
                return new GreatGoblin();
            }
            else
            {
                return new GoblinLord();
            }
            return new Goblin();
        }

        private void Rest()
        {
            Console.WriteLine("You take a rest and recover some health.");
            _player.Rest();
            _player.PrintStatus();
        }

        private void Combat(Enemy enemy)
        {
            while (_player.IsAlive && enemy.IsAlive)
            {
                Console.WriteLine();
                Console.WriteLine($"-- {_player.Name} (HP: {_player.HP}/{_player.MaxHP}) vs {enemy.Name} (HP: {enemy.HP}/{enemy.MaxHP}) --");
                Console.WriteLine("Choose action: 1) Attack 2) Use Potion 3) Flee");
                Console.Write("Action: ");
                var action = Console.ReadLine()?.Trim();

                if (action == "1")
                {
                    var dmg = _player.Attack();
                    Console.WriteLine($"You strike the {enemy.Name} for {dmg} damage.");
                    enemy.TakeDamage(dmg);
                }
                else if (action == "2")
                {
                    if (_player.UsePotion())
                    {
                        Console.WriteLine("You used a potion.");
                    }
                    else
                    {
                        Console.WriteLine("No potions available.");
                    }
                }
                else if (action == "3")
                {
                    if (_rng.NextDouble() < 0.5)
                    {
                        Console.WriteLine("You fled successfully.");
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Failed to flee!");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid action.");
                }

                // Enemy turn if still alive
                if (enemy.IsAlive)
                {
                    var edmg = enemy.Attack();
                    Console.WriteLine($"The {enemy.Name} hits you for {edmg} damage.");
                    _player.TakeDamage(edmg);
                }
            }

            if (_player.IsAlive && !enemy.IsAlive)
            {
                Console.WriteLine($"You defeated the {enemy.Name}!");
                _player.GainExperience(enemy.ExperienceReward);
            }
        }
    }
}
