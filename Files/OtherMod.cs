using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace ZEROWORLD.Files
{
    public static class OtherMod
    {
        internal static class LuoGod
        {
            public static Mod Instance => ModLoader.GetMod("LuoGod");
            public static bool Loaded => Instance != null;
            public static Texture2D GetTexture(string name) => Instance.GetTexture(name);
        }

        internal static class Calamity
        {
            public static Mod Instance => ModLoader.GetMod("CalamityMod");
            public static bool Loaded => Instance != null;
            public static Texture2D GetTexture(string name) => Instance.GetTexture(name);
        }
    }
}