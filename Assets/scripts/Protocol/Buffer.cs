using System.Collections.Generic;

namespace Protocol
{
    public class Buffer
    {
        private readonly Dictionary<int, byte[]> _dictionary;

        public Buffer()
        {
            _dictionary = new Dictionary<int, byte[]>();
        }

        public void InputBuf(byte[] data)
        {
            var parsedData = Parser.GetData(data);
            _dictionary[parsedData.userID] = parsedData.body;
        }

        public Dictionary<int, byte[]> GetBuf()
        {
            return new Dictionary<int, byte[]>(_dictionary);
        }

        public byte[] GetBuf(int id)
        {
            return _dictionary[id];
        }
    }
}
