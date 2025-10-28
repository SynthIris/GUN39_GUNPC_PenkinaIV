using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes_Homework
{
    public class Weapon
    {
        public string Name { get; }
        public Interval Damage { get; private set; }
        public float Durability { get; }

        public Weapon(string name)
        {
            Name = name;
            Durability = 1f;
            Damage = new Interval(1, 10);
        }

        public Weapon(string name, int minDamage, int maxDamage) : this (name)
        {
            SetDamageParams(minDamage, maxDamage);
        }

        public void SetDamageParams(int minDamage, int maxDamage)
        {
            Damage = new Interval(minDamage, maxDamage);
        }

        public int GetDamage()
        {
            return (int) Damage.Get();
        }

    }
}
