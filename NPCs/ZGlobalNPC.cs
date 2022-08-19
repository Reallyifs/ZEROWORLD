using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ZEROWORLD.Buffs.Other;
using ZEROWORLD.Files;
using ZEROWORLD.Files.Interfaces;
using ZEROWORLD.Items.Other;

namespace ZEROWORLD.NPCs
{
    public class ZGlobalNPC : GlobalNPC, ILoadBase, ILanguageBase
    {
        private static Action<NPC> NPCLootAction;
        private static Action<NPC> NPCLootZeroModeAction;

        public void Load()
        {
            NPCLootAction = new Action<NPC>(delegate (NPC npc)
            {
                if (ZFunctions.DuringNewYear())
                {
                    if (ZEROWORLD.SafeRandom.Next(0, 1000).Between(0, 10))
                    {
                        Item.NewItem(npc.getRect(), ModContent.ItemType<Magu>());
                    }
                }
            });
            NPCLootZeroModeAction = new Action<NPC>(delegate (NPC npc)
            {
                if (ZList.vanillaBosses.Contains(npc.type) && !ZWorld.downedFirstBoss)
                {
                    ZWorld.downedFirstBoss = true;
                    ZLanguage.GetForNewText("ZeroMode.第一个Boss被击败", Color.OrangeRed, true);
                }
            });
        }

        public void Unload()
        {
            NPCLootAction = null;
            NPCLootZeroModeAction = null;
        }

        public void LanguageLoad(List<(string, string, string[])> Texts)
        {
            Texts.Add(("ExtendMode.ZeroMode.DownedFirstBoss", "ZeroMode.第一个Boss被击败", new string[]
            {
                "The disaster might have started long ago? ...",
                "灾难可能早就开始了？……"
            }));
        }

        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            if (player.HasBuff<BuffMufuNoNoroi>())
                damage = (int)(damage * 1.5);
        }

        public override void NPCLoot(NPC npc)
        {
            NPCLootAction(npc);
            if (ZWorld.ZeroMode)
                NPCLootZeroModeAction(npc);
        }

        public override void OnHitByItem(NPC npc, Player player, Item item, int damage, float knockback, bool crit)
        {
            if (player.HasBuff<BuffMufuNoNoroi>() && (item.melee || item.OwnerItem().cardClass != Items.ZItemClass.Default))
            {
                int index = player.buffType.FirstOrDefault((int type) => type == ModContent.BuffType<BuffMufuNoNoroi>());
                npc.AddBuff(BuffID.CursedInferno, player.buffTime[index] >= 300 ? player.buffTime[index] : 300);
            }
        }
    }
}