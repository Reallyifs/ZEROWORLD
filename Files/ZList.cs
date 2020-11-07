using System.Collections.Generic;
using Terraria.ID;
using Terraria.Localization;
using ZEROWORLD.Files.Interfaces;

namespace ZEROWORLD.Files
{
    public sealed class ZList : ILoadBase
    {
        public static List<int> VanillaBosses;
        public static List<GameCulture> Cultures;

        public void Load()
        {
            Cultures = new List<GameCulture>()
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
            VanillaBosses = new List<int>()
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
            Cultures = null;
            VanillaBosses = null;
        }
    }
}