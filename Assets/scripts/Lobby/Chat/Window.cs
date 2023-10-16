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
        }

        private void DrawWindow(int windowID)
        {
            if (GUI.Button(new Rect(0, -10, 60, 60), _closeButtonTexture, GUIStyle.none)) _showWindow = false;
            _chatMessage = GUI.TextField(new Rect(20, 350, 320, 20), _chatMessage);

            // ReSharper disable once InvertIf
            if (GUI.Button(new Rect(360, 340, 40, 40), sendButtonTexture, GUIStyle.none))
            {
                if (_chatMessage.Length > 0)
                    chatManager.SendChatMessage(new Message("Demo", "DemoUser", _chatMessage, DateTime.Now));
                _chatMessage = string.Empty;
            }

            var messages = chatManager.GetChatMessages();
            _scrollPosition = GUI.BeginScrollView(new Rect(0, 50, 400, 300), _scrollPosition,
                new Rect(0, 0, 380, messages.Length * 20));
            _scrollPosition = new Vector2(_scrollPosition.x, messages.Length * 20);

            var count = 1; // ループの外部で定義し、1で初期化
            var lineWidth = 8;

            foreach (var msg in messages)
            {
                var displayStr = string.Empty;
                var lineLength = 0;
                var words = msg.Text.Split(' '); // テキストを単語ごとに分割

                foreach (var word in words)
                {
                    var wordWidth = GetWordWidth(word);

                    if (lineLength + wordWidth + (displayStr.Length  > 0 ? 1 : 0) <= lineWidth)
                    {
                        if (!string.IsNullOrEmpty(displayStr))
                        {
                            displayStr += " ";
                        }
                        displayStr += word;
                        lineLength += wordWidth + (displayStr.Length > 0 ? 1 : 0);
                    }
                    else
                    {
                        if (lineLength > 0)
                        {
                            count++;
                            displayStr += word;
                            lineLength = wordWidth;
                        }
                        else
                        {
                            displayStr += word;
                            lineLength = wordWidth;
                        }
                    }
                }

                GUI.Label(new Rect(0, count * 20, 380, 20), displayStr);
                count++; // 改行時の行数増加に加えて、1行分の行数を追加
            }

            int GetWordWidth(string word)
            {
                int width = 0;

                foreach (var c in word)
                {
                    width += GetCharWidth(c);
                }
                return width;
            }

            int GetCharWidth(char c)
            {
                if (c >= 32 && c <= 126) // 印刷可能な ASCII 文字の範囲
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
        }
    }
}


        
    
