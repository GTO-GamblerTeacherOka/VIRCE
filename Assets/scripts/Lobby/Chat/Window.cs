using System;
using Lobby.Chat.DataBase;
using UnityEngine;
using Zenject;

namespace Lobby.Chat
{
    /// <summary>
    /// Class for creating chat window
    /// </summary>
    public class Window : MonoBehaviour
    {
        [Inject]
        private GUISkin _skin;
        [SerializeField]
        private ChatManager chatManager;
        
        private IChatDataBase _chatDataBase;
        
        private Rect _windowRect = new(0, 0, 400, 400);
        private bool _showWindow;
        private Vector2 _screenSize = new(0, 0);
        private Vector2 _scrollPosition = new(0, 0);
        
        [SerializeField] private Texture2D chatButtonTexture;
        [SerializeField] private Texture2D closeButtonTexture;
        [SerializeField] private Texture2D sendButtonTexture;

        private string _chatMessage = string.Empty;

        private void Start()
        {
            _screenSize.x = Screen.width;
            _screenSize.y = Screen.height;
            _windowRect.y = Screen.height - _windowRect.height;
            _showWindow = false;
        }

        private void OnGUI()
        {
            GUI.skin = _skin;
            if (_showWindow)
            {
                _windowRect = GUI.Window(0, _windowRect, DrawWindow, "Chat");
            }
            else
            {
                if (GUI.Button(new Rect(0, _screenSize.y - 60, 60, 60), chatButtonTexture, GUIStyle.none))
                {
                    _showWindow = true;
                }
            }
        }

        private void DrawWindow(int windowID)
        {
            if(GUI.Button(new Rect(0, -10, 60, 60), closeButtonTexture, GUIStyle.none))
            {
                _showWindow = false;
            }
            _chatMessage = GUI.TextField(new Rect(20, 350, 320, 20), _chatMessage);
            
            // ReSharper disable once InvertIf
            if (GUI.Button(new Rect(360, 340, 40, 40), sendButtonTexture, GUIStyle.none) || Event.current.keyCode is KeyCode.Return)
            {
                if(_chatMessage.Length > 0)
                {
                    chatManager.SendChatMessage(new Message("Demo", "DemoUser", _chatMessage, DateTime.Now));
                }
                _chatMessage = string.Empty;
            }
            
            var messages = chatManager.GetChatMessages();
            _scrollPosition = GUI.BeginScrollView(new Rect(0, 50, 400, 300), _scrollPosition, new Rect(0, 0, 380, messages.Length * 20));
            _scrollPosition = new Vector2(_scrollPosition.x, messages.Length * 20);
            for (var i = 0; i < messages.Length; i++)
            {
                GUI.Label(new Rect(0, i * 20, 380, 20), messages[i].ToString());
            }
            GUI.EndScrollView();
        }
    }
}
