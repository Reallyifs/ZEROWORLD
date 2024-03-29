﻿using System;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using ZEROWORLD.Files;

namespace ZEROWORLD.Items.ExtendMode
{
    /// <summary>
    /// 古老的羊皮纸
    /// </summary>
    public sealed class FuruiYohishi : ZItem
    {
        protected override void OwnerDefaults()
        {
            item.rare = ExtendItemRare;
            item.maxStack = 1;
        }

        protected override void OwnerDisplay(GameCulture culture, ref bool support, ref string displayName, ref string displayTooltip)
        {
            if (culture == GameCulture.Chinese)
            {
                support = true;
                displayName = "古老的羊皮纸";
            }
            else if (culture == GameCulture.English)
            {
                support = true;
                displayName = "Old parchment";
            }
        }

        protected override int OwnerListDefault(out float level, out Version version, out DateTime date)
        {
            date = new DateTime(2020, 11, 6);
            level = 0f;
            version = new Version(0, 1, 0, 0);
            return ModContent.ItemType<FuruiYohishi>();
        }

        public override bool CanUseItem(Player player) => !ZWorld.ZeroMode;

        public override bool UseItem(Player player)
        {
            ZWorld.extendMode |= ZID.ZeroMode;
            return true;
        }
    }
}