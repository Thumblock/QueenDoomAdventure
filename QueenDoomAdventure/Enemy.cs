using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace QueenDoomAdventure
{
    public class Enemy : Character
    {
        public Enemy(string name, int health, int damage) : base(name, health, damage) { }

        public Companion RecruitAsCompanion()
        {
            return new Companion(Name, Health, Damage);
        }
    }
}
