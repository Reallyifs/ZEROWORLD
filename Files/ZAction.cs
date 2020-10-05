using System;
using System.Linq;

namespace ZEROWORLD.Files
{
    public static class ZAction
    {
        internal static Action LoadAction;
        internal static Action UnloadAction;

        public static Action NewAction => new Action(delegate { });

        internal static void Initialize()
        {
            LoadAction = NewAction;
            UnloadAction = NewAction;
            ZEROWORLD.Assembly.GetTypes().ForEach(delegate (Type type)
            {
                if (type.IsSubclassOf<FilesBase>())
                {
                    LoadAction += (Action)type.GetMethod("Load").CreateDelegate<Action>();
                    UnloadAction += (Action)type.GetMethod("Unload").CreateDelegate<Action>();
                }
            });
        }

        internal static void Load() => LoadAction();

        internal static void Unload() => UnloadAction();
    }
}