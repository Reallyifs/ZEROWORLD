using System;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ZEROWORLD.Items
{
    public class MochidzukiAku : ZItem
    {
        protected override void OwnerDisplay(GameCulture culture, out bool support, out string displayName, out string displayTooltip)
        {
            if (culture == GameCulture.English)
            {
                support = true;
                displayName = "Mochizuki Ark";
                displayTooltip = "\"Here we offer our blessings and destiny.\"\n\"Unspeakable trouble.\"";
            }
            else if (culture == GameCulture.Chinese)
            {
                support = true;
                displayName = "Aku";
                displayTooltip = "“将其于斩杀的那一刻……”\n“跪下。”";
            }
            else
            {
                support = false;
                displayName = displayTooltip = "";
            }
        }

        protected override int OwnerListDefault(out float level, out Version version, out DateTime date)
        {
            date = new DateTime(2020, 9, 22);
            level = 18.7f;
            version = new Version(0, 1, 0, 1);
            return ModContent.ItemType<MochidzukiAku>();
        }

    }
}