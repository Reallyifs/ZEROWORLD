using System;
using Terraria;
using Terraria.ModLoader;
using ZEROWORLD.Files;

namespace ZEROWORLD.Items
{
    public class ZItemInfo
    {
        private bool setAlready;
        private int _type;

        public int Type
        {
            get
            {
                if (!setAlready)
                    SetDefaultByType();
                return _type;
            }
        }

        public float Level
        {
            get;
            private set;
        }

        public Version Version
        {
            get;
            private set;
        }

        public DateTime Date
        {
            get;
            private set;
        }

        public int width;
        public int height;
        public ZItemClass classes;
        public int damage;
        public int crit;
        public float knockBack;
        public float scale;
        public int useStyle;
        public int useTime;
        public int maxStack;
        public bool canEquipped;
        public bool expert;

        public ZItemInfo(int type)
        {
            _type = type;
        }

        public ZItemInfo(int type, float level, Version version, DateTime date) : this(type)
        {
            Date = date;
            Level = level;
            Version = version;
        }

        public void PasteFrom(ZItemInfo another, bool itemProperty = true)
        {
            if (another == null)
                throw new ArgumentNullException("another");
            _type = another.Type;
            Date = another.Date;
            Level = another.Level;
            Version = another.Version;
            if (itemProperty)
            {
                width = another.width;
                height = another.height;
                classes = another.classes;
                damage = another.damage;
                crit = another.crit;
                knockBack = another.knockBack;
                scale = another.scale;
                useStyle = another.useStyle;
                useTime = another.useTime;
                maxStack = another.maxStack;
                canEquipped = another.canEquipped;
                expert = another.expert;
            }
        }

        public void PasteFrom(uint type)
        {
            if (ItemLoader.GetItem((int)type) == null || !ZItemCollection.itemCollection.TryGetValue((int)type, out ZItemInfo value))
            {
                throw new ArgumentException("Invalid argument. Possibly:\n" +
                    "This is not an item in ZEROWORLD\n" +
                    "This ZItemInfo is not loaded");
            }
            PasteFrom(value);
        }

        public override bool Equals(object obj)
        {
            ZItemInfo another = obj as ZItemInfo;
            if (another == null)
                return false;
            if (_type == another.Type && Level == another.Level && Version == another.Version)
                return Date == another.Date;
            return false;
        }

        public bool Equals(ZItemInfo obj)
        {
            if (obj == null)
                return false;
            if (_type == obj.Type && Level == obj.Level && Version == obj.Version)
                return Date == obj.Date;
            return false;
        }

        public override int GetHashCode()
        {
            return _type.GetHashCode() | Level.GetHashCode() | Version.GetHashCode() | Date.GetHashCode();
        }

        public void SetDefaultByType(bool noMatCheck = false)
        {
            Item typeItem = ZFunctions.ItemSetDefaults(_type, noMatCheck);
            width = typeItem.width;
            height = typeItem.height;
            classes = ZFunctions.ToZItemClass(typeItem);
            damage = typeItem.damage;
            crit = typeItem.crit;
            knockBack = typeItem.knockBack;
            scale = typeItem.scale;
            useStyle = typeItem.useStyle;
            useTime = Math.Max(typeItem.useTime, typeItem.useAnimation);
            maxStack = typeItem.maxStack;
            canEquipped = typeItem.accessory || typeItem.vanity;
            expert = typeItem.expert || typeItem.expertOnly;
            setAlready = true;
        }

        public static ZItemInfo CopyFrom(ZItemInfo info)
        {
            ZItemInfo newInfo = new ZItemInfo(info.Type, info.Level, info.Version, info.Date);
            newInfo.SetDefaultByType();
            return newInfo;
        }

        public static bool operator ==(ZItemInfo info1, ZItemInfo info2) => info1?.Equals(info2) ?? (info2 is null);

        public static bool operator !=(ZItemInfo info1, ZItemInfo info2) => !(info1 == info2);
    }
}