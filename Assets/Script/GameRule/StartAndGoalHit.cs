using UnityEngine;

public class StartAndGoalHit : MonoBehaviour
{
    GameObject _gameManager;
    PlayINF _playInf;
    Collider _collider;
    [SerializeField] bool _isMyturn = false;
    bool _start = false;
    bool _goal = false;
    void Start()
    {
        if (gameObject.CompareTag("StartLine"))_start = true;
        else _goal = true;
        _collider = GetComponent<Collider>();
        _gameManager = GameObject.FindWithTag("MainGameManager");
        _playInf = _gameManager.GetComponent<PlayINF>();
        Debug.Log($"_start‚Í{_start}‚Å_goal‚Í{_goal}‚Å‚·");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    { 
        if (other.CompareTag("PlayerCar") && _isMyturn)
        {
            
            if (_start)_playInf._isStart = true;
           if(_goal)_playInf._isGoal = true;
        }
    }
}
