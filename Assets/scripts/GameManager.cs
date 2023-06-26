using UnityEngine;
using VRoid;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            Auth.Init();
            Application.targetFrameRate = 60;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
