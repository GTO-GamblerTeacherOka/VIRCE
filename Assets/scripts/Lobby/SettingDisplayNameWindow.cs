using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lobby
{
    /// <summary>
    ///     Class for creating setting display name window
    /// </summary>
    public class SettingDisplayNameWindow : MonoBehaviour
    {
        [SerializeField] private Texture2D enterNameTexture;
        [SerializeField] private Texture2D sendButtonTexture;
        
        private string _displayName = string.Empty;
        
        private Vector2 _screenSize = new(0, 0);
        private Rect _windowRect = new(0, 0, 400, 400);
        void Update()
        {

        }
    }
}
