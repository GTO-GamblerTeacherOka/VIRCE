using UnityEngine;
using Zenject;

namespace Lobby.Chat
{
    public class Window : MonoBehaviour
    {
        [Inject]
        private GUISkin _skin;
        
        private Rect _windowRect = new(0, 0, 400, 400);
        private bool _showWindow;
        private Vector2 _screenSize = new(0, 0);
        
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
            if (GUI.Button(new Rect(360, 340, 40, 40), sendButtonTexture, GUIStyle.none))
            {
                _chatMessage = string.Empty;
                // ToDo: Send message
            }

            if (Event.current.keyCode == KeyCode.Return)
            {
                _chatMessage = string.Empty;
                // ToDo: Send message
            }
        }
    }
}
