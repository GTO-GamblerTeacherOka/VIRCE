using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Networking;
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

    public static Dictionary<byte, string> DisplayNames = new();

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
            Socket.Instance.StartRecv().Forget();
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