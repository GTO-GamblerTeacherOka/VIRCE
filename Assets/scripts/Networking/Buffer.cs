using System;
using System.Collections.Generic;
using UnityEngine;

namespace Networking
{
    public class Buffer
    {
        private static Buffer _instance;
        private readonly Dictionary<byte, Vector3[]> _dictionary;

        private Buffer()
        {
            _dictionary = new Dictionary<byte, Vector3[]>();
        }

        public static Buffer Instance
        {
            get { return _instance ??= new Buffer(); }
        }

        public void InputBuf(byte userId, byte[] data)
        {
            var x = data[..4];
            var y = data[4..8];
            var z = data[8..12];
            var vec = new Vector3(
                BitConverter.ToSingle(x),
                BitConverter.ToSingle(y),
                BitConverter.ToSingle(z));

            var angleX = data[12..16];
            var angleY = data[16..20];
            var angleZ = data[20..24];

            var angleVec = new Vector3(
                BitConverter.ToSingle(angleX),
                BitConverter.ToSingle(angleY),
                BitConverter.ToSingle(angleZ));
            _dictionary[userId] = new[] { vec, angleVec };
        }

        public Dictionary<byte, Vector3[]> GetBuf()
        {
            return new Dictionary<byte, Vector3[]>(_dictionary);
        }

        public Vector3[] GetBuf(byte id)
        {
            return _dictionary[id];
        }
    }
}