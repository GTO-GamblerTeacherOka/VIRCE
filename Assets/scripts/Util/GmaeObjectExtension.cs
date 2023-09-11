using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameObjectExtension
{
    /// <summary>
    ///     指定したシーンへ移動(Sceneで選択)
    /// </summary>
    public static void MoveScene(this GameObject gameObject, Scene scene)
    {
        SceneManager.MoveGameObjectToScene(gameObject, scene);
    }

    /// <summary>
    ///     指定したシーンへ移動(シーン名で選択)
    /// </summary>
    public static void MoveScene(this GameObject gameObject, string SceneName)
    {
        gameObject.MoveScene(SceneManager.GetSceneByName(SceneName));
    }

    /// <summary>
    ///     アクティブなシーンへ移動
    /// </summary>
    public static void MoveActiveScene(this GameObject gameObject)
    {
        gameObject.MoveScene(SceneManager.GetActiveScene());
    }

    /// <summary>
    ///     DontDestroyOnLoadからアクティブなシーンへ移動
    /// </summary>
    public static void DestroyOnLoad(this GameObject gameObject)
    {
        gameObject.MoveActiveScene();
    }
}