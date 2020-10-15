using Microsoft.Xna.Framework.Graphics;
using System;

namespace ZEROWORLD.Files
{
    public static class ZAction
    {
        internal static Action LoadAction;
        internal static Action UnloadAction;
        internal static Action<SpriteBatch> TickDrawAction;
        internal static Action<SpriteBatch> PostDrawAction;

        public static Action NewAction => new Action(delegate { });

        internal static void Initialize()
        {
            LoadAction = NewAction;
            UnloadAction = NewAction;
            TickDrawAction = new Action<SpriteBatch>(delegate { });
            PostDrawAction = new Action<SpriteBatch>(delegate { });
            ZEROWORLD.Assembly.GetTypes().ForEach(delegate (Type type)
            {
                if (type.IsClass)
                {
                    if (type.IsSubclassOf<FilesBase>())
                    {
                        LoadAction += type.GetMethod("Load").CreateDelegate<Action>();
                        UnloadAction += type.GetMethod("Unload").CreateDelegate<Action>();
                        TickDrawAction += type.GetMethod("TickDraw").CreateDelegate<Action<SpriteBatch>>();
                        PostDrawAction += type.GetMethod("PostDraw").CreateDelegate<Action<SpriteBatch>>();
                    }
                }
            });
        }

        internal static void TickDraw() => ZFunctions.SafeSpriteBatch(TickDrawAction);
    }
}