using System.Text;
using Lobby.Chat;
using Settings;
using UniJSON;
using UnityEngine.iOS;


namespace Protocol
{
    public static class PacketCreator
    {
        private static ChatManager _chatManager;
        
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
        
        private static uint CreateHash(string str)
        {
            const int mod = 65521;
            uint a = 1, b = 0;
            foreach (char c in str)
            {
                a = (a + c) % mod;
                b = (b + a) % mod;
            }

            return (b << 16) | a;
        }

        private static uint CreateUniqueId()
        {
            Message[] msgs = _chatManager.GetChatMessages();
            uint uniqueId = 0;

            foreach (var msg in msgs)
            {
                var beforeUniqueId = string.Empty;
                var id = msg.Id;
                var time = msg.Time;

                beforeUniqueId += id;
                beforeUniqueId += time;

                uniqueId = CreateHash(beforeUniqueId);
            }

            return uniqueId;
        }
    }
}
