using System.Collections.Generic;

namespace ZEROWORLD.Files.Interfaces
{
    public interface IWorldSaveBase
    {
        /// <summary>
        /// 直接 pairs.Add("key", value); 即可
        /// </summary>
        /// <param name="pairs"></param>
        void WorldSave(Dictionary<string, object> pairs);

        /// <summary>
        /// 直接 value = (value Type)pairs["key"]; 即可
        /// </summary>
        /// <param name="pairs"></param>
        void WorldLoad(Dictionary<string, object> pairs);
    }
}