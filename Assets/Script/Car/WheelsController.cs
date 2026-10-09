using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


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
    [SerializeField] private Wheel flontLeft;
    [SerializeField] private Wheel flontRight;
    [SerializeField] private Wheel rearLeft;
    [SerializeField] private Wheel rearRight;
    public Wheel[] wheels;
    public JointSpring _defaultSpring;//サスペンションの初期設定
    [Header("走行設定")]
    [SerializeField] private float _moveWheelSam = 2f;//駆動するタイヤの数
    [SerializeField] public AnimationCurve torqueCurve;//トルク曲線
    [SerializeField] public float moreTorque = 451f;//最大トルク
    [SerializeField] public float breakeTorque = 600f;//ブレーキ力
    [SerializeField] public bool isLSD = false;//LSDを装備しているかどうか
    [SerializeField] public float iSDStartRPM　= 50;//LSDを有効化する回転の差
    [SerializeField][Range(0, 1)] public float lSDLockLate = 0.5f;//LSDの補正率
    [SerializeField] public float maxSteerAngle = 30f;//最大ステアリング角度
    [SerializeField] private Vector3 _centerOfMassOffset = new Vector3(0, -0.5f, 0); // Yを少し下げる
    [SerializeField] public float downForce = 50;
    public float _carMass;
    private Rigidbody _rb;
    [Header("エンジンとギアボックス")]
    [SerializeField] private CarEngenAndGearBox _engenAndGearBox;

    private float _nowMotor;
    private float _nowTorqueRate;
    public bool isBraking;
    [Range(-1f,1f)]public float verticalInput;
    [Range(-1f, 1f)] public float horizontalInput;
    GameObject _mGM;
    HomeChecker _homeChecker;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.centerOfMass = _centerOfMassOffset;  
        _carMass = _rb.mass;
        wheels = new Wheel[] { flontLeft, flontRight, rearLeft, rearRight };//各タイヤの設定をまとめておくことでforeachで簡単に処理できるようにしている
        foreach (var wheel in wheels)
        {
            wheel.defaultforwardWheelFrictionCurve = wheel.collider.forwardFriction;
        }
        _defaultSpring = wheels[0].collider.suspensionSpring;
        _mGM = GameObject.FindGameObjectWithTag("MainGameManager");
        _homeChecker = _mGM.GetComponent<HomeChecker>();
    }
    private void FixedUpdate()
    {
        if (_homeChecker.isHome) return;
        verticalInput = ControllerINF.varticalInput;
        horizontalInput = ControllerINF.horizontalInput;
        CalculationTorque();
        GivePow();
        LSD();
    }

    private void GivePow()
    {
        isBraking = ControllerINF.isBrake;
        float downforcePow = Mathf.Clamp(downForce * _rb.linearVelocity.magnitude * _rb.linearVelocity.magnitude, 0f, _rb.mass * 5f);
        _rb.AddForce(Vector3.down * downforcePow,ForceMode.Force);
        if (isBraking || _engenAndGearBox.isClutch)//各ギアやブレーキ中クラッチ中によって伝える力をかえている
        {
            _nowMotor = 0f;
        }
        else if (!isBraking && _engenAndGearBox.nowGearNam == 0)
        {
            _nowMotor = -verticalInput * moreTorque * _nowTorqueRate * _engenAndGearBox.gearPow[0] * _engenAndGearBox.fRR / _moveWheelSam;
        }
        else if (!isBraking) _nowMotor = verticalInput * moreTorque * _nowTorqueRate * _engenAndGearBox.gearPow[_engenAndGearBox.nowGearNam] * _engenAndGearBox.fRR  / _moveWheelSam;

        float currentBreake = isBraking ? breakeTorque : 0f;
        float currentSteer = horizontalInput * maxSteerAngle;
        foreach (Wheel wheel in wheels)//各タイヤに力を与える処理をしている。ついでに見た目があうようにもしている
        {
            if (wheel.isMotor && _engenAndGearBox.nowRPM < _engenAndGearBox.maxRPM)
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
        if (!isLSD) return;
        if(_moveWheelSam == 2)
        {
            float leftRPM = rearLeft.collider.rpm;
            float rearRPM = rearRight.collider.rpm;
            float RPMDifference = leftRPM - rearRPM;
            if (RPMDifference > iSDStartRPM)
            {
                WheelFrictionCurve leftWFC = rearLeft.collider.forwardFriction;
                leftWFC.stiffness = rearLeft.defaultforwardWheelFrictionCurve.stiffness + lSDLockLate;
                rearLeft.collider.forwardFriction = leftWFC;
                rearRight.collider.forwardFriction = rearRight.defaultforwardWheelFrictionCurve;
            }
            else if (RPMDifference < -iSDStartRPM)
            {
                WheelFrictionCurve rearWFC = rearRight.collider.forwardFriction;
                rearWFC.stiffness = rearRight.defaultforwardWheelFrictionCurve.stiffness + lSDLockLate;
                rearRight.collider.forwardFriction = rearWFC;
                rearLeft.collider.forwardFriction = rearLeft.defaultforwardWheelFrictionCurve;
            }
            else
            {
                rearLeft.collider.forwardFriction = rearLeft.defaultforwardWheelFrictionCurve;
                rearRight.collider.forwardFriction = rearRight.defaultforwardWheelFrictionCurve;
            }
        }
    }

    private void CalculationTorque()
    {
        float nowRPM = Mathf.Max(_engenAndGearBox.nowRPM, _engenAndGearBox.idelRPM);
        float _nowRPMRate = Mathf.InverseLerp(_engenAndGearBox.idelRPM, _engenAndGearBox.maxRPM, nowRPM);
        _nowTorqueRate = torqueCurve.Evaluate(_nowRPMRate);
        float speedKM = _rb.linearVelocity.magnitude * 3.6f;
        float inputMove = Mathf.Abs(verticalInput);
        if (speedKM > 15 && inputMove > 0.01f)
        {
            _nowTorqueRate = Mathf.Max(_nowTorqueRate, inputMove * 0.5f);
        }
    }

    //private void OnEnable()
    //{
    //    GameObject mGM = GameObject.FindGameObjectWithTag("MainGameManager");
    //    HomeChecker homeChecker = mGM.GetComponent<HomeChecker>();
    //    if (homeChecker.isHome) _rb.isKinematic = true;
    //    else _rb.isKinematic = false;
    //}
}
