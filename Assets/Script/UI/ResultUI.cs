using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    [SerializeField] float _timeToActive;
    [SerializeField] Text _resultTime;
    GameObject _resultUI;
    GameObject _mGM;
    PlayINF _playINF;
    void Start()
    {
        _resultUI = GameObject.FindGameObjectWithTag("ResultUI");
        _resultUI.SetActive(false);
        _mGM = GameObject.FindGameObjectWithTag("MainGameManager");
        _playINF = _mGM.GetComponent<PlayINF>();
    }
    void Update()
    {
        if (_playINF._isGoal)
        {
            StartCoroutine(TakeRunTime());
        }
    }

    IEnumerator TakeRunTime()
    {
        yield return new WaitForSeconds(_timeToActive);
        _resultUI.SetActive(true);
        _resultTime.text = _playINF._goalTime;
    }
}
