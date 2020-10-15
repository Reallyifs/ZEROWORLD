using Microsoft.Xna.Framework.Graphics;

namespace ZEROWORLD.Files
{
    public class ZImage : FilesBase
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

        public override void Load()
        {
            QuestionMark = new Texture2D[]
            {
                TexturnInImages("Symbols/QuestionMarkBlack"),
                TexturnInImages("Symbols/QuestionMarkWhite"),
                TexturnInImages("Symbols/QuestionMarkOFBlack"),
                TexturnInImages("Symbols/QuestionMarkOFWhite")
            };
        }

        public override void Unload()
        {
            QuestionMark = null;
        }

        private static Texture2D TexturnInImages(string path) => ZEROWORLD.Instance.GetTexture($"Images/{path}");
    }
}