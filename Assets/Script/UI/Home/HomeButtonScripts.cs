using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeButtonScripts : MonoBehaviour
{
    [SerializeField] GameObject _selectRaceModeView;
    [SerializeField] GameObject _selectRaceCourseView;
    private void Start()
    {
        _selectRaceModeView.SetActive(false);
        _selectRaceCourseView.SetActive(false);
    }
    string _serectSceneName;
    public void RaceButton()
    {
        _selectRaceModeView.SetActive(true);
    }
    public void TimeAttack()
    {
        _selectRaceCourseView.SetActive(true);
    }

    public void GarageButton()
    {
        Debug.Log("この機能はまだ解放されていません");
    }

    public void CoostomButton()
    {
        Debug.Log("この機能はまだ解放されていません");
    }
    public void SerectScene(string serectSceneName)
    {
        _serectSceneName = serectSceneName;
    }
    public void ChengeScene()
    {
        SceneManager.LoadScene(_serectSceneName);
    }
}
