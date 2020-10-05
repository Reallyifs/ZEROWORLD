using System;
using Terraria.ModLoader;

namespace ZEROWORLD.Items
{
    public class MochidzukiAku : ZItem
    {
        protected override int OwnerListDefault(out float level, out Version version, out DateTime date)
        {
            date = new DateTime(2020, 9, 22);
            level = 18.7f;
            version = new Version(0, 1, 0, 1);
            return ModContent.ItemType<MochidzukiAku>();
        }

        protected override void OwnerStaticDefault(out string DefaultName, out string ChineseName, out string DefaultTooltip,
            out string ChineseTooltip)
        {
            ChineseName = "Aku";
            DefaultName = "Mochizuki Ark";
            ChineseTooltip = "“将其于斩杀的那一刻……”\n“跪下。”";
            DefaultTooltip = "\"Here we offer our blessings and destiny.\"\n\"Unspeakable trouble.\"";
        }
    }
}