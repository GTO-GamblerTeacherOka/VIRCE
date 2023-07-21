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
        public static Dictionary<string, GameObject> userObjects;

        private void Update()
        {
            //TODO:get data from DataBuf
            var body = new byte[24];
            var x = body[0..4];
            var y = body[4..8];
            var z = body[8..12];

            var vec = new Vector3(
                BitConverter.ToSingle(x), 
                BitConverter.ToSingle(y), 
                BitConverter.ToSingle(z));
            
            
        }
    }
}
