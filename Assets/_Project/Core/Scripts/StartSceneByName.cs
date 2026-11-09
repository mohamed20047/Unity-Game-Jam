using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneByName : MonoBehaviour
{
    [SerializeField] private string _SceneName;

    //NOTE: you can't track 2 additive scene by this way
    private string activeScene;

    void Start()
    {

    }

    [ContextMenu("Run Start Scene")]
    public void StartScene()
    {
        SceneManager.LoadScene(_SceneName, LoadSceneMode.Additive);
        activeScene = _SceneName;
    }

    public void StartScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        activeScene = sceneName;
    }
}
