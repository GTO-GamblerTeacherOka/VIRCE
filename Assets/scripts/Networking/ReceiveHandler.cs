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
                            ModelManager.ModelIds[userId] = Encoding.UTF8.GetString(body);
                            ModelManager.WaitingLoadUserIds.Add(userId);
                        }

                        break;
                    case Parser.Flag.AvatarData:
                        ModelManager.ModelIds.Add(userId, Encoding.UTF8.GetString(body));
                        ModelManager.WaitingLoadUserIds.Add(userId);
                        break;
                    case Parser.Flag.RoomExit:
                        ModelManager.DeleteUserIds.Add(userId);
                        break;
                    case Parser.Flag.Reaction:
                        break;
                    case Parser.Flag.ChatData:
                        ChatManager.Instance.AddChatMessage(new Message(
                            BitConverter.ToUInt32(body[..4]).ToString(),
                            GameManager.DisplayNames[userId],
                            Encoding.UTF8.GetString(body[4..]), DateTime.Now));
                        break;
                    case Parser.Flag.DisplayNameData:
                        GameManager.DisplayNames[userId] = Encoding.UTF8.GetString(body);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            });
        }
    }
}