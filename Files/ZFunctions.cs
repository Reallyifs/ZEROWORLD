using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.ID;
using ZEROWORLD.Items;

namespace ZEROWORLD.Files
{
    public static class ZFunctions
    {
        public static Type GetInterface<T>(this Type type) => type.GetInterface(typeof(T).Name);

        public static bool Contains<T>(this T[] array, T item) => array.ToList().Contains(item);

        public static void NewText(this string text, byte R = 255, byte G = 255, byte B = 255)
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
                Main.NewText(text, R, G, B);
        }

        public static void NewText(this string text, Color textColor)
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
                Main.NewText(text, textColor);
        }

        public static Item ItemSetDefaults(int type, bool noMatCheck = false)
        {
            Item item = new Item();
            item.SetDefaults(type, noMatCheck);
            return item;
        }

        public static ZItemClass[] ToZItemClass(Item item)
        {
            List<ZItemClass> Classes = new List<ZItemClass>();
            if (item.magic)
                Classes.Add(ZItemClass.Magic);
            if (item.melee)
                Classes.Add(ZItemClass.Melee);
            if (item.ranged)
                Classes.Add(ZItemClass.Ranged);
            if (item.summon)
                Classes.Add(ZItemClass.Summon);
            if (item.thrown)
                Classes.Add(ZItemClass.Thrown);
            if (Classes.Count < 1)
                Classes.Add(ZItemClass.Default);
            return Classes.ToArray();
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