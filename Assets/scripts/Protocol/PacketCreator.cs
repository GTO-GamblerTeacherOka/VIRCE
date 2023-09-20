using System;
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
            byte[] id = BitConverter.GetBytes(CreateHash(chat));
            byte[] txt = Encoding.UTF8.GetBytes(chat);

            var header = Parser.CreateHeader(Parser.Flag.ChatData, GameSetting.UserId, GameSetting.RoomId);
            var data = header.Concat(id).Concat(txt);
            return data;
        }
        
        private static uint CreateHash(string str)
        {
            Message[] msgs = _chatManager.GetChatMessages();
            foreach (var msg in msgs)
            {
                var time = msg.Time;
                str += time;
            }

            const int mod = 65521;
            uint a = 1, b = 0;
            foreach (char c in str)
            {
                a = (a + c) % mod;
                b = (b + a) % mod;
            }

            return (b << 16) | a;
        }
        
    }
}
