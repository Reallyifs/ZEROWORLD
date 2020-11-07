using System;
using System.Collections.Generic;
using Terraria.ModLoader;
using ZEROWORLD.Files.Interfaces;

namespace ZEROWORLD.Items
{
    public class ZItemCollection : ILoadBase
    {
        internal static Dictionary<int, ZItemInfo> ItemCollection;

        public delegate int ItemCollectionDelegate(out float level, out Version version, out DateTime date);

        public void Load()
        {
            ItemCollection = new Dictionary<int, ZItemInfo>();
        }

        public void Unload()
        {
            ItemCollection = null;
        }

        internal static void AddItemCollection(ItemCollectionDelegate item)
        {
            int type = item(out float level, out Version version, out DateTime date);
            if (CheckMark(ref level, ref version, ref date) && type <= ItemLoader.ItemCount && !ItemCollection.ContainsKey(type))
            {
                ZItemInfo itemInfo = new ZItemInfo(type, level, version, date);
                itemInfo.SetDefaultByType();
                ItemCollection.Add(type, itemInfo);
            }
        }

        private static bool CheckMark(ref float level, ref Version version, ref DateTime date, bool passAlways = true)
        {
            float levelNew = Math.Max(0, level);
            Version versionNew = new Version() > version ? new Version() : version;
            DateTime dateNew = new DateTime() > date ? new DateTime() : date;
            if (!passAlways)
                return levelNew == 0 || versionNew == new Version() || dateNew == new DateTime();
            else
            {
                if (levelNew == 0)
                    level = levelNew;
                if (versionNew == new Version())
                    version = versionNew;
                if (dateNew == new DateTime())
                    date = dateNew;
                return true;
            }
        }
    }
}