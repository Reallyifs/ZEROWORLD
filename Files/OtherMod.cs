using Terraria.ModLoader;

namespace ZEROWORLD.Files
{
    public class OtherMod<T> where T : Mod
    {
        public T Instance
        {
            get;
            private set;
        }

        public bool Loaded => Instance != null;

        public OtherMod() => Instance = (T)ModLoader.GetMod(typeof(T).Name);

        public object Call(params object[] array) => Instance?.Call(array);
    }
}