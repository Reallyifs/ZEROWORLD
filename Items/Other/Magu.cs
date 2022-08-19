using System;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ID;
using ZEROWORLD.Files;

namespace ZEROWORLD.Items.Other
{
    /// <summary>
    /// 马克杯
    /// </summary>
    public sealed class Magu : ZItem
    {
        public override bool Autoload(ref string name) => true;

        protected override void OwnerDefaults()
        {
            item.width = 32;
            item.height = 38;
            item.useStyle = ItemUseStyleID.EatingUsing;
            item.UseTimeAndAnimation(26);
            item.UseSound = SoundID.Item3;
            item.maxStack = 1;
            item.rare = ItemRarityID.Green;
            item.value = Item.buyPrice(silver: ZEROWORLD.SafeRandom.Next(0, 1), copper: ZEROWORLD.SafeRandom.Next(0, 20));
            item.consumable = true;
        }

        protected override void OwnerDisplay(GameCulture culture, ref bool support, ref string displayName, ref string displayTooltip)
        {
            if (culture == GameCulture.Chinese)
            {
                support = true;
                displayName = "马克杯";
                displayTooltip = "只是一个普通的马克杯……\n" +
                    "谁知道呢？" +
                    (ZFunctions.DuringNewYear() ? "\n[特殊效果] 新年期间有神秘掉落！" : null);
            }
            else if (culture == GameCulture.English)
            {
                support = true;
                displayName = "Mug";
                displayTooltip = "Just an ordinary mug...\n" +
                    "Who knows?" +
                    (ZFunctions.DuringNewYear() ? "\n[Special effects] Mysterious drops during the New Year!" : null);
            }
        }

        protected override int OwnerListDefault(out float level, out Version version, out DateTime date)
        {
            date = new DateTime(2020, 12, 20);
            level = 1f;
            version = new Version(0, 1, 0, 0);
            return ModContent.ItemType<Magu>();
        }

        public override bool UseItem(Player player)
        {
            if (ZFunctions.DuringNewYear())
            {
                player.Heal(ZEROWORLD.SafeRandom.Next(10, 40), ZEROWORLD.SafeRandom.Next(2, 5));
                player.AddBuff(BuffID.WellFed, ZEROWORLD.SafeRandom.Next(20, 60));
                Item.NewItem(player.Center, ZEROWORLD.SafeRandom.Next(0, ItemLoader.ItemCount), noGrabDelay: true);
            }
            return true;
        }
    }
}