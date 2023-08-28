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
        
        public static byte[] EntryPacket(EntryType type)
        {
            var header = Parser.CreateHeader(Parser.Flag.RoomEntry, 0, 0);
            var body = (byte)type;
            return header.Concat(new [] { body });
        }
    }
}
