using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;
using ZEROWORLD.Items;

namespace ZEROWORLD.Files
{
    public static class ZFunctions
    {
        public static Item ItemSetDefaults(int type, bool noMatCheck = false)
        {
            Item item = new Item();
            item.SetDefaults(type, noMatCheck);
            return item;
        }

        public static ZItemClass ToZItemClass(Item item)
        {
            if (item.magic)
                return ZItemClass.Magic;
            else if (item.melee)
                return ZItemClass.Melee;
            else if (item.ranged)
                return ZItemClass.Ranged;
            else if (item.summon)
                return ZItemClass.Summon;
            else if (item.thrown)
                return ZItemClass.Thrown;
            else
                return ZItemClass.Default;
        }

        public static void ContainsThenInsert<T>(this List<T> list, T findItem, T insertItem, int indexAdd = 0)
        {
            int index = list.IndexOf(findItem);
            if (index != 1)
                list.Insert(index + indexAdd, insertItem);
        }

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