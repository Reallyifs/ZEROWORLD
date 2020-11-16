using Microsoft.Xna.Framework.Graphics;
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
            MusicIcon = new Texture2D[]
            {
                TexturnInImages("MusicIcon/AlwaysFrosted"),
                TexturnInImages("MusicIcon/CloudyNoConfuse"),
                TexturnInImages("MusicIcon/SunsetByTheWarmSea")
            };
            QuestionMark = new Texture2D[]
            {
                TexturnInImages("Symbols/QuestionMarkBlack"),
                TexturnInImages("Symbols/QuestionMarkWhite"),
                TexturnInImages("Symbols/QuestionMarkOFBlack"),
                TexturnInImages("Symbols/QuestionMarkOFWhite")
            };
            Loaded = true;
        }

        public void Unload()
        {
            MusicIcon = null;
            QuestionMark = null;
            Loaded = false;
        }

        private static Texture2D TexturnInImages(string path) => ZEROWORLD.Instance.GetTexture($"Images/{path}");
    }
}