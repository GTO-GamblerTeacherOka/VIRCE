using Lobby.Chat.DataBase;
using NMeCab.Specialized;
using Protocol;
using UnityEngine;
using Util;
using Zenject;

namespace Lobby.Chat
{
    /// <summary>
    ///     Class for managing chat
    /// </summary>
    public class ChatManager : MonoBehaviour
    {
        private const string DicDir = "Assets/Plugins/dic/ipadic";
        private static readonly MeCabIpaDicTagger Tagger = MeCabIpaDicTagger.Create(DicDir);
        [Inject] private IChatDataBase _chatDataBase;

        private ChatManager _instance;

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
                if (WordCheck.IsBlackListWord(node.Surface))
                    text += new string('*', node.Surface.Length);
                else
                    text += node.Surface;
            var sendData = new Message(msg.Id, msg.UserName, text, msg.Time);
            _chatDataBase.AddMessage(sendData);
            Api.SendChat(text);
        }

        public Message[] GetChatMessages()
        {
            return _chatDataBase.GetMessages();
        }
    }
}