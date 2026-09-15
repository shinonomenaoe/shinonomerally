using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeButtonScripts : MonoBehaviour
{
    string _serectSceneName;
    public void RaceButton()
    {
        Debug.Log("この機能はまだ解放されていません");
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
