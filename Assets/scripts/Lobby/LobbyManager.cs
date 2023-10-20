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
        private const float TimeSpan = 0.05f;
        private static float _lastSendTime;
        private static Dictionary<byte, Vector3[]> _userPositions = new();
        private static Dictionary<byte, GameObject> UserObjects => ModelManager.Models;
        private static GameObject CurrentUserGameObject => UserObjects[GameSetting.UserId];

        private async void Start()
        {
            var data = PacketCreator.EntryPacket(PacketCreator.EntryType.Lobby, GameSetting.ModelPublishId);
            await Socket.Instance.Send(data);
        }

        private void FixedUpdate()
        {
            if (_lastSendTime > TimeSpan)
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

                _userPositions = Buffer.Instance.GetBuf();

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
                        Vector3.Lerp(obj.transform.position, _userPositions[key][0], _lastSendTime / TimeSpan);

                    if (Math.Abs(obj.transform.eulerAngles.y - _userPositions[key][1].y) > 180)
                    {
                        if(obj.transform.eulerAngles.y > _userPositions[key][1].y)
                        {
                            _userPositions[key][1] += new Vector3(0, 360, 0);
                        }
                        else
                        {
                            obj.transform.eulerAngles += new Vector3(0, 360, 0);
                        }
                        float avg = (obj.transform.eulerAngles.y + _userPositions[key][1].y) / 2 + 400 * (float)Math.PI;
                        obj.transform.eulerAngles = new Vector3(0, avg + avg % 360, 0);

                    }
                    else
                    {
                        obj.transform.eulerAngles =
                            Vector3.Lerp(obj.transform.eulerAngles, _userPositions[key][1], _lastSendTime / TimeSpan);
                    }
                }
                catch
                {
                    // ignored
                }
            }
        }

        private static async void Send()
        {
            try
            {
                await Api.SendPosition(CurrentUserGameObject.transform.position,
                    CurrentUserGameObject.transform.eulerAngles);
            }
            catch
            {
                // ignored
            }
        }
    }
}