using System;
using UnityEngine;

public class CarSpecAttachAndCostomINF : MonoBehaviour
{
    [SerializeField] private CarSpecINF _carSpecINF;
    public CarSpec _carSpec;
    [SerializeField] private CarTuneINF _carTuneINF;
    public CarTune carTuneSpec;
    public CostomSettingNam customSettingNam;
    private Rigidbody _carRb;
    private CarEngenAndGearBox _carEngenAndGearBox;
    private WheelsController _wheelsController;
    private void Awake()
    {
        _carRb = GetComponent<Rigidbody>();
        _wheelsController = GetComponent<WheelsController>();
        _carEngenAndGearBox = GetComponent<CarEngenAndGearBox>();
        _carSpec = _carSpecINF.carspec;
        carTuneSpec = _carTuneINF.carTuneSpec;
        Attach();
    }

    private void Attach()
    {
        _carRb.mass = _carSpec.mass;
        _wheelsController.torqueCurve = _carSpec.torqueCurve;
        _wheelsController.moreTorque = _carSpec.moreTorque;
        _wheelsController.downForce = _carSpec.downForce;
        _carEngenAndGearBox.idelRPM = _carSpec.idleRPM;
        _carEngenAndGearBox.maxRPM = _carSpec.maxRPM;
        _carEngenAndGearBox.engenRespons = _carSpec.enginqResponse;
        _carEngenAndGearBox.fRRDefault = _carSpec.fRR;
        _carEngenAndGearBox.fRR = _carSpec.fRR;
        Wheel[] wheels = _wheelsController.wheels;
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
    [Serializable]
    public struct CostomSettingNam
    {
        public int nowTurboNam;
        public int nowCustomEcuNam;
        public int nowTireNam;
        public int nowWeightNam;
    }
}
