using System;
using System.Net.Sockets;
using System.Text;
using Cysharp.Threading.Tasks;
using Lobby.Chat;
using Protocol;
using Settings;
using VRoid;

namespace Networking
{
    public static class ReceiveHandler
    {
        public static async UniTask Handle(UdpReceiveResult res)
        {
            await UniTask.RunOnThreadPool(() =>
            {
                var data = res.Buffer;
                var (header, body) = Parser.Split(data);
                var (flag, userId, roomId) = Parser.AnalyzeHeader(header);
                switch (flag)
                {
                    case Parser.Flag.PositionData:
                        Buffer.Instance.InputBuf(userId, body);
                        break;
                    case Parser.Flag.RoomEntry:
                        if (GameSetting.UserId == 0)
                        {
                            GameSetting.SetUserId(userId);
                            GameSetting.SetRoomId(roomId);
                        }
                        else
                        {
                            ModelManager.LoadModelQueue.Enqueue((userId, Encoding.UTF8.GetString(body)));
                        }

                        break;
                    case Parser.Flag.AvatarData:
                        ModelManager.LoadModelQueue.Enqueue((userId, Encoding.UTF8.GetString(body)));
                        break;
                    case Parser.Flag.RoomExit:
                        ModelManager.DeleteUserIds.Add(userId);
                        break;
                    case Parser.Flag.Reaction:
                        break;
                    case Parser.Flag.ChatData:
                        ChatManager.Instance.AddChatMessage(new Message("test", GameManager.DisplayNames[userId],
                            Encoding.UTF8.GetString(body), DateTime.Now));
                        break;
                    case Parser.Flag.DisplayNameData:
                        GameManager.DisplayNames.Add(userId, Encoding.UTF8.GetString(body));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            });
        }
    }
}