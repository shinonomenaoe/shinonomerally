using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarInfText : MonoBehaviour
{
    GameObject _playUies;
    GameObject _playercar;
    GameObject _gameManager;
    CarEngenAndGearBox _carEngen;
    PlayINF _playInf;
    Rigidbody _rb;
    [SerializeField] private Text _rPM;
    [SerializeField] private Text _gear;
    [SerializeField] private Text _perHour;
    [SerializeField] private Text _time;
    private float _second;
    private int _minutes;
    void Start()
    {
        _playUies = GameObject.FindWithTag("PlayUI");
        _gameManager = GameObject.FindWithTag("MainGameManager");
        _playInf = _gameManager.GetComponent<PlayINF>();
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
            UpdateTime();
        }
        else
        {
            _playInf._goalTime = _time.text;
        }

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
        if (_playInf._isStart && !_playInf._isGoal)
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
            _time.text = $"{_minutes:D2}m {Mathf.FloorToInt(_second):D2}s";
        }
    }

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
        if (scene.name == "Home") return;
        _playUies.SetActive(true);
        _playInf._goalTime = "00m00s";
    }
}
