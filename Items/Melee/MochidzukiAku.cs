using System;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ID;

namespace ZEROWORLD.Items.Melee
{
    /// <summary>
    /// 望月方舟
    /// </summary>
    public sealed class MochidzukiAku : ZItem
    {
        protected override void OwnerDefaults()
        {
            item.melee = true;
            item.damage = 269;
            item.crit = 1;
            item.knockBack = 0.6f;
            item.useStyle = ItemUseStyleID.SwingThrow;
        }

        protected override void OwnerDisplay(GameCulture culture, ref bool support, ref string displayName, ref string displayTooltip)
        {
            if (culture == GameCulture.Chinese)
            {
                support = true;
                displayName = "望月方舟";
                displayTooltip = "“将其于斩杀的那一刻……”\n“跪下。”";
            }
            else if (culture == GameCulture.English)
            {
                support = true;
                displayName = "Mochizuki Ark";
                displayTooltip = "\"Here we offer our blessings and destiny.\"\n\"Unspeakable trouble.\"";
            }
        }

        protected override void OwnerFixedCrit(ref double hardMode, ref double expertMode, ref double zeroMode)
        {
            hardMode = 1.2f;
            expertMode = 1.6f;
            zeroMode = 2f;
        }

        protected override void OwnerFixedKnockBack(ref double hardMode, ref double expertMode, ref double zeroMode)
        {
            hardMode = 0.8f;
            expertMode = 1.3f;
            zeroMode = 0.9f;
        }

        protected override int OwnerListDefault(out float level, out Version version, out DateTime date)
        {
            date = new DateTime(2020, 9, 22);
            level = 18.7f;
            version = new Version(0, 1, 0, 0);
            return ModContent.ItemType<MochidzukiAku>();
        }
    }
}