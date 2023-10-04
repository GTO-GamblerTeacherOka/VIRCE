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

        public static void SendDisplayName(string name)
        {
            var packetData = PacketCreator.DisplayNamePacket(name);
            
            Socket.Instance.Send(packetData);
        }

        public static void SendPosition(in UnityEngine.Vector3 position, in UnityEngine.Vector3 rotation)
        {
            var packetData = PacketCreator.PositionPacket(position, rotation);

            Socket.Instance.Send(packetData);
        }

        public static void SendAvatarData(string avatarId)
        {
            var packetData = PacketCreator.AvatarDataPacket(avatarId);
            
            Socket.Instance.Send(packetData);
        }

        public static void SendReaction(string uniqueId)
        {
            var packetData = PacketCreator.ReactionPacket(uniqueId);
            
            Socket.Instance.Send(packetData);
        }
    }
}
