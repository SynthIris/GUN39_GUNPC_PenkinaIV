using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;
using GamePrototype.Utils;
using System.Text;

namespace GamePrototype.Units
{
    public sealed class Player : Unit
    {
        private readonly Dictionary<EquipSlot, EquipItem> _equipment = new();

        public Player(string name, uint health, uint maxHealth, uint baseDamage) : base(name, health, maxHealth, baseDamage)
        {
        }

        public override uint GetUnitDamage()
        {
            uint damage = BaseDamage;

            if (_equipment.TryGetValue(EquipSlot.Weapon, out var weapon) && weapon is Weapon mainWeapon)
            {
                damage += mainWeapon.Damage;
            }
            else if (_equipment.TryGetValue(EquipSlot.RangeWeapon, out var rangeWeapon) && rangeWeapon is RangeWeapon rWeapon)
            {
                damage += rWeapon.Damage;
            }

            return damage;
        }

        public override void HandleCombatComplete()
        {
            var items = Inventory.Items;
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] is EconomicItem economicItem)
                {
                    UseEconomicItem(economicItem);
                    Inventory.TryRemove(items[i]);
                }
            }
        }

        public override void AddItemToInventory(Item item)
        {
            if (item is EquipItem equipItem)
            {
                if (_equipment.ContainsKey(equipItem.Slot))
                {
                    var oldItem = _equipment[equipItem.Slot];
                    _equipment[equipItem.Slot] = equipItem;
                    Console.WriteLine($"Произошла замена экипировки в слоте {equipItem.Slot}: {oldItem.Name} -> {equipItem.Name}");
                    base.AddItemToInventory(oldItem);
                }
                else
                {
                    _equipment.TryAdd(equipItem.Slot, equipItem);
                    Console.WriteLine($"Экипирован предмет: {equipItem.Name} в слот {equipItem.Slot}");
                }
            }
            else
            {
                base.AddItemToInventory(item);
            }
        }

        private void UseEconomicItem(EconomicItem economicItem)
        {
            if (economicItem is HealthPotion healthPotion)
            {
                Health = System.Math.Min(Health + healthPotion.HealthRestore, MaxHealth);
            }
            else if (economicItem is Grindstone grindstone)
            {
                if (_equipment.TryGetValue(EquipSlot.Weapon, out var weaponItem) && weaponItem is Weapon weapon)
                {
                    weapon.Repair(4);
                }
                Inventory.TryRemove(grindstone);
            }
        }

        protected override uint CalculateAppliedDamage(uint damage)
        {
            if (_equipment.TryGetValue(EquipSlot.Armour, out var item) && item is Armour armour)
            {
                damage -= (uint)(damage * (armour.Defence / 100f));
            }
            return damage;
        }

        protected override void DamageReceiveHandler()
        {
            if (_equipment.TryGetValue(EquipSlot.Armour, out var armourItem) && armourItem is Armour armour)
            {
                armour.ReduceDurability(1);
                if (armour.Durability == 0)
                {
                    _equipment.Remove(EquipSlot.Armour);
                }
            }
        }

        public new uint MaxHealth
        {
            get
            {
                uint bonusHealth = 0;
                if (_equipment.TryGetValue(EquipSlot.Helmet, out var helmet) && helmet is Helmet helm)
                {
                    bonusHealth = helm.AdditionalHealth;
                }
                return base.MaxHealth + bonusHealth;
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine(Name);
            builder.AppendLine($"Health {Health}/{MaxHealth}");
            builder.AppendLine("Loot:");
            var items = Inventory.Items;
            for (int i = 0; i < items.Count; i++)
            {
                builder.AppendLine($"[{items[i].Name}] : {items[i].Amount}");
            }
            return builder.ToString();
        }
    }
}