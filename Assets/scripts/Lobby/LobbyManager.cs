using System;
using System.Collections.Generic;
using Networking;
using Protocol;
using Settings;
using UnityEngine;
using VRoid;
using Buffer = Networking.Buffer;

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
            var buf = Buffer.Instance.GetBuf();
            foreach (var (key, obj) in UserObjects)
            {
                if (key == GameSetting.UserId) continue;

                try
                {
                    var body = buf[key];
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
                catch
                {
                    // ignored
                }
            }
        }

        private void FixedUpdate()
        {
            if (CurrentUserGameObject is null) return;
            Send();
        }

        private static void Send()
        {
            Api.SendPosition(CurrentUserGameObject.transform.position,
                CurrentUserGameObject.transform.eulerAngles);
        }
    }
}