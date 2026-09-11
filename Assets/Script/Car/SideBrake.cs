using UnityEngine;

public class SideBrake : MonoBehaviour
{
    WheelsController _wheelsController;
    [SerializeField] WheelCollider _rearLeft;
    [SerializeField] WheelCollider _rearReft;
    [Header("各種サイドブレーキ設定")]
    [SerializeField] float _sideBrakeTorque = 4000f;
    [SerializeField] float _extremumSlip = 0.3f;
    [SerializeField] float _extremumValue = 0.5f;
    [SerializeField] float _asymptoteSlip = 0.8f;
    [SerializeField] float _asymptoteValue = 0.4f;
    [SerializeField] float _stiffness = 0.3f;
    private WheelFrictionCurve _wheelDifoltFrictionCurve;





    private WheelCollider[] _sideBrakewheels;
    private void Awake()
    {
        _sideBrakewheels = new WheelCollider[] { _rearLeft, _rearReft };
        _wheelsController = GetComponent<WheelsController>();
        _wheelDifoltFrictionCurve = _rearLeft.sidewaysFriction;
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
        }
        else
        {
            foreach(WheelCollider wheel in _sideBrakewheels)
            {
                wheel.sidewaysFriction = _wheelDifoltFrictionCurve;
                wheel.brakeTorque = 0f;
            }
        }
    }
}
