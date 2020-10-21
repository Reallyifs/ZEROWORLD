using System;
using Terraria;
using Terraria.ModLoader;
using ZEROWORLD.Files;

namespace ZEROWORLD.Items
{
    public class ZItemInfo
    {
        public readonly int Type;

        public float Level;
        public Version Version;
        public DateTime Date;

        public int Crit;
        public int Damage;
        public int UseTime;
        public bool CanEquipped;
        public bool Expert;
        public float Scale;
        public float KnockBack;
        public ZItemClass Class;

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

        public void CopyFrom(ZItemInfo another, bool itemProperty = true)
        {
            Date = another.Date;
            Level = another.Level;
            Version = another.Version;
            if (itemProperty)
            {
                Crit = another.Crit;
                Class = another.Class;
                Scale = another.Scale;
                Damage = another.Damage;
                Expert = another.Expert;
                UseTime = another.UseTime;
                KnockBack = another.KnockBack;
                CanEquipped = another.CanEquipped;
            }
        }

        public void SetDefaultByType(bool noMatCheck = false)
        {
            Item typeItem = ZFunctions.ItemSetDefaults(Type, noMatCheck);
            Crit = typeItem.crit;
            Class = ZFunctions.ToZItemClass(typeItem);
            Scale = typeItem.scale;
            Damage = typeItem.damage;
            Expert = typeItem.expert || typeItem.expertOnly;
            UseTime = Math.Max(typeItem.useTime, typeItem.useAnimation);
            KnockBack = typeItem.knockBack;
            CanEquipped = typeItem.accessory || typeItem.vanity;
        }

        public static ZItemInfo PasteFrom(ZItemInfo info)
        {
            ZItemInfo newInfo = new ZItemInfo(info.Type, info.Level, info.Version, info.Date);
            newInfo.SetDefaultByType();
            return newInfo;
        }

        public static ZItemInfo PasteFrom(int type)
        {
            ZItemInfo info = new ZItemInfo(type);
            if (ItemLoader.GetItem(type) != null && ZItemCollection.ItemCollection.ContainsKey(type))
                info.CopyFrom(ZItemCollection.ItemCollection[type]);
            return info;
        }
    }
}