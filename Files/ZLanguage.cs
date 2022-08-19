using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using ZEROWORLD.Files.Interfaces;

namespace ZEROWORLD.Files
{
    public class ZLanguage : ILoadBase
    {
        private static List<(string, string, string[])> Texts;
        private static Dictionary<string, string[]> TextValues;

        public void Load()
        {
            Texts = new List<(string, string, string[])>();
            TextValues = new Dictionary<string, string[]>();
            ZAction.LanguageAction(Texts);
            End();
            Texts = null;
        }

        public void Unload()
        {
            TextValues = null;
        }

        public static string GetForNewText(string internalCode, byte R = 255, byte G = 255, byte B = 255, bool chineseCode = false)
        {
            string textGet = Get(internalCode, chineseCode);
            textGet.NewText(R, G, B);
            return textGet;
        }

        public static string GetForNewText(string internalCode, Color textColor, bool chineseCode = false)
        {
            string textGet = Get(internalCode, chineseCode);
            textGet.NewText(textColor);
            return textGet;
        }

        public static string Get(string internalCode, bool chineseCode = false)
        {
            if (!string.IsNullOrWhiteSpace(internalCode))
            {
                if (!chineseCode)
                    return Language.GetTextValue($"Mods.ZEROWORLD.{internalCode}");
                else if (TextValues.ContainsKey(internalCode))
                    return TextValues[internalCode][GameCulture.Chinese.IsActive.ToInt()];
            }
            return string.Empty;
        }
        public static string Get(string internalCode, bool chineseCode = false, params object[] array)
        {
            if (!string.IsNullOrWhiteSpace(internalCode))
            {
                if (!chineseCode)
                    return Language.GetTextValue($"Mods.ZEROWORLD.{internalCode}", array);
                else if (TextValues.ContainsKey(internalCode))
                {
                    int index = GameCulture.Chinese.IsActive ? 1 : 0;
                    return string.Format(TextValues[internalCode][index], array);
                }
            }
            return string.Empty;
        }

        private static void End()
        {
            Texts.ForEach(delegate ((string, string, string[]) All)
            {
                ModTranslation translation = ZEROWORLD.Instance.CreateTranslation(All.Item1);
                translation.SetDefault(All.Item3[0]);
                translation.AddTranslation(GameCulture.Chinese, All.Item3[1]);
                ZEROWORLD.Instance.AddTranslation(translation);

                TextValues.Add(All.Item2, All.Item3);
            });
        }
    }
}