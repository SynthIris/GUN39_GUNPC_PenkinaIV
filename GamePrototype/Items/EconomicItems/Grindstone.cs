namespace GamePrototype.Items.EconomicItems
{
    public sealed class Grindstone : EconomicItem
    {
        public uint RepairAmount => 4;
        public override bool Stackable => false;
        public Grindstone() : base("Sharpening Stone")
        {
        }

        public Grindstone(string name) : base(name)
        {
        }    
    }
}
