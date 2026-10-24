using UnityEngine;

public class StartAndGoalHit : MonoBehaviour
{
    GameObject _mGM;
    PlayINF _playInf;
    MoneySystem _moneySystem;
    Collider _collider;
    [SerializeField] bool _isMyturn = false;
    public int maxReward = 10000;
    public int goalPos = 1;
    bool _start = false;
    bool _goal = false;
    void Start()
    {
        if (gameObject.CompareTag("StartLine"))_start = true;
        else _goal = true;
        _collider = GetComponent<Collider>();
        _mGM = GameObject.FindWithTag("MainGameManager");
        _playInf = _mGM.GetComponent<PlayINF>();
        _moneySystem = _mGM.GetComponent <MoneySystem>();
        Debug.Log($"_start‚Í{_start}‚Å_goal‚Í{_goal}‚Å‚·");
        goalPos = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Map"))return;
        if (other.CompareTag("PlayerCar") && _isMyturn)
        {
            if (_start) _playInf._isStart = true;
            if (_goal)
            {
                _playInf._isGoal = true;
                _moneySystem.money += maxReward / goalPos;
            }
        } 
        goalPos++;
    }
}
