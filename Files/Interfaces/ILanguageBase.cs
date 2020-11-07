using System.Collections.Generic;

namespace ZEROWORLD.Files.Interfaces
{
    public interface ILanguageBase
    {
        void LanguageLoad(List<(string, string, string[])> Texts);
    }
}