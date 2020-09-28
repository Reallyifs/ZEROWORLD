using Terraria.ModLoader;
using static Terraria.ModLoader.ModLoader;

namespace ZEROWORLD.Files
{
    public static class OtherMod
    {
        public static Mod LuoGod => GetMod("LuoGod");
        public static bool LuoGodLoaded => LuoGod != null;

        public static Mod Calamity => GetMod("CalamityMod");
        public static bool CalamityLoaded => Calamity != null;
    }
}