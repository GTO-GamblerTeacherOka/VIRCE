using UnityEngine;
using UnityEngine.SceneManagement;
using VRoid;

namespace Launch
{
    public class LaunchManager : MonoBehaviour
    {
        [SerializeField]
        private GUISkin skin;

        private Rect _windowRect = new(0, 0, 400, 400);
        private string _registerCode = "";

        private void Start()
        {
            _windowRect.x = (Screen.width - _windowRect.width) / 2;
            _windowRect.y = (Screen.height - _windowRect.height) / 2;
        }

        private void OnGUI()
        {
            GUI.skin = skin;
            _windowRect = GUI.Window(0, _windowRect, DrawWindow, "Welcome to V.I.R.C.E");
        }

        private void DrawWindow(int windowID)
        {
            if (GUI.Button(new Rect(50, 140, 300, 20), "Login with VRoid Account"))
            {
                Auth.Login(() =>
                {
                    SceneManager.LoadScene("main");
                });
            }
            
            _registerCode = GUI.TextField(new Rect(70, 230, 260, 20), _registerCode);
            if(GUI.Button(new Rect(50, 300, 300, 20), "Register Code"))
            {
                Auth.OnRegisterCode(_registerCode);
            }
        }
    }
}
