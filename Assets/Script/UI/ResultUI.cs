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
    bool _isTake;
    void Start()
    {
        _resultUi.SetActive(false);
        _isTake = false; 
        _mGM = GameObject.FindGameObjectWithTag("MainGameManager");
        _playINF = _mGM.GetComponent<PlayINF>();
        Debug.Log("‘ã“üŠ®—¹");
    }
    void Update()
    {
        if (_playINF._isGoal && !_isTake)
        {
            _isTake = true;
            StartCoroutine(TakeRunTime());
        }
    }

    IEnumerator TakeRunTime()
    {
        yield return new WaitForSeconds(_timeToActive);
        _resultUi.SetActive(true);
        yield return new WaitForSeconds(1f);
        _resultTime.text = _playINF._goalTime;
        Debug.Log(_playINF._goalTime);
        if (_resultTime.text == null) Debug.Log("•¶Žš‚ª‘ã“ü‚³‚ê‚Ä‚¢‚È‚¢");
    }
}
