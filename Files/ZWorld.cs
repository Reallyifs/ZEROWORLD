using System.Collections.Generic;
using System.IO;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ZEROWORLD.Files
{
    public sealed class ZWorld : ModWorld
    {
        internal static bool ZeroMode;
        internal static byte ExtendMode;

        public override void Initialize()
        {
            ZeroMode = false;
            ExtendMode = 0;
        }

        public override TagCompound Save()
        {
            Dictionary<string, object> pairs = new Dictionary<string, object>
            {
                ["ZeroMode"] = ZeroMode,
                ["ExtendMode"] = ExtendMode
            };
            return new TagCompound() { ["ZWSL"] = pairs };
        }

        public override void Load(TagCompound tag)
        {
            Dictionary<string, object> pairs = tag.Get<Dictionary<string, object>>("ZWSL");
            ZeroMode = (bool)pairs["ZeroMode"];
            ExtendMode = (byte)pairs["ExtendMode"];
        }

        public override void NetSend(BinaryWriter writer)
        {
            writer.Write(ZeroMode);
            writer.Write(ExtendMode);
        }

        public override void NetReceive(BinaryReader reader)
        {
            ZeroMode = reader.ReadBoolean();
            ExtendMode = reader.ReadByte();
        }
    }
}