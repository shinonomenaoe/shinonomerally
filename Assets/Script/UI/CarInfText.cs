using UnityEngine;
using UnityEngine.UI;

public class CarInfText : MonoBehaviour
{
    GameObject _playercar;
    CarEngenAndGearBox _carEngen;
    Rigidbody _rb;
    [SerializeField] private Text _rPM;
    [SerializeField] private Text _gear;
    [SerializeField] private Text _perHour;
    [SerializeField] private Text _time;
    private float _second;
    private int _minutes;
    void Start()
    {
        _playercar = GameObject.FindWithTag("PlayerCar");
        _carEngen = _playercar.GetComponent<CarEngenAndGearBox>();
        _rb = _playercar.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRPM();
        UpdateGear();
        UpdatePerHour();
        UpdateTime();
    }

    void UpdateRPM()
    {
        int rpm = (int)_carEngen._nowRPM;
        _rPM.text = rpm.ToString();
    }

    void UpdateGear()
    {
        if (_carEngen._nowGearNam == 0) _gear.text = "R";
        if (_carEngen._nowGearNam == 1) _gear.text = "N";
        if (_carEngen._nowGearNam >= 2) _gear.text = $"{_carEngen._nowGearNam - 1}";
    }

    void UpdatePerHour()
    {
        int perHour = Mathf.RoundToInt(_rb.linearVelocity.magnitude * 3.6f);
        _perHour.text = perHour.ToString();
    }

    void UpdateTime()
    {
        if (_second < 60)
        {
            _second += Time.deltaTime;
        }
        else
        {
            _second = 0f;
            _minutes++;
        }
        _time.text = $"{_minutes:D2}M {_second:D2}S";
    }
}
