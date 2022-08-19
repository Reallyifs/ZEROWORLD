using Terraria.ModLoader;

namespace ZEROWORLD.Players
{
    public partial class ZPlayer : ModPlayer
    {
        public bool windlessCurseflameBuff;

        public override void ResetEffects()
        {
            windlessCurseflameBuff = false;
            CardPlayerReset();
        }
    }
}