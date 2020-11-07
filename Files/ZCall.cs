using ZEROWORLD.Items;

namespace ZEROWORLD.Files
{
    /// <summary>
    /// 这里的每个方法都与 <see cref="Call(object[])"/>（即 <see cref="ZEROWORLD.Call(object[])"/>）作用相符<para></para>
    /// 即方法名为 args[0]，其余参数为 args[1]、args[2]……
    /// </summary>
    public static class ZCall
    {
        public static object Call(object[] args)
        {
            if (args != null && args.Length >= 2 && args[0] is string)
            {
                switch ((args[0] as string).ToLower())
                {
                    case "iteminfo":
                        return ItemInfo(args[1] as string);
                }
            }
            return null;
        }

        public static ZItemInfo ItemInfo(string itemName)
        {
            if (!string.IsNullOrWhiteSpace(itemName))
            {
                int internalID = ZEROWORLD.Instance.ItemType(itemName);
                if (ZItemCollection.ItemCollection.TryGetValue(internalID, out ZItemInfo info))
                    return info;
            }
            return null;
        }
    }
}