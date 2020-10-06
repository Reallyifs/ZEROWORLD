using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using System.Reflection;
using Terraria;

namespace ZEROWORLD.Files
{
    public static class ZFunctions
    {
        public static void SafeSpriteBatch(Action<SpriteBatch> action)
        {
            SpriteBatch defaultSB = Main.spriteBatch;
            try
            {
                Main.spriteBatch.End();
            }
            finally
            {
                Main.spriteBatch.Begin();
            }
            action(Main.spriteBatch);
            Main.spriteBatch = defaultSB;
        }

        public static void ForEach<T>(this T[] array, Action<T> action) => array.ToList().ForEach(action);

        public static T CreateDelegate<T>(this MethodInfo info) where T : Delegate => (T)info.CreateDelegate(typeof(T));

        public static bool IsSubclassOf<T>(this Type type) => type.IsSubclassOf(typeof(T));

        public static bool Is(this string text, params string[] array)
        {
            foreach (string line in array)
            {
                if (text == line)
                    return true;
            }
            return false;
        }
    }
}