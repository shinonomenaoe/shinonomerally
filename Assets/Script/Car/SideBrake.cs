using UnityEngine;

public class SideBrake : MonoBehaviour
{
    [Header("ホイール")]
    WheelsController _wheelsController;
    [SerializeField] WheelCollider _flontLeft;
    [SerializeField] WheelCollider _flontRight;
    [SerializeField] WheelCollider _rearLeft;
    [SerializeField] WheelCollider _rearReft;
    [Header("各種サイドブレーキ設定")]
    [SerializeField] float _sideBrakeTorque = 4000f;
    [SerializeField] float _extremumSlip = 0.3f;
    [SerializeField] float _extremumValue = 0.5f;
    [SerializeField] float _asymptoteSlip = 0.8f;
    [SerializeField] float _asymptoteValue = 0.4f;
    [SerializeField] float _stiffness = 0.3f;
    [Header("前輪タイヤの滑り具合")]
    [SerializeField] float _flontStiffness = 1.5f;
    private WheelFrictionCurve _wheelDifoltRearFrictionCurve;
    private WheelFrictionCurve _wheelDifoltFlontFrictionCurve;





    private WheelCollider[] _sideBrakewheels;
    private WheelCollider[] _flontWheels;
    private void Awake()
    {
        _sideBrakewheels = new WheelCollider[] { _rearLeft, _rearReft };
        _flontWheels = new WheelCollider[] { _flontLeft, _flontRight };
        _wheelsController = GetComponent<WheelsController>();
        _wheelDifoltRearFrictionCurve = _rearLeft.sidewaysFriction;
        _wheelDifoltFlontFrictionCurve = _flontLeft.sidewaysFriction;
    }
    void Update()
    {
        OnBrales();
    }

    void OnBrales()
    {
        if (Input.GetMouseButton(0))
        {
            foreach (WheelCollider wheel in _sideBrakewheels)
            {
                wheel.brakeTorque = _sideBrakeTorque;
                WheelFrictionCurve sfriction = wheel.sidewaysFriction;
                sfriction.extremumSlip = _extremumSlip;
                sfriction.extremumValue = _extremumValue;
                sfriction.asymptoteSlip = _asymptoteSlip;
                sfriction.asymptoteValue = _asymptoteValue;
                sfriction.stiffness = _stiffness;
                wheel.sidewaysFriction = sfriction;
            }
            foreach(WheelCollider wheel in _flontWheels)
            {
                WheelFrictionCurve sfriction = wheel.sidewaysFriction;
                sfriction.stiffness = _flontStiffness;
                wheel.sidewaysFriction = sfriction;
            }
        }
        else
        {
            foreach (WheelCollider wheel in _sideBrakewheels)
            {
                wheel.sidewaysFriction = _wheelDifoltRearFrictionCurve;
                wheel.brakeTorque = 0f;
            }
            foreach(WheelCollider wheel in _flontWheels)
            {
                wheel.sidewaysFriction = _wheelDifoltFlontFrictionCurve;
            }

        }
    }
}
