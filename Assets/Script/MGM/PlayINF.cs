using UnityEngine;

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
}
