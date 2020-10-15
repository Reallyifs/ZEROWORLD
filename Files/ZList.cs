using System.Collections.Generic;
using Terraria.Localization;

namespace ZEROWORLD.Files
{
    public class ZList : FilesBase
    {
        public static List<GameCulture> Cultures;

        public override void Load()
        {
            Cultures = new List<GameCulture>()
            {
                null,
                GameCulture.Chinese,
                GameCulture.English,
                GameCulture.French,
                GameCulture.German,
                GameCulture.Italian,
                GameCulture.Polish,
                GameCulture.Portuguese,
                GameCulture.Russian,
                GameCulture.Spanish
            };
        }

        public override void Unload()
        {
            Cultures = null;
        }
    }
}