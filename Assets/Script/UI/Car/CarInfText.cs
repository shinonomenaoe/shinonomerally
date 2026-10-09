using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarInfText : MonoBehaviour
{
    GameObject _playercar;
    GameObject _gameManager;
    CarEngenAndGearBox _carEngen;
    [SerializeField]PlayINF _playInf;
    Rigidbody _rb;
    [SerializeField] private Text _rPM;
    [SerializeField] private Text _gear;
    [SerializeField] private Text _perHour;
    void Start()
    {
        _playercar = GameObject.FindWithTag("PlayerCar");
        _carEngen = _playercar.GetComponent<CarEngenAndGearBox>();
        _rb = _playercar.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_playInf._isGoal)
        {
            UpdateRPM();
            UpdateGear();
            UpdatePerHour();
        }

    }

    void UpdateRPM()
    {
        int rpm = (int)_carEngen.nowRPM;
        _rPM.text = rpm.ToString();
    }

    void UpdateGear()
    {
        if(_carEngen.mission == CarEngenAndGearBox.Mission.MT)
        {
            if (_carEngen.nowGearNam == 0) _gear.text = "R";
            if (_carEngen.nowGearNam == 1) _gear.text = "N";
            if (_carEngen.nowGearNam >= 2) _gear.text = $"{_carEngen.nowGearNam - 1}";
            return;
        }
        else
        {
            if(_carEngen.isRevers) _gear.text = "R";
            else _gear.text = "D";
        }
    }

    void UpdatePerHour()
    {
        int perHour = Mathf.RoundToInt(_rb.linearVelocity.magnitude * 3.6f);
        _perHour.text = perHour.ToString();
    }

    //void UpdateTime()
    //{
    //    if (_playInf._isStart && !_playInf._isGoal)
    //    {
    //        if (_second < 60)
    //        {
    //            _second += Time.deltaTime;
    //        }
    //        else
    //        {
    //            _second = 0f;
    //            _minutes++;
    //        }
    //        _time.text = $"{_minutes:D2}m {Mathf.FloorToInt(_second):D2}s";
    //    }
    //}

    private void OnEnable()
    {
        SceneManager.sceneLoaded += ResetState;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= ResetState;
    }

    private void ResetState(Scene scene, LoadSceneMode mode)
    {
    }
}
