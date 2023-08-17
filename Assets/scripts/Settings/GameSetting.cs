using System.Net;

namespace Settings
{
    public static class GameSetting
    {
        public static IPEndPoint RemoteEndPoint { get; private set; }
        public static string UserName { get; private set; }
        public static string ModelId { get; private set; }

        static GameSetting()
        {
            var ip = IPAddress.Parse("127.0.0.1");
            RemoteEndPoint = new IPEndPoint(ip, 5000);
        }
        
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
        }
    }
}
