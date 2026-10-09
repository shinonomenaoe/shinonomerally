using UnityEngine;

public class CarEngenAndGearBox : MonoBehaviour
{
    Rigidbody _rb;
    [SerializeField] bool _isPlayer;
    [Range(-1f, 1f)] public float verticalInput;
    [Range(-1f, 1f)] public float horizontalInput;
    [Header("エンジン")]
    [SerializeField] public float idelRPM = 1000f;
    [SerializeField] public float maxRPM = 6800;
    [SerializeField]public float meterMAXRPM = 8000;
    public float nowRPM;
    public float nowSpeed;
    [SerializeField] public float engenRespons = 10;
    [SerializeField] WheelCollider _driveWheel;
    [SerializeField] public float fRRDefault = 3.266f;//FRRの初期値
    public float fRR;
    [SerializeField] public float tireRadius = 0.32f;
    public bool isHightEngenRoal = false;
    [Header("ギアボックス")]
    [SerializeField]  public Mission mission = Mission.MT;
    [SerializeField] public bool isRevers = false;
    [SerializeField] public float[] gearPow = { 3.226f, 0, 3.724f, 2.246f, 1.541f, 1.205f, 1.000f, 0.818f };
    [SerializeField] public float[] _maxGearSpeed = { 77, 0, 67, 111, 162, 207, 250, 305 };
    public int nowGearNam = 2;
    [SerializeField] public int maxGear;
    public float nowGearMaxSpeed;
    public bool isClutch = false;
    public bool isMT = true;

    private void Start()
    {
        _isPlayer = CompareTag("PlayerCar");
        maxGear = gearPow.Length - 1;
        _rb = GetComponent<Rigidbody>();
        if (mission == Mission.MT) isMT = true;
        else isMT = false;
    }
    void Update()
    {
        verticalInput = ControllerINF.varticalInput;
        horizontalInput = ControllerINF.horizontalInput;
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
        if (mission == Mission.MT)
        {
            if (Input.GetKeyDown(KeyCode.Q) && nowGearNam > 0 && isClutch) nowGearNam--;
            if (Input.GetKeyDown(KeyCode.E) && nowGearNam < maxGear && isClutch) nowGearNam++;
            return;
        }
        if(mission == Mission.AT)
        {
            if(Input.GetKeyDown(KeyCode.Q))isRevers = true;
            if(Input.GetKeyDown(KeyCode.E))isRevers = false;
            float speedKmh = _rb.linearVelocity.magnitude * 3.6f;
            if(isRevers)
            {
                nowGearNam = 0;
                return;
            }
            if (isRevers == false)
            {
                if (nowGearNam <= 1 && verticalInput > 0.1f) nowGearNam = 2;
                if (nowGearNam > 1 && nowGearNam < maxGear && nowRPM > maxRPM - 800) nowGearNam++;
                if (nowGearNam > 2 && nowRPM < idelRPM + 2000) nowGearNam--;
                Debug.Log("現在のギアは"+nowGearNam);

            }
        }
    }


    void NowMaxSpeed()
    {
        nowGearMaxSpeed = _maxGearSpeed[nowGearNam];
    }

    void CalculateRPM()//実車のタイヤのサイズと移動している速度を使ってRPMを求める
    {
        if (isClutch || nowGearNam == 1)
        {
            float throttle = Mathf.Clamp01(verticalInput);
            float targetRPM = Mathf.Lerp(idelRPM, meterMAXRPM, throttle);
            nowRPM = Mathf.Lerp(nowRPM, targetRPM , engenRespons * Time.deltaTime);
            return;
        }
        float targetEngineRPM = idelRPM;
         nowSpeed = Mathf.Abs(_rb.linearVelocity.magnitude);
        float tireCircumference = 2f * Mathf.PI * tireRadius;
        float calcuatedWeelRPM = (nowSpeed * 60) / tireCircumference;
        targetEngineRPM = calcuatedWeelRPM * gearPow[nowGearNam] * fRR;
        targetEngineRPM = Mathf.Max(targetEngineRPM, idelRPM);
        nowRPM = Mathf.Lerp(nowRPM, targetEngineRPM, engenRespons * Time.deltaTime);
        nowRPM = Mathf.Clamp(nowRPM, idelRPM, maxRPM);
        if (nowRPM >= maxRPM - 50f && verticalInput > 0.1)
        {
            nowRPM += Random.Range(-100, 100);
        }

        if(nowRPM >= maxRPM - 1500f) isHightEngenRoal = true;
        else isHightEngenRoal = false;
    }

    void Clutch()
    {
        if (Input.GetKey(KeyCode.LeftShift)) isClutch = true;
        else isClutch = false;
    }

    public enum Mission
    {
        AT,
        MT,
    }

    private void OnEnable()
    {
        _isPlayer = CompareTag("PlayerCar");
        maxGear = gearPow.Length - 1;
        _rb = GetComponent<Rigidbody>();
        if (mission == Mission.MT)isMT = true;
        else isMT = false;
    }
}
