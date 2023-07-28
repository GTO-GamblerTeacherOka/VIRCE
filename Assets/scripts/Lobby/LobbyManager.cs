using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using static Cysharp.Threading.Tasks.UniTask;

namespace Lobby
{
    /// <summary>
    /// Class for managing lobby
    /// </summary>
    public class LobbyManager : MonoBehaviour
    {
        private static Dictionary<string, GameObject> _userObjects;
        private static GameObject _currentUserGameObject;

        private void Send()
        {
            var sendVec = new float[]
            {
                _currentUserGameObject.transform.position.x,
                _currentUserGameObject.transform.position.y,
                _currentUserGameObject.transform.position.z
            };
            var sendAngleVec = new float[]
            {
                _currentUserGameObject.transform.eulerAngles.x,
                _currentUserGameObject.transform.eulerAngles.y,
                _currentUserGameObject.transform.eulerAngles.z
            };
            var vf = sendVec.Concat(sendAngleVec).ToArray();
            var body = new byte[24];
            for (var i = 0; i < vf.Length; i++)
            {
                var b = BitConverter.GetBytes(vf[i]);
                Array.Copy(b, 0, body, i * 4, 4);
            }
            //todo:send data with header
            UniTask.Run(() =>
            {

            });
        }

        private void Update()
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

                var angleX = body[12..16];
                var angleY = body[16..20];
                var angleZ = body[20..24];

                var angleVec = new Vector3(
                    BitConverter.ToSingle(angleX),
                    BitConverter.ToSingle(angleY),
                    BitConverter.ToSingle(angleZ));
                
                obj.transform.position = vec;
                obj.transform.eulerAngles = angleVec;
                
            }
        }

        private void FixedUpdate()
        {
            Send();
        }
    }
}
