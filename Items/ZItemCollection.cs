using System;
using System.Collections.Generic;
using Terraria.ModLoader;
using ZEROWORLD.Files;

namespace ZEROWORLD.Items
{
    public class ZItemCollection : FilesBase
    {
        internal static Dictionary<int, (float, Version, DateTime)> ItemCollection;

        public delegate int ItemCollectionDelegate(out float level, out Version version, out DateTime date);

        public override void Load()
        {
            ItemCollection = new Dictionary<int, (float, Version, DateTime)>();
        }

        public override void Unload()
        {
            ItemCollection = null;
        }

        internal static void AddItemCollection(ItemCollectionDelegate item)
        {
            int type = item(out float level, out Version version, out DateTime date);
            date = new DateTime() > date ? new DateTime() : date;
            level = Math.Max(0, level);
            version = new Version() > version ? new Version() : version;
            if (type <= ItemLoader.ItemCount && !ItemCollection.ContainsKey(type))
                ItemCollection.Add(type, (level, version, date));
        }
    }
}