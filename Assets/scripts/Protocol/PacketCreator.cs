using UniJSON;

namespace Protocol
{
    public static class PacketCreator
    {
        public enum EntryType
        {
            Lobby = 0,
            MiniGame = 1
        }
        
        public static byte[] EntryPacket(EntryType type, string modelID)
        {
            var header = Parser.CreateHeader(Parser.Flag.RoomEntry, 0, 0);
            var body = (byte)type;
            var data = header.Concat(new [] { body });
            data = data.Concat(Encording.UTF8.GetBytes(modelID));
            return data;
        }
    }
}
