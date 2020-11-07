using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using ZEROWORLD.Files;
using ZEROWORLD.Items.ExtendMode;

namespace ZEROWORLD.Players
{
    public class SetupStartPlayer : ModPlayer
    {
        public override void SetupStartInventory(IList<Item> items, bool mediumcoreDeath)
        {
            if (!mediumcoreDeath)
                items.Add(ZFunctions.ItemSetDefaults(ModContent.ItemType<FuruiYohishi>()));
            else
                items.Add(ZFunctions.ItemSetDefaults(ModContent.ItemType<ChisanaMemo>()));
        }
    }
}