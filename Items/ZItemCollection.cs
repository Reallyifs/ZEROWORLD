using System;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace ZEROWORLD.Items
{
    public static class ZItemCollection
    {
        internal static Dictionary<int, (float, Version, DateTime)> ItemCollection;

        public delegate void ItemCollectionDelegate(out int type, out float level, out Version version, out DateTime date);

        internal static void Initialize()
        {
            ItemCollection = new Dictionary<int, (float, Version, DateTime)>();
        }

        internal static void AddItemCollection(ItemCollectionDelegate item)
        {
            item(out int type, out float level, out Version version, out DateTime date);
            date = new DateTime() > date ? new DateTime() : date;
            level = Math.Max(0, level);
            version = new Version() > version ? new Version() : version;
            if (type < ItemLoader.ItemCount && !ItemCollection.ContainsKey(type))
                ItemCollection.Add(type, (level, version, date));
        }
    }
}