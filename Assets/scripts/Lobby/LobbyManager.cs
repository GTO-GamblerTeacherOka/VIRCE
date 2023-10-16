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
        private static float _lastSendTime;
        private static Dictionary<byte, Vector3[]> UserPositions = new();
        private static Dictionary<byte, GameObject> UserObjects => ModelManager.Models;
        private static GameObject CurrentUserGameObject => UserObjects[GameSetting.UserId];

        private async void Start()
        {
            var data = PacketCreator.EntryPacket(PacketCreator.EntryType.Lobby, GameSetting.ModelPublishId);
            await Socket.Instance.Send(data);
        }

        private void FixedUpdate()
        {
            if (_lastSendTime > 0.05f)
            {
                try
                {
                    if (GameSetting.UserId == 0) return;
                    Send();
                }
                catch
                {
                    // ignored
                }

                UserPositions = Buffer.Instance.GetBuf();

                _lastSendTime = 0;
            }
            else
            {
                _lastSendTime += Time.deltaTime;
            }

            foreach (var (key, obj) in UserObjects)
            {
                if (key == GameSetting.UserId) continue;

                try
                {
                    obj.transform.position =
                        Vector3.Lerp(obj.transform.position, UserPositions[key][0], _lastSendTime / 0.1f);
                    obj.transform.eulerAngles = Vector3.Lerp(obj.transform.eulerAngles, UserPositions[key][1],
                        _lastSendTime / 0.1f);
                }
                catch
                {
                    // ignored
                }
            }
        }

        private static async void Send()
        {
            await Api.SendPosition(CurrentUserGameObject.transform.position,
                CurrentUserGameObject.transform.eulerAngles);
        }
    }
}