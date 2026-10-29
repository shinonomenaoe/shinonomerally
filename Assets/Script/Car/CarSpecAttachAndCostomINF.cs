using System;
using System.IO;
using UnityEngine;
using static CarSpecAttachAndCostomINF;

[System.Serializable]
public class CarSettingAndCostomJson
{
    public float downForce;
    public float idleRPM;
    public float maxRPM;
    public float enginqResponse;
    public float brakeTorque;
    public CostomSettingNam customSettingNam;
    public float fRR;
    public float sasSliderRate = 1.5f;
    public bool isLSD = true;
    public float lSDSliderRate = 0.5f;
    public float wheelAngleSliderRate = 30f;
    public CarEngenAndGearBox.Mission mission = CarEngenAndGearBox.Mission.AT;
    public float stabilizerRatioSliderRate = 0.2f;
}

public class CarSpecAttachAndCostomINF : MonoBehaviour
{
    [SerializeField] string _jsonName;
    [SerializeField] private CarSpecINF _carSpecINF;
    [HideInInspector] public CarSpec _carSpec;
    [SerializeField] private CarTuneINF _carTuneINF;
    [HideInInspector] public CarTune carTuneSpec;
    public CostomSettingNam customSettingNam;
    private Rigidbody _carRb;
    private CarEngenAndGearBox _carEngenAndGearBox;
    private WheelsController _wheelsController;
    private Stabilizer _stabilizer;
    string _filePath => Path.Combine(Application.persistentDataPath, _jsonName);
    [HideInInspector] public CarSettingAndCostomJson carSpecJson = new CarSettingAndCostomJson();
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
        _carRb.mass = _carSpec.mass - carTuneSpec.customWeightDatas[carSpecJson.customSettingNam.nowWeightNam].cutWeight;
        _wheelsController.torqueCurve = carTuneSpec.turboPartsDatas[carSpecJson.customSettingNam.nowTurboNam].customTorqueCurve;
        _wheelsController.moreTorque = (_carSpec.moreTorque * carTuneSpec.turboPartsDatas[carSpecJson.customSettingNam.nowTurboNam].moreTorqueBonusRate) * carTuneSpec.customECUDatas[carSpecJson.customSettingNam.nowCustomEcuNam].addTorquePowRate;
        _wheelsController.downForce = carSpecJson.downForce;
        _carEngenAndGearBox.idelRPM = carSpecJson.idleRPM;
        _carEngenAndGearBox.maxRPM = carSpecJson.maxRPM;
        _carEngenAndGearBox.engenRespons = carSpecJson.enginqResponse;
        _carEngenAndGearBox.fRRDefault = _carSpec.fRR;
        _carEngenAndGearBox.fRR = carSpecJson.fRR;
        Wheel[] wheels = _wheelsController.wheels;
        foreach (var wheel in wheels)
        {
            JointSpring spring = wheel.collider.suspensionSpring;
            spring.spring = _carSpec.suspensionSpring * carSpecJson.sasSliderRate;
            spring.damper = _carSpec.suspensionDamper * carSpecJson.sasSliderRate;
            spring.targetPosition = _carSpec.targetPosition;
            wheel.collider.suspensionSpring = spring;
            wheel.collider.suspensionDistance = _carSpec.suspensionDistance;
            wheel.collider.forwardFriction = carTuneSpec.tirePartsDatas[carSpecJson.customSettingNam.nowTireNam].forwardFriction.ToWheelFrictionCurve();
            wheel.collider.sidewaysFriction = carTuneSpec.tirePartsDatas[carSpecJson.customSettingNam.nowTireNam].sidewayFriction.ToWheelFrictionCurve();
        }
        _wheelsController.lSDLockLate = carSpecJson.lSDSliderRate;
        _wheelsController.maxSteerAngle = carSpecJson.wheelAngleSliderRate;
        _carEngenAndGearBox.mission = carSpecJson.mission;
        _stabilizer.stabilizerRate = carSpecJson.stabilizerRatioSliderRate;
        _wheelsController.isLSD = carSpecJson.isLSD;
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
        if (_filePath == null) throw new NullReferenceException("ファイルの名前が指定されていません");
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
        if (carSpecJson == null) return;
        string json = JsonUtility.ToJson(carSpecJson, true);
        File.WriteAllText(_filePath, json);
    }

    public void FirstSet()
    {
        carSpecJson.downForce = _carSpec.downForce;
        carSpecJson.enginqResponse = _carSpec.enginqResponse;
        carSpecJson.idleRPM = _carSpec.idleRPM;
        carSpecJson.maxRPM = _carSpec.maxRPM;
        carSpecJson.fRR = _carSpec.fRR;
        carSpecJson.brakeTorque = _carSpec.brakeTorque;
    }

    public void ChengeAndSaveJson()
    {
        carSpecJson.downForce = _wheelsController.downForce;
        carSpecJson.idleRPM = _carEngenAndGearBox.idelRPM;
        carSpecJson.maxRPM = _carEngenAndGearBox.maxRPM;
        carSpecJson.enginqResponse = _carEngenAndGearBox.engenRespons;
        carSpecJson.brakeTorque = _wheelsController.breakeTorque;
        carSpecJson.customSettingNam = customSettingNam;
        carSpecJson.fRR = _carEngenAndGearBox.fRR;
        carSpecJson.sasSliderRate = _wheelsController.wheels[0].collider.suspensionSpring.spring / _carSpec.suspensionSpring;
        carSpecJson.stabilizerRatioSliderRate = _stabilizer.stabilizerRate;
        carSpecJson.lSDSliderRate = _wheelsController.lSDLockLate;
        carSpecJson.wheelAngleSliderRate = _wheelsController.maxSteerAngle;
        carSpecJson.mission = _carEngenAndGearBox.mission;
        carSpecJson.isLSD = _wheelsController.isLSD;
        SaveJson();
    }
}
