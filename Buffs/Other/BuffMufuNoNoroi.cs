using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ZEROWORLD.Buffs.Other
{
    /// <summary>
    /// 无风咒焰
    /// </summary>
    public class BuffMufuNoNoroi : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Windless Curseflame");
            DisplayName.AddTranslation(GameCulture.Chinese, "无风咒焰");
            Description.SetDefault("\"The silent rainbow is the most dazzling after the storm.\"\n" +
                "All damage increase 50% at latest\n" +
                "[Card] and [Melee] are additionally added with [" +
                Language.GetTextValue("BuffName.CursedInferno") +
                "] of 1/10 of the remaining time of this buff (at least 5 seconds)");
            Description.AddTranslation(GameCulture.Chinese, "“风雨过后无声的彩虹最为耀眼。”\n" +
                "所有攻击伤害最终加成50%\n" +
                "[卡牌] 与 [近战] 额外附加此 buff 剩余时间 [1/10] 的 [" +
                Language.GetTextValue("BuffName.CursedInferno") +
                "] 效果（最少为5秒）");
            canBeCleared = false;
            longerExpertDebuff = true;
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.persistentBuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex) => buffIndex = 1;
    }
}