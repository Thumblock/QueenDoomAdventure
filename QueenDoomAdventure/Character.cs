using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueenDoomAdventure
{
    public abstract class Character
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Damage { get; set; }

        public Character(string name, int health, int damage)
        {
            Name = name;
            Health = health;
            Damage = damage;
        }

        public void Attack(Character target)
        {
            target.Health -= Damage;
            System.Console.WriteLine($"{Name} attacks {target.Name} for {Damage} damage!");
        }

        public bool IsAlive() => Health > 0;
    }
}
