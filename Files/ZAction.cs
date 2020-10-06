using Microsoft.Xna.Framework.Graphics;
using System;

namespace ZEROWORLD.Files
{
    public static class ZAction
    {
        internal static Action<SpriteBatch> TickDrawAction;
        internal static Action LoadAction;
        internal static Action UnloadAction;

        public static Action NewAction => new Action(delegate { });

        internal static void Initialize()
        {
            LoadAction = NewAction;
            TickDrawAction = new Action<SpriteBatch>(delegate { });
            UnloadAction = NewAction;
            ZEROWORLD.Assembly.GetTypes().ForEach(delegate (Type type)
            {
                if (type.IsClass)
                {
                    if (type.IsSubclassOf<FilesBase>())
                    {
                        LoadAction += type.GetMethod("Load").CreateDelegate<Action>();
                        TickDrawAction += type.GetMethod("TickDraw").CreateDelegate<Action<SpriteBatch>>();
                        UnloadAction += type.GetMethod("Unload").CreateDelegate<Action>();
                    }
                }
            });
        }

        internal static void Load() => LoadAction();

        internal static void TickDraw() => ZFunctions.SafeSpriteBatch(TickDrawAction);

        internal static void Unload() => UnloadAction();
    }
}