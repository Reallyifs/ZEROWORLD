using Microsoft.Xna.Framework.Graphics;
using ZEROWORLD.Files.Interfaces;

namespace ZEROWORLD.Files
{
    public sealed class ZImage : ILoadBase
    {
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

        public void Load()
        {
            QuestionMark = new Texture2D[]
            {
                TexturnInImages("Symbols/QuestionMarkBlack"),
                TexturnInImages("Symbols/QuestionMarkWhite"),
                TexturnInImages("Symbols/QuestionMarkOFBlack"),
                TexturnInImages("Symbols/QuestionMarkOFWhite")
            };
        }

        public void Unload()
        {
            QuestionMark = null;
        }

        private static Texture2D TexturnInImages(string path) => ZEROWORLD.Instance.GetTexture($"Images/{path}");
    }
}