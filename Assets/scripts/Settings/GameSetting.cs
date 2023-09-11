using System.Net;
using VRoid;

namespace Settings
{
    public static class GameSetting
    {
        static GameSetting()
        {
            var ip = IPAddress.Parse("127.0.0.1");
            RemoteEndPoint = new IPEndPoint(ip, 5000);
        }

        public static IPEndPoint RemoteEndPoint { get; private set; }
        public static string UserName { get; private set; }
        public static string ModelId { get; private set; }
        public static string ModelPublishId { get; private set; }
        public static int RoomId { get; private set; }
        public static int UserId { get; private set; }

        public static void SetRemoteEndPoint(IPEndPoint endPoint)
        {
            RemoteEndPoint = endPoint;
        }

        public static void SetUserName(string userName)
        {
            UserName = userName;
        }

        public static void SetModelId(string modelId)
        {
            ModelId = modelId;
            Auth.MultiplayApi.PostDownloadLicenses(modelId, license => ModelPublishId = license.id, _ => { });
        }

        public static void SetRoomId(int roomId)
        {
            RoomId = roomId;
        }

        public static void SetUserId(int userId)
        {
            UserId = userId;
        }
    }
}