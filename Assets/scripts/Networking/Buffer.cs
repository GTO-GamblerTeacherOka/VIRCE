using System.Collections.Generic;

namespace Networking
{
    public class Buffer
    {
        private static Buffer _instance;
        private readonly Dictionary<byte, byte[]> _dictionary;

        private Buffer()
        {
            _dictionary = new Dictionary<byte, byte[]>();
        }

        public static Buffer Instance
        {
            get { return _instance ??= new Buffer(); }
        }

        public void InputBuf(byte userId, byte[] data)
        {
            _dictionary[userId] = data;
        }

        public Dictionary<byte, byte[]> GetBuf()
        {
            return new Dictionary<byte, byte[]>(_dictionary);
        }

        public byte[] GetBuf(byte id)
        {
            return _dictionary[id];
        }
    }
}