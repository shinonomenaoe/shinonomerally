using UnityEngine;

public class CarEngenAndGearBox : MonoBehaviour
{
    [Header("エンジン")]
    [SerializeField] public float _idelRPM = 1000f;
    [SerializeField] public float _maxRPM = 6800;
    [SerializeField] public float _meterMAXRPM = 8000;
    private float _nowRPM;
    [SerializeField] float _engenRespons = 10;
    [SerializeField] WheelCollider _driveWheel;
    [SerializeField] float _fRR = 3.266f;
    [Header("ギアボックス")]
    [SerializeField] public float[] _gearPow = { 0, 3.724f, 2.246f, 1.541f, 1.205f, 1.000f, 0.818f };
    [SerializeField] public float[] _maxGearSpeed = { 0, 67, 111, 162, 207, 250, 305 };
    [SerializeField] public float _maxBaxkSpeed = 77;
    int _nowGearNam = 0;
    [SerializeField] public int _maxGear;
    float _nowGearMaxSpeed;
    bool _isClutch = false;

    private void Start()
    {
        _maxGear = _gearPow.Length - 1;
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift)) _isClutch = true;
        else _isClutch = false;
        NowMaxSpeed();
        ChengeGear();
        CalculateRPM();
    }

    void ChengeGear()
    {
        if (Input.GetKeyDown(KeyCode.Q) && _nowGearNam > -1 && _isClutch) _nowGearNam--;
        if (Input.GetKeyDown(KeyCode.E) && _nowGearNam < _maxGear && _isClutch) _nowGearNam++;
    }


    void NowMaxSpeed()
    {
        if (_nowGearNam == -1) _nowGearMaxSpeed = _maxBaxkSpeed;
        else _nowGearMaxSpeed = _maxGearSpeed[_nowGearNam];
    }

    void CalculateRPM()
    {
        if (_isClutch || _nowGearNam == 0)
        {
            float throttle = Mathf.Clamp01(Input.GetAxis("Vertical"));
            float targetRPM = Mathf.Lerp(_idelRPM, _meterMAXRPM, throttle);
            _nowRPM = Mathf.Lerp(_nowRPM, _meterMAXRPM, _engenRespons * Time.deltaTime);
            return;
        }
        float wheelRPM = Mathf.Abs(_driveWheel.rpm);
        float nowRPM = wheelRPM * _gearPow[_nowGearNam] * _fRR;
        nowRPM = Mathf.Clamp(_nowRPM, _idelRPM, _maxRPM);
        if(_nowRPM >= _maxRPM - 50f)
        {
            _nowRPM += Random.Range(-100, 100);
        }
    }

}
