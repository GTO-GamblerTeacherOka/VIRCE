using System.Net;
using System.Net.Sockets;
using Cysharp.Threading.Tasks;
using Protocol;
using Settings;

namespace Networking
{
    public class Socket
    {
        private static Socket _instance;
        private UdpClient _client;
        private bool _isConnected;
        private IPEndPoint _localEp;
        private IPEndPoint _remoteEp;

        private Socket()
        {
            _client = new UdpClient(0);
            _localEp = (IPEndPoint)_client.Client.LocalEndPoint;
            _isConnected = true;
        }

        public static Socket Instance
        {
            get { return _instance ??= new Socket(); }
        }

        public void Open()
        {
            if (_isConnected is not false) return;
            _client = new UdpClient(0);
            _localEp = (IPEndPoint)_client.Client.LocalEndPoint;
            _isConnected = true;
        }

        public async UniTask Close()
        {
            _isConnected = false;
            await UniTask.Delay(1000);
            _client.Close();
            _client = null;
        }

        public void StartRecv()
        {
            UniTask.Void(async () =>
            {
                while (_isConnected)
                {
                    var res = await _client.ReceiveAsync();
                    var data = res.Buffer;
                    Buffer.Instance.InputBuf(data);
                }
            });
        }

        public void Send(byte[] data)
        {
            _remoteEp = GameSetting.RemoteEndPoint;
            _client.SendAsync(data, data.Length, _remoteEp);
        }
    }
}