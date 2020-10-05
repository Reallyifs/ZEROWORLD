using System;
using System.Linq;
using System.Reflection;

namespace ZEROWORLD.Files
{
    public static class ZFunctions
    {
        public static void ForEach<T>(this T[] array, Action<T> action) => array.ToList().ForEach(action);

        public static Delegate CreateDelegate<T>(this MethodInfo info) => info.CreateDelegate(typeof(T));

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