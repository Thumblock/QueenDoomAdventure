using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueenDoomAdventure
{
    public class Item
    {
        public string Name { get; }
        public bool IsHealing { get; }

        public Item(string name, bool isHealing = false)
        {
            Name = name;
            IsHealing = isHealing;
        }

        public static List<Item> GetPredefinedItems()
        {
            return new List<Item>
            {
                new Item("Silver Cross"),
                new Item("Silver Bullet"),
                new Item("The one ring"),
                new Item("Healing Potion", true)
            };
        }
    }
}
