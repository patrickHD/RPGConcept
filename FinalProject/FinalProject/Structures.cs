using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public struct Gear
    {
        public string Name { get; set; }
        public Stat StatChange { get; set; }
    }

    public enum Stat
    {
        Hp,
        Ap,
        Def,
        Atk
    }

    public enum Direction
    {
        Up, Down, Left, Right
    }
}
