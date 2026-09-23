using UnityEngine;

public class CarEngenAndGearBox : MonoBehaviour
{
    Rigidbody _rb;
    [SerializeField] bool _isPlayer;
    [Range(-1f, 1f)] public float _verticalInput;
    [Range(-1f, 1f)] public float _horizontalInput;
    [Header("エンジン")]
    [SerializeField] public float _idelRPM = 1000f;
    [SerializeField] public float _maxRPM = 6800;
    [SerializeField]public float _meterMAXRPM = 8000;
    public float _nowRPM;
    public float _nowSpeed;
    [SerializeField] public float _engenRespons = 10;
    [SerializeField] WheelCollider _driveWheel;
    [SerializeField] public float _fRRDefault = 3.266f;//FRRの初期値
    public float _fRR;
    [SerializeField] public float _tireRadius = 0.32f;
    public bool _isHightEngenRoal = false;
    [Header("ギアボックス")]
    [SerializeField]  public Mission _mission = Mission.MT;
    [SerializeField] public bool _isRevers = false;
    [SerializeField] public float[] _gearPow = { 3.226f, 0, 3.724f, 2.246f, 1.541f, 1.205f, 1.000f, 0.818f };
    [SerializeField] public float[] _maxGearSpeed = { 77, 0, 67, 111, 162, 207, 250, 305 };
    public int _nowGearNam = 2;
    [SerializeField] public int _maxGear;
    public float _nowGearMaxSpeed;
    public bool _isClutch = false;
    public bool _isMT = true;

    private void Start()
    {
        _isPlayer = CompareTag("PlayerCar");
        _maxGear = _gearPow.Length - 1;
        _rb = GetComponent<Rigidbody>();
        if (_mission == Mission.MT) _isMT = true;
        else _isMT = false;
    }
    void Update()
    {
        _verticalInput = Input.GetAxis("Vertical");
        _horizontalInput = Input.GetAxis("Horizontal");
        if (_isPlayer)
        {
            Clutch();
        }
        NowMaxSpeed();
        ChengeGear();
        CalculateRPM();
    }

    void ChengeGear()
    {
        if (_mission == Mission.MT)
        {
            if (Input.GetKeyDown(KeyCode.Q) && _nowGearNam > 0 && _isClutch) _nowGearNam--;
            if (Input.GetKeyDown(KeyCode.E) && _nowGearNam < _maxGear && _isClutch) _nowGearNam++;
            return;
        }
        if(_mission == Mission.AT)
        {
            if(Input.GetKeyDown(KeyCode.Q))_isRevers = true;
            if(Input.GetKeyDown(KeyCode.E))_isRevers = false;
            float speedKmh = _rb.linearVelocity.magnitude * 3.6f;
            if(_isRevers)
            {
                _nowGearNam = 0;
                return;
            }
            if (_isRevers == false)
            {
                if (_nowGearNam <= 1 && _verticalInput > 0.1f) _nowGearNam = 2;
                if (_nowGearNam > 1 && _nowGearNam < _maxGear && _nowRPM > _maxRPM - 800) _nowGearNam++;
                if (_nowGearNam > 2 && _nowRPM < _idelRPM + 2000) _nowGearNam--;
                Debug.Log("現在のギアは"+_nowGearNam);

            }
        }
    }


    void NowMaxSpeed()
    {
        _nowGearMaxSpeed = _maxGearSpeed[_nowGearNam];
    }

    void CalculateRPM()//実車のタイヤのサイズと移動している速度を使ってRPMを求める
    {
        if (_isClutch || _nowGearNam == 1)
        {
            float throttle = Mathf.Clamp01(_verticalInput);
            float targetRPM = Mathf.Lerp(_idelRPM, _meterMAXRPM, throttle);
            _nowRPM = Mathf.Lerp(_nowRPM, targetRPM , _engenRespons * Time.deltaTime);
            return;
        }
        float targetEngineRPM = _idelRPM;
         _nowSpeed = Mathf.Abs(_rb.linearVelocity.magnitude);
        float tireCircumference = 2f * Mathf.PI * _tireRadius;
        float calcuatedWeelRPM = (_nowSpeed * 60) / tireCircumference;
        targetEngineRPM = calcuatedWeelRPM * _gearPow[_nowGearNam] * _fRR;
        targetEngineRPM = Mathf.Max(targetEngineRPM, _idelRPM);
        _nowRPM = Mathf.Lerp(_nowRPM, targetEngineRPM, _engenRespons * Time.deltaTime);
        _nowRPM = Mathf.Clamp(_nowRPM, _idelRPM, _maxRPM);
        if (_nowRPM >= _maxRPM - 50f && _verticalInput > 0.1)
        {
            _nowRPM += Random.Range(-100, 100);
        }

        if(_nowRPM >= _maxRPM - 1500f) _isHightEngenRoal = true;
        else _isHightEngenRoal = false;
    }

    void Clutch()
    {
        if (Input.GetKey(KeyCode.LeftShift)) _isClutch = true;
        else _isClutch = false;
    }

    public enum Mission
    {
        AT,
        MT,
    }

    private void OnEnable()
    {
        if(_mission == Mission.MT)_isMT = true;
        else _isMT = false;
    }
}
