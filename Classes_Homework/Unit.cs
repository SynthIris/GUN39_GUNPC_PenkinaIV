using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes_Homework
{
    public class Unit
    {
        private float _health;
        public string Name { get; }                    
        public float Health => _health;   
        public Interval Damage {  get; }                   
        public float Armor { get; }
        public Unit() : this("Unknown Unit")          
        {
           
        }

        public Unit(string name) : this(name, 0, 10)
        {

        }

        public Unit(string name, int minDamage, int maxDamage)                      
        {
            Name = name;
            _health = 100f;     
            Damage = new Interval(minDamage, maxDamage);         
            Armor = 0.6f;     
        }

        public float GetRealHealth()                   
        {
            return _health * (1f + Armor);  
        }

        public bool SetDamage(float value)             
        {
            _health = _health - value * Armor;
            return _health <= 0f;
        }

    }
}
