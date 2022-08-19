using Microsoft.Xna.Framework.Graphics;
using System.Reflection;
using ZEROWORLD.Files.Interfaces;

namespace ZEROWORLD.Files
{
    public sealed class ZImage : ILoadBase
    {
        public static bool Loaded
        {
            get;
            private set;
        }

        /// <summary>
        /// 0：无框黑<para></para>
        /// 1：无框白<para></para>
        /// 2：有框黑<para></para>
        /// 3：有框白
        /// </summary>
        internal static Texture2D[] QuestionMark
        {
            get;
            private set;
        }

        /// <summary>
        /// 0：Always Frosted<para></para>
        /// 1：Cloudy, no Confuse<para></para>
        /// 2：Sunset by the Warm sea
        /// </summary>
        internal static Texture2D[] MusicIcon
        {
            get;
            private set;
        }

        public void Load()
        {
            //MusicIcon = new Texture2D[]
            //{
            //    TextureInImages("MusicIcon/AlwaysFrosted"),
            //    TextureInImages("MusicIcon/CloudyNoConfuse"),
            //    TextureInImages("MusicIcon/SunsetByTheWarmSea")
            //};
            QuestionMark = new Texture2D[]
            {
                TextureInImages("Symbols/QuestionMarkBlack"),
                TextureInImages("Symbols/QuestionMarkWhite"),
                TextureInImages("Symbols/QuestionMarkOFBlack"),
                TextureInImages("Symbols/QuestionMarkOFWhite")
            };
            Loaded = true;
        }

        public void Unload()
        {
            foreach (FieldInfo info in typeof(ZImage).GetFields(BindingFlags.Static | BindingFlags.NonPublic))
            {
                if (info.Name != "Loaded")
                    info.SetValue(null, null);
            }
            Loaded = false;
        }

        private static Texture2D TextureInImages(string path) => ZEROWORLD.Instance.GetTexture($"Images/{path}");
    }
}