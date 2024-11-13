using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace QueenDoomAdventure
{
    public class Game
    {
        Player player;
        Companion companion;
        List<Item> inventory;
        List<House> map;
        int currentLocation;
        Random random = new Random();

        public void Start()
        {
            player = new Player("Lilith", 100, 20);
            companion = new Companion("Companion friend", 80, 15);
            inventory = new List<Item>();

            map = new List<House>
            {
                new House("Main-House A", "Old barn house with broken windows."),
                new House("Side-HouseB", "A smaller house connected to houseA"),
                new House("Side-HouseC", "A smaller house connected to Housea"),
                new House("BackYard", "Backyard behind the 3 houses")
            };
            currentLocation = 0;

            Console.WriteLine("Welcome to QueenDoom - A text based adventure");
            Console.WriteLine("You and your companion are on a journey to defeat monsters.");

            while (player.IsAlive())
            {
                Console.WriteLine("\nWhat would you like to do?");
                Console.WriteLine("1. Explore the area");
                Console.WriteLine("2. Check Inventory");
                Console.WriteLine("3. Move to another location");
                Console.WriteLine("4. Quit");
                Console.Write("> ");
                string choice = Console.ReadLine();

                if (choice == "1") Explore();
                else if (choice == "2") ShowInventory();
                else if (choice == "3") Move();
                else if (choice == "4") break;
                else Console.WriteLine("Invalid choice. Try again.");
            }

            if (!player.IsAlive()) Console.WriteLine("You have been defeated. Game Over.");
        }

        private void Explore()
        {
            Console.WriteLine($"\nExploring the {map[currentLocation].Name}: {map[currentLocation].Description}");

            int encounterChance = random.Next(3);
            if (encounterChance == 0)
            {
                Enemy enemy = GenerateEnemy();
                Console.WriteLine($"A wild {enemy.Name} appears!");

                if (HasEffectiveItem(enemy))
                {
                    Console.WriteLine($"You use an effective item against {enemy.Name}, instantly defeating it!");
                    RecruitCompanion(enemy);
                }
                else
                {
                    Fight(enemy);
                }
            }
            else if (encounterChance == 1)
            {
                FindItem();
            }
            else
            {
                Console.WriteLine("You found nothing of interest here.");
            }
        }

        private void Fight(Enemy enemy)
        {
            while (enemy.IsAlive() && player.IsAlive())
            {
                Console.WriteLine($"\n{player.Name} HP: {player.Health} | {companion.Name} HP: {companion.Health} | {enemy.Name} HP: {enemy.Health}");
                Console.Write("Choose an action: (attack / flee) > ");
                string action = Console.ReadLine().ToLower();

                if (action == "attack")
                {
                    player.Attack(enemy);
                    if (enemy.IsAlive()) companion.Attack(enemy);
                    if (enemy.IsAlive())
                    {
                        enemy.Attack(player);
                        if (player.IsAlive()) enemy.Attack(companion);
                    }
                }
                else if (action == "flee")
                {
                    Console.WriteLine("You flee from the battle!");
                    return;
                }
            }

            if (!enemy.IsAlive()) RecruitCompanion(enemy);
        }

        private void RecruitCompanion(Enemy enemy)
        {
            Console.WriteLine($"Recruit {enemy.Name} as a companion? (yes/no)");
            if (Console.ReadLine().ToLower() == "yes")
            {
                companion = enemy.RecruitAsCompanion();
                Console.WriteLine($"{enemy.Name} has joined you as a companion!");
            }
        }

        private bool HasEffectiveItem(Character monster)
        {
            foreach (Item item in inventory)
                if (!item.IsHealing && item.Name.Contains(monster.Name)) return true;

            return false;
        }

        private void FindItem()
        {
            Item newItem = Item.GetPredefinedItems()[random.Next(Item.GetPredefinedItems().Count)];
            Console.WriteLine($"You found a {newItem.Name}!");
            inventory.Add(newItem);
        }

        private void ShowInventory()
        {
            Console.WriteLine("\nInventory:");
            if (inventory.Count == 0) Console.WriteLine("Your inventory is empty.");
            else foreach (var item in inventory) Console.WriteLine($"- {item.Name}");
        }

        private void Move()
        {
            Console.WriteLine("\nChoose a new location to explore:");
            for (int i = 0; i < map.Count; i++)
            {
                Console.WriteLine($"{i}. {map[i].Name}");
            }
            Console.Write("> ");
            if (int.TryParse(Console.ReadLine(), out int location) && location >= 0 && location < map.Count)
            {
                currentLocation = location;
                Console.WriteLine($"You move to {map[currentLocation].Name}.");
            }
            else
            {
                Console.WriteLine("Invalid location.");
            }
        }

        private Enemy GenerateEnemy()
        {
            string[] enemyNames = { "Ring-Wrath", "Werewolf", "Vampire-bat" };
            return new Enemy(enemyNames[random.Next(enemyNames.Length)], random.Next(30, 70), random.Next(10, 20));
        }
    }
}
