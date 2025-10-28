using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes_Homework
{
    public struct Room
    {
        public Unit Unit {  get; }
        public Weapon Weapon { get; }
        public Room(Unit unit, Weapon weapon)
        {
            Unit = unit;
            Weapon = weapon;
        }
    }
}
