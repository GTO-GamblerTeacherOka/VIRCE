using Cysharp.Threading.Tasks;
using Networking;
using Settings;
using UnityEngine;

namespace Protocol
{
    public static class Api
    {
        public static async UniTask RoomEntry(PacketCreator.EntryType type)
        {
            var packetData = PacketCreator.EntryPacket(type, GameSetting.ModelPublishId);

            await Socket.Instance.Send(packetData);
        }

        public static async UniTask SendChat(string chat)
        {
            var packetData = PacketCreator.ChatPacket(chat);

            await Socket.Instance.Send(packetData);
        }

        public static async UniTask SendDisplayName(string name)
        {
            var packetData = PacketCreator.DisplayNamePacket(name);

            await Socket.Instance.Send(packetData);
        }

        public static async UniTask SendPosition(Vector3 position, Vector3 rotation)
        {
            var packetData = PacketCreator.PositionPacket(position, rotation);

            await Socket.Instance.Send(packetData);
        }

        public static void SendExit()
        {
            var packetData = PacketCreator.ExitPacket();

            Socket.Instance.SendSync(packetData);
        }

        public static async UniTask SendAvatarData(string avatarId)
        {
            var packetData = PacketCreator.AvatarDataPacket(avatarId);

            await Socket.Instance.Send(packetData);
        }

        public static async UniTask SendReaction(string uniqueId)
        {
            var packetData = PacketCreator.ReactionPacket(uniqueId);

            await Socket.Instance.Send(packetData);
        }
    }
}