using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public class Item
    {
        public string Name { get; set; }
        public Stat StatChange { get; set; }
        public int Magnitute { get; set; }

        public void Use(Player player)
        {
            switch (StatChange)
            {
                case Stat.Hp:
                    player.Hp += Magnitute;
                    player.heal(Magnitute);
                    break;

                default:

                    break;
            }
        }

        public Item(Stat stat, string name, int magnitude)
        {
            Name = name;
            StatChange = stat;
            Magnitute = magnitude;
        }
    }
}
