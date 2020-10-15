using System;
using Terraria.ModLoader;
using ZEROWORLD.Items;

namespace ZEROWORLD.Files
{
    public class ZItemInfo
    {
        public readonly int Type;

        public float Level;
        public Version Version;
        public DateTime Date;

        public ZItemInfo(int type)
        {
            Type = type;
        }

        public ZItemInfo(int type, float level, Version version, DateTime date) : this(type)
        {
            Date = date;
            Level = level;
            Version = version;
        }

        public void Copy(ZItemInfo another)
        {
            Date = another.Date;
            Level = another.Level;
            Version = another.Version;
        }

        public static ZItemInfo ReadFrom(int type)
        {
            ZItemInfo info = new ZItemInfo(type);
            if (ItemLoader.GetItem(type) != null && ZItemCollection.ItemCollection.ContainsKey(type))
                info.Copy(ZItemCollection.ItemCollection[type]);
            return info;
        }
    }
}