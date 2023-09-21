using System;
using System.Collections.Generic;
using Networking;
using Protocol;
using Settings;
using UnityEngine;

namespace Lobby
{
    /// <summary>
    ///     Class for managing lobby
    /// </summary>
    public class LobbyManager : MonoBehaviour
    {
        private static Dictionary<string, GameObject> _userObjects;
        private static GameObject _currentUserGameObject;

        private void Start()
        {
            _userObjects = new Dictionary<string, GameObject>();
            var data = PacketCreator.EntryPacket(PacketCreator.EntryType.Lobby, GameSetting.ModelPublishId);
            Socket.Instance.Send(data);
        }

        private void Update()
        {
            foreach (var keyValuePair in _userObjects)
            {
                var key = keyValuePair.Key;
                var obj = keyValuePair.Value;

                //TODO:get data from DataBuf
                var body = new byte[24];

                var x = body[..4];
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

        private static void Send()
        {
            var data = PacketCreator.PositionPacket(_currentUserGameObject.transform.position,
                _currentUserGameObject.transform.eulerAngles);
            Socket.Instance.Send(data);
        }

        public void SetCurrentGameObject(GameObject playerGameObject)
        {
            _currentUserGameObject = playerGameObject;
        }
    }
}