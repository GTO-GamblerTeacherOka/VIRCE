using System.Net;

namespace Settings
{
    public static class GameSetting
    {
        public static IPEndPoint RemoteEndPoint { get; private set; }
        public static string UserName { get; private set; }
        public static string ModelId { get; private set; }
        
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
