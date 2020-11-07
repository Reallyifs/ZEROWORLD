using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using ZEROWORLD.Files;
using ZEROWORLD.Files.Interfaces;

namespace ZEROWORLD.NPCs
{
    public class ZGlobalNPC : GlobalNPC, ILoadBase, ILanguageBase
    {
        private static Action<NPC> NPCLootZeroModeAction;

        public void Load()
        {
            NPCLootZeroModeAction = new Action<NPC>(delegate (NPC npc)
            {
                if (ZList.VanillaBosses.Contains(npc.type) && ZWorld.ExtendMode < 1)
                {
                    ZWorld.ExtendMode = 1;
                    ZLanguage.GetForNewText("ExtendMode为1", Color.OrangeRed, true);
                }
            });
        }

        public void Unload()
        {
            NPCLootZeroModeAction = null;
        }

        public void LanguageLoad(List<(string, string, string[])> Texts)
        {
            Texts.Add(("ExtendMode.ExtendTo1", "ExtendMode为1", new string[]
            {
                "The disaster might have started long ago? ...",
                "灾难可能早就开始了？……"
            }));
        }

        public override void NPCLoot(NPC npc)
        {
            if (ZWorld.ExtendMode == 1)
                NPCLootZeroModeAction(npc);
        }
    }
}