using Pixiv.VroidSdk;
using UnityEngine;
using VRoid;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            Auth.Init();
            ModelLoader.Initialize(Auth.SDKConfig, Auth.Api, "virce");
            Application.targetFrameRate = 60;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        Auth.Logout();
    }
}
