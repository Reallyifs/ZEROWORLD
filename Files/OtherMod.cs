using Terraria.ModLoader;
using static Terraria.ModLoader.ModLoader;

namespace ZEROWORLD.Files
{
    public static class OtherMod
    {
        internal static class LuoGod
        {
            public static Mod Instance => GetMod("LuoGod");
            public static bool Loaded => Instance != null;
        }

        internal static class Calamity
        {
            public static Mod Instance => GetMod("CalamityMod");
            public static bool Loaded => Instance != null;
        }
    }
}