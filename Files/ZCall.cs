using System;
using System.Reflection;
using ZEROWORLD.Items;

namespace ZEROWORLD.Files
{
    /// <summary>
    /// 这里的每个方法都与 <see cref="Call(object[])"/>（即 <see cref="ZEROWORLD.Call(object[])"/>）作用相符<para></para>
    /// 即方法名为 args[0]，其余参数为 args[1]、args[2]……
    /// </summary>
    public class ZCall : FilesBase
    {
        internal static Type ZCallType;
        internal static MethodInfo[] ZCallMethods;

        public override void Load()
        {
            ZCallType = typeof(ZCall);
            ZCallMethods = ZCallType.GetMethods();
        }

        public override void Unload()
        {
            ZCallType = null;
            ZCallMethods = null;
        }

        public static object Call(object[] args)
        {
            if (args != null && args.Length >= 1 && args[0] is string)
            {
                switch ((args[0] as string).ToLower())
                {
                    case "iteminfo":
                        {
                            string itemName = args[1] as string;
                            if (!string.IsNullOrWhiteSpace(itemName))
                                return ItemInfo(itemName);
                            break;
                        }
                }
            }
            return null;
        }

        public static ZItemInfo ItemInfo(string itemName)
        {
            int internalID = ZEROWORLD.Instance.ItemType(itemName);
            if (ZItemCollection.ItemCollection.TryGetValue(internalID, out ZItemInfo info))
                return info;
            return null;
        }
    }
}