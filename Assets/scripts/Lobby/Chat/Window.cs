using System;
using System.Text.RegularExpressions;
using Lobby.Chat.DataBase;
using UnityEngine;
using Zenject;

namespace Lobby.Chat
{
    /// <summary>
    ///     Class for creating chat window
    /// </summary>
    public class Window : MonoBehaviour
    {
        [SerializeField] private ChatManager chatManager;

        [SerializeField] private Texture2D chatButtonTexture;
        [SerializeField] private Texture2D sendButtonTexture;

        private IChatDataBase _chatDataBase;

        private string _chatMessage = string.Empty;
        [Inject(Id = "closeButtonTexture")] private Texture2D _closeButtonTexture;
        private Vector2 _screenSize = new(0, 0);
        private Vector2 _scrollPosition = new(0, 0);
        private bool _showWindow;

        [Inject] private GUISkin _skin;

        private Rect _windowRect = new(0, 0, 400, 400);
        
        // Use for Debug
        private string _errorMessage = string.Empty;

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
                    _showWindow = true;
            }
            // debug
            GUI.Label(new Rect(0, 0, 400, 20), _errorMessage);
            // end debug
        }

        private void DrawWindow(int windowID)
        {
            if (GUI.Button(new Rect(0, -10, 60, 60), _closeButtonTexture, GUIStyle.none)) _showWindow = false;
            _chatMessage = GUI.TextField(new Rect(20, 350, 320, 20), _chatMessage);

            // ReSharper disable once InvertIf
            if (GUI.Button(new Rect(360, 340, 40, 40), sendButtonTexture, GUIStyle.none))
            {
                try
                {
                    if (_chatMessage.Length > 0)
                        chatManager.SendChatMessage(new Message("Demo", "DemoUser", _chatMessage, DateTime.Now));
                    _chatMessage = string.Empty;
                }
                catch (Exception e)
                {
                    _errorMessage = e.Message;
                }
            }

            var messages = chatManager.GetChatMessages();
            _scrollPosition = GUI.BeginScrollView(new Rect(0, 50, 400, 300), _scrollPosition,
                new Rect(0, 0, 380, messages.Length * 20));
            _scrollPosition = new Vector2(_scrollPosition.x, messages.Length * 20);
            
            foreach(var msg in messages)
            {
                var l = 0;
                var count = 1;
                var displayStr = string.Empty;
                foreach (var c in msg.Text)
                {
                    var str = $"{c}";
                    l += new Regex(@"[ -~]").IsMatch(str) ? 1 : 2;
                    displayStr += str;
                    if (l >= 8)
                    {
                        displayStr += "\n";
                        count++;
                    }
                }
                GUI.Label(new Rect(0, count * 20, 380, 20), displayStr);
            }
            GUI.EndScrollView();
        }
        
    }
}