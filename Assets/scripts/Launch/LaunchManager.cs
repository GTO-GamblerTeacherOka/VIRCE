using UnityEngine;
using Zenject;

namespace Launch
{
    /// <summary>
    ///     Class for managing launch scene
    /// </summary>
    public class LaunchManager : MonoBehaviour
    {
        private string _registerCode = "";

        [Inject] private GUISkin _skin;

        private Rect _windowRect = new(0, 0, 400, 400);

        private void Start()
        {
            _windowRect.x = (Screen.width - _windowRect.width) / 2;
            _windowRect.y = (Screen.height - _windowRect.height) / 2;
        }
    }
}