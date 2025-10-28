using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes_Homework
{
    public class Dungeon
    {
        private Room[] _rooms;
        public Dungeon()
        {
            _rooms = new Room[0];
            {
                new Room(new Unit("Воин"), new Weapon("Меч", 5, 15));
                new Room(new Unit("Эльф", 2, 8), new Weapon("Лук", 3, 12));
                new Room(new Unit("Маг", 1, 20), new Weapon("Посох", 8, 25));
                new Room(new Unit("Варвар", 6, 12), new Weapon("Топор", 10, 18));
            }
        }

        public void ShowRooms()
        {
            for (int i =0; i < _rooms.Length; i++)
            {
                var room = _rooms[i];
                Console.WriteLine("Unit of room " + room.Unit);
                Console.WriteLine("Weapon of room " + room.Weapon);
                Console.WriteLine("---");
            }
        }
    }
}
