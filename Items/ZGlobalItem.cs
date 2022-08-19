using Terraria.ModLoader;

namespace ZEROWORLD.Items
{
    public class ZGlobalItem : GlobalItem
    {
        public int ownerRare;
        public ZItemClass cardClass;

        public override bool InstancePerEntity => true;

        public override bool CloneNewInstances => base.CloneNewInstances;
    }
}