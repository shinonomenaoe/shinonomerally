using UnityEngine.SceneManagement;
using UnityEngine;

public class HomeChecker : MonoBehaviour
{
    string _nowSceneName;
    GameObject _inGameUi;
    void Start()
    {
        _nowSceneName = SceneManager.GetActiveScene().name;
        _inGameUi = GameObject.FindGameObjectWithTag("InGameUI");
    }
    void Update()
    {
        if (_nowSceneName == "Home")
        {
            _inGameUi.SetActive(false);
        }
        else
        {
            _inGameUi.SetActive(true);
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
