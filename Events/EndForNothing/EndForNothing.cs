using System.Collections.Generic;
using ZEROWORLD.Files.Interfaces;

namespace ZEROWORLD.Events.EndForNothing
{
    public class EndForNothing : ILanguageBase,  IWorldSaveBase
    {
        public static bool Processing
        {
            get;
            private set;
        }

        public void LanguageLoad(List<(string, string, string[])> Texts)
        {
            Texts.Add(("EndForNothing.Start", "EndForNothing.开始", new string[]
            {
                "\"Hello, my FRIEND. Do you think you actually have a lot of time?\"",
                "“您好，我的 [朋友]。您是否以为您的时间其实很多呢？”"
            }));
        }

        public void WorldLoad(Dictionary<string, object> pairs)
        {
            Processing = (bool)pairs["EndForNothing.Processing"];
        }

        public void WorldSave(Dictionary<string, object> pairs)
        {
            pairs.Add("EndForNothing.Processing", Processing);
        }
    }
}