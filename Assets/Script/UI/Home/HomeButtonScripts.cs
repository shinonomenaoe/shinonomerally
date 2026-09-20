using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeButtonScripts : MonoBehaviour
{
    [SerializeField] GameObject UnderSelectVelt;
    [SerializeField] GameObject[] _views;
    GameObject _gameManager;
    MenuCarTransform _menuCarTransform;
    string _serectSceneName;
    private void Start()
    {
        _gameManager = GameObject.FindWithTag("GameManager");
        _menuCarTransform = _gameManager.GetComponent<MenuCarTransform>();
        foreach (var view in _views)
        {
            view.SetActive(false);
        }
    }
     
    public void ChengeView(GameObject SelectView)
    {
        foreach (var view in _views)view.SetActive(false);
        SelectView.SetActive(true);
    }

    public void SelectScene(string serectSceneName)
    {
        Debug.Log("ÉVÅ[ÉìñºÇ™ãLò^Ç≥ÇÍÇΩ");
        _serectSceneName = serectSceneName;
    }
    public void ChengeScene()
    {
        SceneManager.LoadScene(_serectSceneName);
    }
}
