using UnityEngine;

public class CarSpecAttach : MonoBehaviour
{
    [SerializeField]
    private CarSpecINF _carSpecINF;
    [SerializeField]
    private CarSpec _carSpec;
    private Rigidbody _carRb;
    private CarEngenAndGearBox _carEngenAndGearBox;
    private WheelsController _wheelsController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        _carRb = GetComponent<Rigidbody>();
        _wheelsController = GetComponent<WheelsController>();
        _carEngenAndGearBox = GetComponent<CarEngenAndGearBox>();
        _carSpec = _carSpecINF.carspec;
        Attach();
    }

    private void Attach()
    {
        _carRb.mass = _carSpec.mass;
        _wheelsController._torqueCurve = _carSpec.torqueCurve;
        _wheelsController._moreTorque = _carSpec.moreTorque;
        _carEngenAndGearBox._idelRPM = _carSpec.idleRPM;
        _carEngenAndGearBox._maxRPM = _carSpec.maxRPM;
        _carEngenAndGearBox._engenRespons = _carSpec.enginqResponse;
        _carEngenAndGearBox._fRRDefault = _carSpec.fRR;
        _carEngenAndGearBox._fRR = _carSpec.fRR;
        Wheel[] wheels = _wheelsController._wheels;
        foreach (var wheel in wheels)
        {
            JointSpring spring = wheel.collider.suspensionSpring;
            spring.spring = _carSpec.suspensionSpring;
            spring.damper = _carSpec.suspensionDamper;
            spring.targetPosition = _carSpec.targetPosition;
            wheel.collider.suspensionSpring = spring;
            wheel.collider.suspensionDistance = _carSpec.suspensionDistance;
        }
    }


}
