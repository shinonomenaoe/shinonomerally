using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeButtonScripts : MonoBehaviour
{
    [SerializeField] GameObject _selectRaceModeView;
    [SerializeField] GameObject _selectRaceCourseView;
    [SerializeField] GameObject _selectMugicViwe;
    private void Start()
    {
        _selectRaceModeView.SetActive(false);
        _selectRaceCourseView.SetActive(false);
        _selectMugicViwe.SetActive(false);
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

    public void SelectMagic()
    {
        _selectMugicViwe.SetActive(true);
    }

    public void GarageButton()
    {
        Debug.Log("この機能はまだ解放されていません");
    }

    public void CoostomButton()
    {
        Debug.Log("この機能はまだ解放されていません");
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
