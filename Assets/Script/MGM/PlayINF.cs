using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayINF : MonoBehaviour
{
    public bool _isStart = false;
    public bool _isGoal = false;
    public int _nowNam;
    public string _goalTime;
    private float _seconds;
    private int _minits;
    [SerializeField] Text _TimeText;
    [SerializeField] Transform[] _startPos;
    [SerializeField] Transform[] _goalPos;

    private void Awake()
    {
        _isStart = false;
        _isGoal = false;
    }

    private void Update()
    {
        Timer();
    }

    private void Timer()
    {
        if (!_isStart) { return; }
        if (!_isGoal && _isStart)
        {
            if (_seconds < 60)
            {
                _seconds += Time.deltaTime;
            }
            else
            {
                _seconds = 0;
                _minits++;
            }
        }
        int Second = Mathf.RoundToInt(_seconds);
        _goalTime = (_minits.ToString("D2") + ":" + Second.ToString("D2"));
        _TimeText.text = _goalTime;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += ResetState;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= ResetState;
    }

    private void ResetState(Scene scene,LoadSceneMode mode)
    {
        _isStart = false;
        _isGoal = false;
        _goalTime = string.Empty;
        _seconds = 0;
        _minits = 0;
    }
}
