using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayINF : MonoBehaviour
{
    public bool _isStart = false;
    public bool _isGoal = false;
    public int _nowNam;
    public string _goalTime;
    [SerializeField] Transform[] _startPos;
    [SerializeField] Transform[] _goalPos;

    private void Awake()
    {
        _isStart = false;
        _isGoal = false;
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
    }
}
