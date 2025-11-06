using GamePrototype.Dungeon;
using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;

namespace GamePrototype.Utils
{
    public static class DungeonBuilder
    {
        public static DungeonRoom BuildDungeon()
        {
            var enter = new DungeonRoom("Enter");
            var monsterRoom = new DungeonRoom("Monster", UnitFactoryDemo.CreateGoblinEnemy());
            var emptyRoom = new DungeonRoom("Empty");
            var rangeWeaponRoom = new DungeonRoom("Range Weapon", new RangeWeapon(8, 12, "Wooden Bow"));
            var helmetRoom = new DungeonRoom("Helmet", new Helmet(5, 10, "Iron Helmet"));
            var finalRoom = new DungeonRoom("Final", new Grindstone("Stone1"));

            enter.TrySetDirection(Direction.Right, monsterRoom);
            enter.TrySetDirection(Direction.Left, emptyRoom);

            monsterRoom.TrySetDirection(Direction.Forward, rangeWeaponRoom);
            monsterRoom.TrySetDirection(Direction.Left, emptyRoom);

            emptyRoom.TrySetDirection(Direction.Forward, helmetRoom);

            rangeWeaponRoom.TrySetDirection(Direction.Forward, finalRoom);
            helmetRoom.TrySetDirection(Direction.Forward, finalRoom);

            return enter;
        }
    }
}