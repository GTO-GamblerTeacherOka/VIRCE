using Lobby.Chat.DataBase;
using UnityEngine;
using Zenject;
using NMeCab.Specialized;
using Protocol;
using Util;

namespace Lobby.Chat
{
    /// <summary>
    ///     Class for managing chat
    /// </summary>
    public class ChatManager : MonoBehaviour
    {
        [Inject] private IChatDataBase _chatDataBase;

        private ChatManager _instance;

        private static readonly MeCabIpaDicTagger Tagger = MeCabIpaDicTagger.Create();

        private void Start()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void SendChatMessage(in Message msg)
        {
            var nodes = Tagger.Parse(msg.Text);
            var text = string.Empty;
            foreach (var node in nodes)
            {
                if (WordCheck.IsBlackListWord(node.Surface))
                {
                    text += new string('*', node.Surface.Length);
                }
                else
                {
                    text += node.Surface;
                }
            }

            _chatDataBase.AddMessage(new Message(msg.Id, msg.UserName, text, msg.Time));
            Api.SendChat(text);
        }

        public Message[] GetChatMessages()
        {
            return _chatDataBase.GetMessages();
        }
    }
}