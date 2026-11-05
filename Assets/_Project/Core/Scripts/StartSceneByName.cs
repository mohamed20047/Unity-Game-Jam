using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneByName : MonoBehaviour
{
    [SerializeField] private string _SceneName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    [ContextMenu("Run Start Scene")]
    public void StartScene()
    {
        SceneManager.LoadScene(_SceneName,LoadSceneMode.Additive);
    }
}
