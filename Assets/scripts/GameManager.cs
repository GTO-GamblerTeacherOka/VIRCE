using Pixiv.VroidSdk;
using Protocol;
using UnityEngine;
using VRoid;

/// <summary>
///     Class for managing game
/// </summary>
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
            MultiplayModelLoader.Initialize(Auth.SDKConfig, Auth.Api, "virce");
            Application.targetFrameRate = 60;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        Api.SendExit();

        Auth.Logout();
    }
}