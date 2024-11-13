using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueenDoomAdventure
{
    public class House
    {
        public string Name { get; }
        public string Description { get; }

        public House(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
