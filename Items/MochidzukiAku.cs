using System;
using Terraria;
using Terraria.ModLoader;

namespace ZEROWORLD.Items
{
    public class MochidzukiAku : ZItem
    {
        protected override void OwnerListDefault(out int type, out float level, out Version version, out DateTime date)
        {
            date = new DateTime(2020, 9, 22);
            type = ModContent.ItemType<MochidzukiAku>();
            level = 18.7f;
            version = new Version(0, 1, 0, 1);
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