using System;
using System.Collections.Generic;
using Networking;
using Protocol;
using Settings;
using UnityEngine;
using VRoid;

namespace Lobby
{
    /// <summary>
    ///     Class for managing lobby
    /// </summary>
    public class LobbyManager : MonoBehaviour
    {
        private static Dictionary<byte, GameObject> UserObjects => ModelManager.Models;
        private static GameObject CurrentUserGameObject => UserObjects[GameSetting.UserId];

        private void Start()
        {
            var data = PacketCreator.EntryPacket(PacketCreator.EntryType.Lobby, GameSetting.ModelPublishId);
            Socket.Instance.Send(data);
        }

        private void Update()
        {
            foreach (var keyValuePair in UserObjects)
            {
                var key = keyValuePair.Key;
                if (key == GameSetting.UserId) continue;
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
            if (CurrentUserGameObject is null) return;
            Send();
        }

        private static void Send()
        {
            Api.SendPosition(_currentUserGameObject.transform.position,
                _currentUserGameObject.transform.eulerAngles);
        }
    }
}