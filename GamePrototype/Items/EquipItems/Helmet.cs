using GamePrototype.Utils;

namespace GamePrototype.Items.EquipItems
{
    public sealed class Helmet : EquipItem
    {
        public uint AdditionalHealth { get; }

        public Helmet(uint additionalHealth, uint durability, string name) : base(durability, name)
        {
            AdditionalHealth = additionalHealth;
        }

        public override EquipSlot Slot => EquipSlot.Helmet;
    }
}
