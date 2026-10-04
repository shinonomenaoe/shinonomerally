using UnityEngine;

public class Stabilizer : MonoBehaviour
{
    private Rigidbody _rb;
    [Header("物理スタビライザー")]
    [Tooltip("タイヤの浮きを抑える力　4000～8000推奨")]
    [SerializeField] public float antiRollForce = 5000f;

    [Header("旋回スタビリティ")]
    [Tooltip("ニュートラル時のスピン抑制力")]
    [SerializeField] public float yawDamping = 12;

    Wheel[] _wheels;
    WheelsController _wheelsController;
    public bool _isStabilaize = true;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _wheelsController = GetComponent<WheelsController>();
        _wheels = _wheelsController._wheels;
    }

    private void FixedUpdate()
    {
        if (_isStabilaize)
        {
            AntiRollBar(_wheels[0].collider, _wheels[1].collider);
            AntiRollBar(_wheels[2].collider, _wheels[3].collider);
            ApplyYawStabiizer();
        }
    }

    private void AntiRollBar(WheelCollider left, WheelCollider right)
    {
        WheelHit hit;
        float travelL = 1.0f;
        float travelR = 1.0f;
        bool groundedL = left.GetGroundHit(out hit);
        if (groundedL)
        {
            travelL = (left.transform.InverseTransformPoint(hit.point).y - left.radius) / left.suspensionDistance;
        }
        bool groundedR = right.GetGroundHit(out hit);
        if (groundedR)
        {
            travelR = (right.transform.InverseTransformPoint(hit.point).y - right.radius) / right.suspensionDistance;
        }
        float localAntiRollForce = (travelL - travelR) * antiRollForce;
        if (groundedL)
        {
            _rb.AddForceAtPosition(left.transform.up * -localAntiRollForce, left.transform.position);
        }
        if (groundedR)
        {
            _rb.AddForceAtPosition(right.transform.up * localAntiRollForce, right.transform.position);
        }
    }

    private void ApplyYawStabiizer()
    {
        if(Mathf.Abs(ControllerINF.horizontalInput) < 0.05f)
        {
            Vector3 av = _rb.angularVelocity;
            av.y = Mathf.Lerp(av.y, 0, yawDamping * Time.fixedDeltaTime);
            _rb.angularVelocity = av;
        }
    }
}
