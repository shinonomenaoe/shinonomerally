using UnityEngine;


[System.Serializable]
public class Wheel
{
    public WheelCollider collider;
    public Transform transform;
    public bool isMotor; //駆動輪かどうか
    public bool isSteer;// ステアリング輪かどうか
    public WheelFrictionCurve defaultforwardWheelFrictionCurve;//LSD用の初期カーブ設定
}
[RequireComponent(typeof(Rigidbody))]
public class WheelsController : MonoBehaviour
{
    [Header("ホイール設定")]
    [SerializeField] private Wheel _flontLeft;
    [SerializeField] private Wheel _flontRight;
    [SerializeField] private Wheel _rearLeft;
    [SerializeField] private Wheel _rearRight;
    [Header("走行設定")]
    public float _carMass;
    [SerializeField] private float _moveWheelSam = 2f;//駆動するタイヤの数
    [SerializeField] AnimationCurve _torqueCurve;//トルク曲線
    [SerializeField] public float _moreTorque = 451f;//最大トルク
    [SerializeField] public float _breakeTorque = 600f;//ブレーキ力
    [SerializeField] public bool _isLSD = false;//LSDを装備しているかどうか
    [SerializeField] public float _iSDStartRPM　= 50;//LSDを有効化する回転の差
    [SerializeField][Range(0, 1)] public float _lSDLockLate = 0.5f;//LSDの補正率
    [SerializeField] public float _maxSteerAngle = 30f;//最大ステアリング角度
    [SerializeField] private Vector3 __centerOfMassOffset = new Vector3(0, -0.5f, 0); // Yを少し下げる
    [SerializeField] public float _downForce = 50;
    [Header("エンジンとギアボックス")]
    [SerializeField] private CarEngenAndGearBox _engenAndGearBox;

    private float _nowMotor;
    private float _nowTorqueRate;

    private Rigidbody _rb;
    private Wheel[] wheels;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.centerOfMass = __centerOfMassOffset;
        _carMass = _rb.mass;
        wheels = new Wheel[] { _flontLeft, _flontRight, _rearLeft, _rearRight };//各タイヤの設定をまとめておくことでforeachで簡単に処理できるようにしている
        foreach(var wheel in wheels)
        {
            wheel.defaultforwardWheelFrictionCurve = wheel.collider.forwardFriction;
        }

    }
    private void FixedUpdate()
    {
        CalculationTorque();
        GivePow();
        LSD();
    }

    private void GivePow()
    {
        float moveInput = Input.GetAxis("Vertical");
        float steerInput = Input.GetAxis("Horizontal");
        bool isBraking = Input.GetKey(KeyCode.Space);
        _rb.AddForce(-transform.up * _downForce * _rb.angularVelocity.magnitude);
        float nowSpeedKM = _rb.linearVelocity.magnitude * 3.6f;
        if (isBraking || _engenAndGearBox._isClutch)//各ギアやブレーキ中クラッチ中によって伝える力をかえている
        {
            _nowMotor = 0f;
        }
        else if (!isBraking && _engenAndGearBox._nowGearNam == 0)
        {
            _nowMotor = moveInput * _moreTorque * _nowTorqueRate * -_engenAndGearBox._gearPow[0] * _engenAndGearBox._fRR / _moveWheelSam;
        }
        else if (!isBraking) _nowMotor = moveInput * _moreTorque * _nowTorqueRate * _engenAndGearBox._gearPow[_engenAndGearBox._nowGearNam] * _engenAndGearBox._fRR  / _moveWheelSam;

        float currentBreake = isBraking ? _breakeTorque : 0f;
        float currentSteer = steerInput * _maxSteerAngle;
        foreach (Wheel wheel in wheels)//各タイヤに力を与える処理をしている。ついでに見た目があうようにもしている
        {
            if (wheel.isMotor && nowSpeedKM <= _engenAndGearBox._nowGearMaxSpeed)
            {
                wheel.collider.motorTorque = _nowMotor;//最高速以上出ないようにしている
                wheel.collider.brakeTorque = currentBreake;
            }
            else if (wheel.isMotor)
            {
                wheel.collider.motorTorque = 0f;
                wheel.collider.brakeTorque = currentBreake;
            }
            if (wheel.isSteer) wheel.collider.steerAngle = currentSteer;
            UpdateWheelVisual(wheel);
        }
    }

    private void UpdateWheelVisual(Wheel wheel)//コライダーの向きや角度を見た目用のオブジェクトの各値にいれることで同期している
    {
        if (wheel.collider == null || wheel.transform == null) return;
        wheel.collider.GetWorldPose(out Vector3 pos, out Quaternion rot);
        wheel.transform.position = pos;
        wheel.transform.rotation = rot;
    }

    private void LSD()
    {
        if (!_isLSD) return;
        if(_moveWheelSam == 2)
        {
            float leftRPM = _rearLeft.collider.rpm;
            float rearRPM = _rearRight.collider.rpm;
            float RPMDifference = leftRPM - rearRPM;
            if (RPMDifference > _iSDStartRPM)
            {
                WheelFrictionCurve leftWFC = _rearLeft.collider.forwardFriction;
                leftWFC.stiffness = _rearLeft.defaultforwardWheelFrictionCurve.stiffness + _lSDLockLate;
                _rearLeft.collider.forwardFriction = leftWFC;
                _rearRight.collider.forwardFriction = _rearRight.defaultforwardWheelFrictionCurve;
            }
            else if (RPMDifference < -_iSDStartRPM)
            {
                WheelFrictionCurve rearWFC = _rearRight.collider.forwardFriction;
                rearWFC.stiffness = _rearRight.defaultforwardWheelFrictionCurve.stiffness + _lSDLockLate;
                _rearRight.collider.forwardFriction = rearWFC;
                _rearLeft.collider.forwardFriction = _rearLeft.defaultforwardWheelFrictionCurve;
            }
            else
            {
                _rearLeft.collider.forwardFriction = _rearLeft.defaultforwardWheelFrictionCurve;
                _rearRight.collider.forwardFriction = _rearRight.defaultforwardWheelFrictionCurve;
            }
        }
    }

    private void CalculationTorque()
    {
        float nowRPM = Mathf.Max(_engenAndGearBox._nowRPM, _engenAndGearBox._idelRPM);
        float _nowRPMRate = Mathf.InverseLerp(_engenAndGearBox._idelRPM, _engenAndGearBox._maxRPM, nowRPM);
        _nowTorqueRate = _torqueCurve.Evaluate(_nowRPMRate);
        float speedKM = _rb.linearVelocity.magnitude * 3.6f;
        float inputMove = Mathf.Abs(Input.GetAxis("Vertical"));
        if (speedKM > 15 && inputMove > 0.01f)
        { 
            _nowTorqueRate = Mathf.Max(_nowTorqueRate, inputMove * 0.5f);
        }
            
    }
}
