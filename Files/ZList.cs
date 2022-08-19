using System.Collections.Generic;
using Terraria.ID;
using Terraria.Localization;
using ZEROWORLD.Files.Interfaces;
using ZEROWORLD.Items;

namespace ZEROWORLD.Files
{
    public sealed class ZList : ILoadBase
    {
        public static List<int> vanillaBosses;
        public static List<ZItemClass> itemClasses;
        public static List<GameCulture> cultures;

        public void Load()
        {
            cultures = new List<GameCulture>()
            {
                GameCulture.Chinese,
                GameCulture.English,
                GameCulture.French,
                GameCulture.German,
                GameCulture.Italian,
                GameCulture.Polish,
                GameCulture.Portuguese,
                GameCulture.Russian,
                GameCulture.Spanish
            };
            itemClasses = new List<ZItemClass>()
            {
                ZItemClass.Default,
                ZItemClass.Card,
                ZItemClass.Magic,
                ZItemClass.Melee,
                ZItemClass.Ranged,
                ZItemClass.Summon,
                ZItemClass.Thrown
            };
            vanillaBosses = new List<int>()
            {
                NPCID.KingSlime,
                NPCID.EyeofCthulhu,
                NPCID.EaterofWorldsHead,
                NPCID.BrainofCthulhu,
                NPCID.QueenBee,
                NPCID.SkeletronHead,
                NPCID.WallofFlesh,
                NPCID.TheDestroyer,
                NPCID.Retinazer,
                NPCID.Spazmatism,
                NPCID.SkeletronPrime,
                NPCID.Plantera,
                NPCID.Golem,
                NPCID.CultistBoss,
                NPCID.LunarTowerVortex,
                NPCID.LunarTowerStardust,
                NPCID.LunarTowerNebula,
                NPCID.LunarTowerSolar,
                NPCID.MoonLordCore
            };
        }

        public void Unload()
        {
            cultures = null;
            itemClasses = null;
            vanillaBosses = null;
        }
    }
}