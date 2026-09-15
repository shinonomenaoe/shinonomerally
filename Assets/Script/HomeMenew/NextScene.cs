using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    //[SerializeField] string[] _sceneName;
    [SerializeField]private string _serectSceneName;
    public void ChengeScene()
    {
        SceneManager.LoadScene(_serectSceneName);
    }

    public void SerectScene(string serectSceneName)
    {
        _serectSceneName = serectSceneName;
    }
}
