using UnityEngine;


[System.Serializable]
public class Wheel
{
    public WheelCollider collider;
    public Transform transform;
    public bool isMotor; //駆動輪かどうか
    public bool isSteer;// ステアリング輪かどうか
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
    [SerializeField] private float _tireSize = 0.643f;//タイヤのサイズ
    [SerializeField] private float _moreTorque = 451f;//最大トルク
    [SerializeField] private float _breakeTorque = 451f;//ブレーキ力
    [SerializeField] private float _maxSteerAngle = 30f;//最大ステアリング角度
    [Header("エンジンとギアボックス")]
    [SerializeField] private CarEngenAndGearBox _engenAndGearBox;

    private float _nowMotor;

    private Rigidbody _rb;
    private Wheel[] wheels;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _carMass = _rb.mass;
        wheels = new Wheel[] { _flontLeft, _flontRight, _rearLeft, _rearRight };//各タイヤの設定をまとめておくことでforeachで簡単に処理できるようにしている
    }
    private void FixedUpdate()
    {
        float moveInput = Input.GetAxis("Vertical");
        float steerInput = Input.GetAxis("Horizontal");
        bool isBraking = Input.GetKey(KeyCode.Space);
        float nowSpeedKM = _rb.linearVelocity.magnitude * 3.6f;
        if (isBraking || _engenAndGearBox._isClutch)//各ギアやブレーキ中クラッチ中によって伝える力をかえている
        {
            _nowMotor = 0f;
        }
        else if (!isBraking && _engenAndGearBox._nowGearNam == 0)
        {
            _nowMotor = moveInput * _moreTorque * -_engenAndGearBox._gearPow[_engenAndGearBox._nowGearNam] * _engenAndGearBox._fRR;
        }
        else if(!isBraking) _nowMotor = moveInput * _moreTorque * _engenAndGearBox._gearPow[_engenAndGearBox._nowGearNam] * _engenAndGearBox._fRR;

        float currentBreake = isBraking ? _breakeTorque : 0f;
        float currentSteer = steerInput * _maxSteerAngle;
        foreach(Wheel wheel in wheels)//各タイヤに力を与える処理をしている。ついでに見た目があうようにもしている
        {
            if (wheel.isMotor && nowSpeedKM <= _engenAndGearBox._nowGearMaxSpeed) wheel.collider.motorTorque = _nowMotor;
            else wheel.collider.motorTorque = 0f;
            wheel.collider.brakeTorque = currentBreake;
            if (wheel.isSteer) wheel.collider.steerAngle = currentSteer;
            UpdateWheelVisual(wheel);
        }
    }

    private void UpdateWheelVisual(Wheel wheel)//コライダーの向きや角度を見た目用のオブジェクトの各値にいれることで同期している
    {
        if(wheel.collider == null || wheel.transform == null) return;
        wheel.collider.GetWorldPose(out Vector3 pos, out Quaternion rot);
        wheel.transform.position = pos;
        wheel.transform.rotation = rot;
    }
}
