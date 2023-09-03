using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Cysharp.Threading.Tasks;

namespace Lace
{
    /// <summary>
    /// Class for managing lace
    /// </summary>
    public class LaceManager : MonoBehaviour
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
            }
        }
}

