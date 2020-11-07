using System;
using System.Collections.Generic;
using System.Reflection;
using ZEROWORLD.Files.Interfaces;

namespace ZEROWORLD.Files
{
    public static class ZAction
    {
        public const BindingFlags FlagsAll = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance;

        internal static Action LoadAction;
        internal static Action UnloadAction;
        internal static Action<List<(string, string, string[])>> LanguageAction;

        public static Action NewAction => new Action(delegate { });

        internal static void Initialize()
        {
            LoadAction = NewAction;
            UnloadAction = NewAction;
            LanguageAction = new Action<List<(string, string, string[])>>(delegate { });
            ZEROWORLD.Assembly.GetTypes().ForEach(delegate (Type type)
            {
                if (type.IsClass)
                {
                    if (type.GetInterface<ILoadBase>() != null)
                    {
                        LoadAction += TypeDelegate<Action>("Load");
                        UnloadAction += TypeDelegate<Action>("Unload");
                    }
                    if (type.GetInterface<ILanguageBase>() != null)
                        LanguageAction += TypeDelegate<Action<List<(string, string, string[])>>>("LanguageLoad");
                }
                T TypeDelegate<T>(string name) where T : Delegate => type.GetMethod(name, FlagsAll)?.CreateDelegate<T>();
            });
        }

        internal static void LanguageLoad(List<(string, string, string[])> array) => LanguageAction(array);
    }
}