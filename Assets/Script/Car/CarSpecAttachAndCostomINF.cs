using System;
using System.IO;
using UnityEngine;
using static CarSpecAttachAndCostomINF;

[System.Serializable]
public class CarSettingAndCostomJson
{
    public float mass;
    public float downForce;
    public float idleRPM;
    public float maxRPM;
    public float enginqResponse;
    public float moreTorque;
    public CostomSettingNam costomSettingNam;
    public float fRRSliderRate;
    public float sasSliderRate = 1.5f;
    public float lSDSliderRate = 0.5f;
    public float wheelAngleSliderRate = 30f;
    public CarEngenAndGearBox.Mission mission = CarEngenAndGearBox.Mission.AT;
    public float stabilizerRatioSliderRate = 0.2f;
}

public class CarSpecAttachAndCostomINF : MonoBehaviour
{
    [SerializeField] string _jsonName;
    [SerializeField] private CarSpecINF _carSpecINF;
    [HideInInspector]public CarSpec _carSpec;
    [SerializeField] private CarTuneINF _carTuneINF;
    [HideInInspector]public CarTune carTuneSpec;
    public CostomSettingNam customSettingNam;
    private Rigidbody _carRb;
    private CarEngenAndGearBox _carEngenAndGearBox;
    private WheelsController _wheelsController;
    private Stabilizer _stabilizer;
    string _filePath => Path.Combine(Application.persistentDataPath, _jsonName);
    [HideInInspector]public CarSettingAndCostomJson carSpecJson = new CarSettingAndCostomJson();
    private void Awake()
    {
        _carRb = GetComponent<Rigidbody>();
        _wheelsController = GetComponent<WheelsController>();
        _carEngenAndGearBox = GetComponent<CarEngenAndGearBox>();
        _stabilizer = GetComponent<Stabilizer>();
        _carSpec = _carSpecINF.carspec;
        carTuneSpec = _carTuneINF.carTuneSpec;
        Attach();
    }

    private void Attach()
    {
        LoadJson();
        _carRb.mass = carSpecJson.mass;
        _wheelsController.torqueCurve = _carSpec.torqueCurve;
        _wheelsController.moreTorque = carSpecJson.moreTorque;
        _wheelsController.downForce = carSpecJson.downForce;
        _carEngenAndGearBox.idelRPM = carSpecJson.idleRPM;
        _carEngenAndGearBox.maxRPM = carSpecJson.maxRPM;
        _carEngenAndGearBox.engenRespons = carSpecJson.enginqResponse;
        _carEngenAndGearBox.fRRDefault = _carSpec.fRR;
        _carEngenAndGearBox.fRR = carSpecJson.fRRSliderRate;
        Wheel[] wheels = _wheelsController.wheels;
        foreach (var wheel in wheels)
        {
            JointSpring spring = wheel.collider.suspensionSpring;
            spring.spring = _carSpec.suspensionSpring * carSpecJson.sasSliderRate;
            spring.damper = _carSpec.suspensionDamper * carSpecJson.sasSliderRate;
            spring.targetPosition = _carSpec.targetPosition;
            wheel.collider.suspensionSpring = spring;
            wheel.collider.suspensionDistance = _carSpec.suspensionDistance;
        }
        _wheelsController.lSDLockLate = carSpecJson.lSDSliderRate;
        _wheelsController.maxSteerAngle = carSpecJson.wheelAngleSliderRate;
        _carEngenAndGearBox.mission = carSpecJson.mission;
        _stabilizer.stabilizerRate = carSpecJson.stabilizerRatioSliderRate;
    }
    [Serializable]
    public struct CostomSettingNam
    {
        public int nowTurboNam;
        public int nowCustomEcuNam;
        public int nowTireNam;
        public int nowWeightNam;
    }

    public void LoadJson()
    {
        if (File.Exists(_filePath))
        {
            string json = File.ReadAllText(_filePath);
            carSpecJson = JsonUtility.FromJson<CarSettingAndCostomJson>(json);
        }
        else
        {
            carSpecJson = new CarSettingAndCostomJson();
            FirstSet();
            SaveJson();
        }
    }

    public void SaveJson()
    {
        if(carSpecJson == null) return;
        string json = JsonUtility.ToJson(carSpecJson, true);
        File.WriteAllText(_filePath, json);
    }

    public void FirstSet()
    {
        carSpecJson.mass = _carSpec.mass;
        carSpecJson.downForce = _carSpec.downForce;
        carSpecJson.enginqResponse = _carSpec.enginqResponse;
        carSpecJson.idleRPM = _carSpec.idleRPM;
        carSpecJson.maxRPM = _carSpec.maxRPM;
        carSpecJson.moreTorque = _carSpec.moreTorque;
        carSpecJson.fRRSliderRate = _carSpec.fRR;
    }
}
