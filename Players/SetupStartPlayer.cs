using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using ZEROWORLD.Files;
using ZEROWORLD.Items.ExtendMode;

namespace ZEROWORLD.Players
{
    public partial class ZPlayer : ModPlayer
    {
        public override void SetupStartInventory(IList<Item> items, bool mediumcoreDeath)
        {
            if (!mediumcoreDeath)
            {
                ZFunctions.ItemSetDefaults(ModContent.ItemType<FuruiYohishi>());
            }
        }
    }
}