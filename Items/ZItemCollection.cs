using System;
using System.Collections.Generic;
using Terraria.ModLoader;
using ZEROWORLD.Files;
using ZEROWORLD.Files.Interfaces;

namespace ZEROWORLD.Items
{
    public class ZItemCollection : ILoadBase
    {
        internal static Dictionary<int, ZItemInfo> itemCollection;

        public delegate int ItemCollectionDelegate(out float level, out Version version, out DateTime date);

        public void Load()
        {
            itemCollection = new Dictionary<int, ZItemInfo>();
        }

        public void Unload()
        {
            itemCollection = null;
        }

        internal static void AddItemCollection(ItemCollectionDelegate item)
        {
            int type = item(out float level, out Version version, out DateTime date);
            if (CheckMask(type) && CheckMark(ref level, ref version, ref date))
                itemCollection.Add(type, ZFunctions.ZItemInfoSetDefaults(type, level, version, date));
        }

        private static bool CheckMark(ref float level, ref Version version, ref DateTime date, bool passAlways = true)
        {
            if (passAlways)
            {
                level = Math.Max(0, level);
                version = new Version() > version ? new Version() : version;
                date = new DateTime() > date ? new DateTime() : date;
                return true;
            }
            return level >= 0 || version >= new Version() || date >= new DateTime();
        }

        private static bool CheckMask(int type)
        {
            ModItem item = ModContent.GetModItem(type);
            return item != null && item.mod == ZEROWORLD.Instance && !itemCollection.ContainsKey(type);
        }
    }
}