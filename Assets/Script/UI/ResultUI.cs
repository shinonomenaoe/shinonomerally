using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    [SerializeField] float _timeToActive;
    [SerializeField] Text _resultTime;
    [SerializeField] GameObject _resultUi;
    GameObject _mGM;
    PlayINF _playINF;
    void Start()
    {
        _resultUi.SetActive(false);
        _mGM = GameObject.FindGameObjectWithTag("MainGameManager");
        _playINF = _mGM.GetComponent<PlayINF>();
        Debug.Log("‘ã“üŠ®—¹");
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
        _resultUi.SetActive(true);
        _resultTime.text = _playINF._goalTime;
        Debug.Log("UI‚Ý‚¹‚Ä‚é");
    }
}
