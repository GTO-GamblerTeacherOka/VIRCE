using Networking;
using Protocol;
using Settings;

public static class Api
{
    public static void RoomEntry(PacketCreator.EntryType type)
    {
        var packetData = PacketCreator.EntryPacket(type, GameSetting.ModelPublishId);

       Socket.Instance.Send(packetData);
    }
}
