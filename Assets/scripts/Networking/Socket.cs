using System;
using System.Net;
using System.Net.Sockets;
using Cysharp.Threading.Tasks;
using Buffer = Protocol.Buffer;

namespace Networking
{
    public class Socket
    {
        private static Socket _instance;

        public static Socket Instance
        {
            get
            {
                return _instance ??= new Socket();
            }
        }

        private UdpClient _client;
        private IPEndPoint _remoteEp;
        private IPEndPoint _localEp;
        private bool _isConnected;
    
        private Socket()
        {
            _client = new UdpClient(0);
            _localEp = (IPEndPoint)_client.Client.LocalEndPoint;
        }

        public void Open()
        {
            if (_isConnected is not false) return;
            _client = new UdpClient(0);
            _localEp = (IPEndPoint)_client.Client.LocalEndPoint;
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
            UniTask.Void(async() =>
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
            _remoteEp = Settings.GameSetting.RemoteEndPoint;
            _client.Send(data, data.Length, _remoteEp);
        }
    }
}
