using System;
using System.Text;
using Lobby.Chat;
using Settings;
using UniJSON;
using UnityEngine;


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
            var data = header.Concat(new[] { body });
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

        public static byte[] PositionPacket(in Vector3 position, in Vector3 rotation)
        {
            var header = Parser.CreateHeader(Parser.Flag.PositionData, GameSetting.UserId, GameSetting.RoomId);
            var body = new byte[24];
            var x = BitConverter.GetBytes(position.x);
            var y = BitConverter.GetBytes(position.y);
            var z = BitConverter.GetBytes(position.z);
            var angleX = BitConverter.GetBytes(rotation.x);
            var angleY = BitConverter.GetBytes(rotation.y);
            var angleZ = BitConverter.GetBytes(rotation.z);
            x.CopyTo(body, 0);
            y.CopyTo(body, 4);
            z.CopyTo(body, 8);
            angleX.CopyTo(body, 12);
            angleY.CopyTo(body, 16);
            angleZ.CopyTo(body, 20);
            var data = header.Concat(body);
            return data;
        }
        
        private static uint CreateHash(string str)
        {
            str += DateTime.Now;

            const int mod = 1029495;
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