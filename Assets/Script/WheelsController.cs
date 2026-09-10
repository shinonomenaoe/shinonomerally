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
    [SerializeField] private float _moreTorque = 451f;//最大トルク
    [SerializeField] private float _breakeTorque = 451f;//ブレーキ力
    [SerializeField] private float _maxSteerAngle = 30f;//最大ステアリング角度
    [Header("エンジンとギアボックス")]
    [SerializeField] private CarEngenAndGearBox _engenAndGearBox;

    private Rigidbody _rb;
    private Wheel[] wheels;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        wheels = new Wheel[] { _flontLeft, _flontRight, _rearLeft, _rearRight };
    }
    private void FixedUpdate()
    {
        float moveInput = Input.GetAxis("Vertical");
        float steerInput = Input.GetAxis("Horizontal");
        bool isBraking = Input.GetKey(KeyCode.Space);
        float nowSpeedKM = _rb.linearVelocity.magnitude * 3.6f;
        float currentMotor = isBraking ? 0f : moveInput * _moreTorque * _engenAndGearBox._gearPow[_engenAndGearBox._nowGearNam] * _engenAndGearBox._fRR;
        float currentBreake = isBraking ? _breakeTorque * _engenAndGearBox._backGeraPow * _engenAndGearBox._fRR : 0f;
        float currentSteer = steerInput * _maxSteerAngle;
        foreach(Wheel wheel in wheels)
        {
            if (wheel.isMotor && nowSpeedKM <= _engenAndGearBox._nowGearMaxSpeed)
            {
                wheel.collider.motorTorque = currentMotor;
            }
            if(nowSpeedKM >= _engenAndGearBox._maxBaxkSpeed)wheel.collider.brakeTorque = currentBreake;
            if (wheel.isSteer) wheel.collider.steerAngle = currentSteer;
            UpdateWheelVisual(wheel);
        }
    }

    private void UpdateWheelVisual(Wheel wheel)
    {
        if(wheel.collider == null || wheel.transform == null) return;
        wheel.collider.GetWorldPose(out Vector3 pos, out Quaternion rot);
        wheel.transform.position = pos;
        wheel.transform.rotation = rot;
    }
}
