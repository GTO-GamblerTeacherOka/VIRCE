using Networking;
using Settings;

namespace Protocol
{
    public static class Api
    {
        public static void RoomEntry(PacketCreator.EntryType type)
        { 
            var packetData = PacketCreator.EntryPacket(type, GameSetting.ModelPublishId);

            Socket.Instance.Send(packetData);
        }

        public static void SendChat(string chat)
        {
            var packetData = PacketCreator.ChatPacket(chat);

            Socket.Instance.Send(packetData);
        }

        public static void SendUserName(string name)
        {
            var packetData = PacketCreator.UserNamePacket(name);
            
            Socket.Instance.Send(packetData);
        }
    }
}
