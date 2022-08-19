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
        internal static Action OnEnterWorldAction;
        internal static Action<Dictionary<string, object>> WorldLoadAction;
        internal static Action<Dictionary<string, object>> WorldSaveAction;
        internal static Action<List<(string, string, string[])>> LanguageAction;

        public static Action NewAction => new Action(delegate { });

        internal static void Initialize()
        {
            typeof(ZAction).GetFields(FlagsAll).ForEach(delegate (FieldInfo info)
            {
                if (info.FieldType == typeof(Action))
                    info.SetValue(null, NewAction);
            });
            Action initializeAction = NewAction;
            LanguageAction = new Action<List<(string, string, string[])>>(delegate { });
            ZEROWORLD.Assembly.GetTypes().ForEach(delegate (Type type)
            {
                if (!type.IsAbstract && !type.IsInterface)
                {
                    if (type.IsClass)
                    {
                        ActionAdd(ref initializeAction, typeof(IInitializeBase), "Initialize");
                        ActionAdd(ref LoadAction, typeof(ILoadBase), "Load");
                        ActionAdd(ref UnloadAction, typeof(ILoadBase), "Unload");
                        ActionAdd(ref OnEnterWorldAction, typeof(IEnterWorldBase), "OnEnterWorld");
                        ActionAdd(ref WorldSaveAction, typeof(IWorldSaveBase), "WorldSave");
                        ActionAdd(ref WorldLoadAction, typeof(IWorldSaveBase), "WorldLoad");
                        ActionAdd(ref LanguageAction, typeof(ILanguageBase), "LanguageLoad");
                    }
                    void ActionAdd<T, T2>(ref T2 action, T interfaceType, string methodName) where T : Type where T2 : Delegate
                    {
                        if (type.GetInterface<T>() != null)
                        {
                            if (methodName != null)
                            {
                                MethodInfo info = type.GetMethod(methodName, FlagsAll) ?? throw new ArgumentNullException(methodName);
                                action = (T2)Delegate.Combine(action, info.CreateDelegate<T2>());
                            }
                        }
                    }
                }
            }, true, new Type[]
            {
                typeof(ZLanguage),
                typeof(ZDeveloperSetting),
                typeof(ZList),
                typeof(ZImage)
            });
            initializeAction.TryAction(ZEROWORLD.ThrowError);
        }

        internal static void LanguageLoad(List<(string, string, string[])> array) => LanguageAction(array);
    }
}