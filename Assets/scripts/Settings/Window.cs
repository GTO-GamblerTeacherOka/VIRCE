using System.Net;
using UnityEngine;
using Zenject;
using Protocol;

namespace Settings
{
    public class Window : MonoBehaviour
    {
        private string _licenseFile;
        
        [Inject]
        private GUISkin _skin;
        [Inject(Id = "closeButtonTexture")]
        private Texture2D _closeButtonTexture;

        private Rect _windowRect;
        private Vector2 _screenSize = new(0, 0);
        private bool _showWindow;
        private bool _showLicense;
        private Vector2 _scrollPosition = new(0, 0);
        private string _ipAddress;
        private string _port;
        private string _userName;

        private void Start()
        {
            _screenSize.x = Screen.width;
            _screenSize.y = Screen.height;
            // 右端にwindowを表示
            _windowRect = new Rect(Screen.width - 400, 0, 400, 400);
            _showWindow = false;
            _licenseFile = (Resources.Load("license", typeof(TextAsset)) as TextAsset)?.text;
        }

        private void OnGUI()
        {
            GUI.skin = _skin;
            if (_showLicense)
            {
                _windowRect = GUI.Window(1000, _windowRect, DrawLicense, "License");
            }
            else
            {
                if (_showWindow)
                {
                    _windowRect = GUI.Window(1000, _windowRect, DrawWindow, "Settings");
                }
                else
                {
                    if (GUI.Button(new Rect(_screenSize.x - 220, 20, 200, 20), "Settings"))
                    {
                        _showWindow = true;
                    }
                }
            }
        }
        
        private void DrawWindow(int windowID)
        {
            if (GUI.Button(new Rect(0, -10, 60, 60), _closeButtonTexture, GUIStyle.none))
            {
                _showWindow = false;
            }
            if (GUI.Button(new Rect(100, 100, 200, 20), "License"))
            {
                _showLicense = true;
            }
            
            // for debug
            GUI.Label(new Rect(100, 150, 100, 20), "IP Address");
            GUI.Label(new Rect(100, 200, 100, 20), "Port");
            _ipAddress = GUI.TextField(new Rect(200, 150, 100, 20), _ipAddress);
            _port = GUI.TextField(new Rect(200, 200, 100, 20), _port);
            if (GUI.Button(new Rect(100, 250, 200, 20), "Apply"))
            {
                try
                {
                    var ep = new IPEndPoint(IPAddress.Parse(_ipAddress), int.Parse(_port));
                    GameSetting.SetRemoteEndPoint(ep);
                    Debug.Log("Save success");
                }
                catch
                {
                    _ipAddress = string.Empty;
                    _port = string.Empty;
                }
            }
            // for debug end
            
            GUI.Label(new Rect(100, 300, 100, 20), "User Name");
            _userName = GUI.TextField(new Rect(200, 300, 100, 20), _userName);
            if(GUI.Button(new Rect(100, 350, 200, 20), "Name Change"))
            {
                GameSetting.SetUserName(_userName);
                Api.SendDisplayName(_userName);
            }
        }

        private void DrawLicense(int windowID)
        {
            var licenseTexts = _licenseFile.Split('\n');
            var style = new GUIStyle(GUI.skin.label)
            {
                fontSize = 10
            };
            _scrollPosition = GUI.BeginScrollView(new Rect(0, 60, 400, 400), _scrollPosition, new Rect(0, 0, 400, licenseTexts.Length * 10));
            foreach (var text in licenseTexts)
            {
                GUILayout.Label(text, style);
            }
            GUI.EndScrollView();
            if(GUI.Button(new Rect(0, -10, 60, 60), _closeButtonTexture, GUIStyle.none))
            {
                _showLicense = false;
            }
        }
    }
}
