using System.Text;
using Settings;
using UniJSON;
using UnityEngine.iOS;

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
            data = data.Concat(Encoding.UTF8.GetBytes(modelID));
            return data;
        }

        public static byte[] ChatPacket(string chat)
        {
            var header = Parser.CreateHeader(Parser.Flag.ChatData, GameSetting.UserId, GameSetting.RoomId);
            var data = header.Concat(Encoding.UTF8.GetBytes(chat));
            return data;
        }
    }
}
