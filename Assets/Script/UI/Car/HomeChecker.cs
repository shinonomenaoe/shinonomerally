using UnityEngine.SceneManagement;
using UnityEngine;

public class HomeChecker : MonoBehaviour
{
    string _nowSceneName;
    public bool _isHome;
    void Start()
    {
        _nowSceneName = SceneManager.GetActiveScene().name;
    }
    void Update()
    {
        if (_nowSceneName == "Home")
        {
            _isHome = true;
        }
        else
        {
            _isHome = false;
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

    private void TakeSceneName(Scene scene,LoadSceneMode mode)
    {
        _nowSceneName = SceneManager.GetActiveScene().name;
    }
}
