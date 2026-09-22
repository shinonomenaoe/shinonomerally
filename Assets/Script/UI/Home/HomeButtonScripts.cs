using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeButtonScripts : MonoBehaviour
{
    [SerializeField] GameObject UnderSelectVelt;
    [SerializeField] GameObject[] _views;
    GameObject _gameManager;
    GameObject _mainGameManager;
    RaceINF _raceINF;
    MenuCarTransform _menuCarTransform;
    string _serectSceneName;
    private void Start()
    {
        _mainGameManager = GameObject.FindWithTag("MainGameManager");
        _gameManager = GameObject.FindWithTag("GameManager");
        _raceINF = _mainGameManager.GetComponent<RaceINF>();
        _menuCarTransform = _gameManager.GetComponent<MenuCarTransform>();
        foreach (var view in _views)
        {
            view.SetActive(false);
        }
    }

    public void ChengeView(GameObject SelectView)
    {
        foreach (var view in _views) view.SetActive(false);
        SelectView.SetActive(true);
    }

    public void BackView(GameObject nowView)
    {
        //var BackView = transform.parent.gameObject;
        nowView.SetActive(false);
    }

    public void SelectRaceMode(int raceModeIndexs)
    {
        _raceINF._raceType = (RaceINF.RaceType)raceModeIndexs;
        Debug.Log($"RaceModeが{_raceINF._raceType}に変更されました");
    }

    public void SelectScene(string serectSceneName)
    {
        Debug.Log("シーン名が記録された");
        _serectSceneName = serectSceneName;
    }
    public void ChengeScene()
    {
        SceneManager.LoadScene(_serectSceneName);
    }
}
