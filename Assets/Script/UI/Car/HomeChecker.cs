using UnityEngine.SceneManagement;
using UnityEngine;

public class HomeChecker : MonoBehaviour
{
    string _nowSceneName;
    public bool isHome;
    void Awake()
    {
        _nowSceneName = SceneManager.GetActiveScene().name;
        if (_nowSceneName == "Home")
        {
            isHome = true;
        }
        else
        {
            isHome = false;
        }
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += TakeSceneName;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= TakeSceneName;
    }

    private void TakeSceneName(Scene scene, LoadSceneMode mode)
    {
        _nowSceneName = SceneManager.GetActiveScene().name;
        if (_nowSceneName == "Home")
        {
            isHome = true;
        }
        else
        {
            isHome = false;
        }
    }
}
