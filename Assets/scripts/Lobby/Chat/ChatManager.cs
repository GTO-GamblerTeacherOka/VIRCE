using System.IO;
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
        private static string _dicDir = string.Empty;
        private static MeCabIpaDicTagger Tagger;
        [Inject] private IChatDataBase _chatDataBase;

        public static ChatManager Instance { get; private set; }

        private void Start()
        {
            if (Instance == null)
            {
                Instance = this;
                Initialize();
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Initialize()
        {
            _dicDir = Path.Combine(Application.streamingAssetsPath, "dic", "ipadic");
            Tagger = MeCabIpaDicTagger.Create(_dicDir);
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

        public void AddChatMessage(in Message msg)
        {
            _chatDataBase.AddMessage(msg);
        }
    }
}