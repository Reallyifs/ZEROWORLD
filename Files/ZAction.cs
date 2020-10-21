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
        public static Action<SpriteBatch> NewSpriteBatchAction => new Action<SpriteBatch>(delegate { });

        internal static void Initialize()
        {
            LoadAction = NewAction;
            UnloadAction = NewAction;
            TickDrawAction = NewSpriteBatchAction;
            PostDrawAction = NewSpriteBatchAction;
            ZEROWORLD.Assembly.GetTypes().ForEach(delegate (Type type)
            {
                if (type.IsClass)
                {
                    if (type.IsSubclassOf<FilesBase>())
                    {
                        LoadAction += TypeDelegate("Load");
                        UnloadAction += TypeDelegate("Unload");
                        TickDrawAction += TypeDelegateSpriteBatch("TickDraw");
                        PostDrawAction += TypeDelegateSpriteBatch("PostDraw");
                    }
                }
                Action TypeDelegate(string name) => type.GetMethod(name)?.CreateDelegate<Action>() ?? NewAction;
                Action<SpriteBatch> TypeDelegateSpriteBatch(string name)
                {
                    return type.GetMethod(name)?.CreateDelegate<Action<SpriteBatch>>() ?? NewSpriteBatchAction;
                }
            });
        }

        internal static void TickDraw() => ZFunctions.SafeSpriteBatch(TickDrawAction);
    }
}