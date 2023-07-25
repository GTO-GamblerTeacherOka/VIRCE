using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lobby
{
    /// <summary>
    /// Class for managing lobby
    /// </summary>
    public class LobbyManager : MonoBehaviour
    {
        private static Dictionary<string, GameObject> _userObjects;

        private async void Send()
        {
            //todo:send data
        }

        public void Update()
        {
            foreach (var keyValuePair in _userObjects)
            {
                var key = keyValuePair.Key;
                var obj = keyValuePair.Value;
                
                //TODO:get data from DataBuf
                var body = new byte[24];
                var x = body[0..4];
                var y = body[4..8];
                var z = body[8..12];

                var vec = new Vector3(
                    BitConverter.ToSingle(x), 
                    BitConverter.ToSingle(y), 
                    BitConverter.ToSingle(z));

                var anglex = body[12..16];
                var angley = body[16..20];
                var anglez = body[20..24];

                var anglevec = new Vector3(
                    BitConverter.ToSingle(anglex),
                    BitConverter.ToSingle(angley),
                    BitConverter.ToSingle(anglez));
                
                obj.transform.position = vec;
                obj.transform.eulerAngles = anglevec;
                
                //todo:send data to server
                
            }
        }
    }
}
