using UnityEngine;
using UnityEngine.UI;

public class SettingViewButton : MonoBehaviour
{
    [SerializeField] private float _reStartTime = 3;
    [SerializeField] private GameObject _reStartTimeUI;
    [SerializeField] Text _reStartTimeText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StopGame()
    {
        Time.timeScale = 0;
    }

    public void ReStartGame()
    {
        _reStartTimeUI.SetActive(true);
        for (float reStartTime = _reStartTime; reStartTime <= 0;)
        {
            reStartTime = reStartTime -= Time.unscaledDeltaTime;
            _reStartTimeText.text = $"•œ‹A‚Ü‚Å{Mathf.CeilToInt(reStartTime)}•b";
        }
        Time.timeScale = 1.0f;
        _reStartTimeUI.SetActive(false);
    }
}
