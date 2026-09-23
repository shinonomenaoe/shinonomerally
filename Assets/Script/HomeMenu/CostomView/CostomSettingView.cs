using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CostomSettingView : MonoBehaviour
{
    GameObject _playerCar;
    CarEngenAndGearBox _carEngenAndGearBox;
    WheelsController _wheelsController;
    [Header("UI")]
    [SerializeField] Slider _fRRSlider;
    [SerializeField] Text _fRRText;
    [SerializeField] Slider _sasSlider;
    [SerializeField] Text _sasText;
    [SerializeField] Slider _lSDLateSlider;
    [SerializeField] Text _lSDLateText;
    [SerializeField] Slider _wheelAngleSlider;
    [SerializeField] Text _wheelAngleText;
    [SerializeField] Dropdown _missionDropDown;
    [SerializeField] Dropdown _bodyColorDropDown;

    private void Awake()
    {
        SetUpSliders();
        AddUIMesod();
    }
    private void OnEnable()
    {
        _playerCar = GameObject.FindWithTag("PlayerCar");
        _carEngenAndGearBox = _playerCar.GetComponent<CarEngenAndGearBox>();
        _wheelsController = _playerCar.GetComponent<WheelsController>();
        StartSliderSetUp();
        StartSetUpDropDown();
    }

    void ChangeFRR(float rate)
    {
        _carEngenAndGearBox._fRR = rate;
        _fRRText.text = $"{rate:F2}";
    }
    void ChengeSas(float rate)
    {
        foreach (var wheel in _wheelsController._wheels)
        {
            JointSpring spring = _wheelsController._defaultSpring;
            spring.spring = spring.spring * rate;
            spring.damper = spring.damper * rate;
            wheel.collider.suspensionSpring = spring;
        }
        _sasText.text = $"{rate:F2}";
    }
    void ChengeLSDRate(float rate)
    {
        _wheelsController._lSDLockLate = rate;
        _lSDLateText.text = $"{rate:F2}";
    }

    void ChengeWheelAngle(float rate)
    {
        _wheelsController._maxSteerAngle = rate;
        _wheelAngleText.text = $"{rate:F2}";
    }

    void ChengeMission(int index)
    {
        _carEngenAndGearBox._mission = (CarEngenAndGearBox.Mission)index;
    }

    void SetUpSliders()
    {
        _lSDLateSlider.maxValue = 1f;
        _lSDLateSlider.minValue = 0.1f;
        _sasSlider.maxValue = 1.5f;
        _sasSlider.minValue = 0.5f;
        _sasSlider.value = 1f;
        _wheelAngleSlider.maxValue = 45f;
        _wheelAngleSlider.minValue = 20f;
    }
    void StartSetUpDropDown()
    {
        _missionDropDown.ClearOptions();
        string[] missionName = Enum.GetNames(typeof(CarEngenAndGearBox.Mission));
        _missionDropDown.AddOptions(new List<string>(missionName));
        _missionDropDown.value = (int)_carEngenAndGearBox._mission;
        _bodyColorDropDown.ClearOptions();

    }
    void StartSliderSetUp()
    {
        float nowFRR = _carEngenAndGearBox._fRR;
        _fRRSlider.maxValue = _carEngenAndGearBox._fRRDefault + 1f;
        _fRRSlider.minValue = _carEngenAndGearBox._fRRDefault - 1f;
        _fRRSlider.SetValueWithoutNotify(nowFRR);
        ChangeFRR(_fRRSlider.value);
        _sasSlider.SetValueWithoutNotify(_wheelsController._wheels[0].collider.suspensionSpring.spring / _wheelsController._defaultSpring.spring);
        ChengeSas(_sasSlider.value);
        _lSDLateSlider.value = _wheelsController._lSDLockLate;
        ChengeLSDRate(_lSDLateSlider.value);
        _wheelAngleSlider.value = _wheelsController._maxSteerAngle;
        ChengeWheelAngle(_wheelAngleSlider.value);
    }

    private void AddUIMesod()
    {
        _fRRSlider.onValueChanged.AddListener(ChangeFRR);
        _sasSlider.onValueChanged.AddListener(ChengeSas);
        _lSDLateSlider.onValueChanged.AddListener(ChengeLSDRate);
        _wheelAngleSlider.onValueChanged.AddListener(ChengeWheelAngle);
        _missionDropDown.onValueChanged.AddListener(ChengeMission);
    }

    public void ChengeLSDMode(bool isOn)
    {
        _wheelsController._isLSD = isOn;
    }
}
