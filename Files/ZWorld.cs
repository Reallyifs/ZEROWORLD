using System.Collections.Generic;
using System.IO;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ZEROWORLD.Files
{
    public sealed class ZWorld : ModWorld
    {
        internal static int extendMode;
        internal static bool downedFirstBoss;

        internal static bool ZeroMode => (extendMode & 1) == 1;

        public override void Initialize()
        {
            extendMode = 0;
            downedFirstBoss = false;
        }

        public override TagCompound Save()
        {
            Dictionary<string, object> pairs = new Dictionary<string, object>
            {
                ["ExtendMode"] = extendMode,
                ["DownedFirstBoss"] = downedFirstBoss
            };
            ZAction.WorldSaveAction(pairs);
            return new TagCompound() { ["ZWSL"] = pairs };
        }

        public override void Load(TagCompound tag)
        {
            Dictionary<string, object> pairs = tag.Get<Dictionary<string, object>>("ZWSL");
            extendMode = (byte)pairs["ExtendMode"];
            downedFirstBoss = (bool)pairs["DownedFirstBoss"];
            ZAction.WorldLoadAction(pairs);
        }

        public override void NetSend(BinaryWriter writer)
        {
            writer.Write(extendMode);
            writer.Write(downedFirstBoss);
        }

        public override void NetReceive(BinaryReader reader)
        {
            extendMode = reader.ReadByte();
            downedFirstBoss = reader.ReadBoolean();
        }
    }
}