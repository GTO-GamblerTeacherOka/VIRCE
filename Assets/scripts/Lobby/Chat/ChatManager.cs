using Lobby.Chat.DataBase;
using UnityEngine;
using Zenject;

namespace Lobby.Chat
{
    public class ChatManager : MonoBehaviour
    {
        [Inject]
        private IChatDataBase _chatDataBase;
        
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
            _chatDataBase.AddMessage(msg);
        }
        
        public Message[] GetChatMessages()
        {
            return _chatDataBase.GetMessages();
        }
    }
}
