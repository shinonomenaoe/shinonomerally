using UnityEngine;
using UnityEngine.UI;

public class CostomSettingView : MonoBehaviour
{
    GameObject _playerCar;
    CarEngenAndGearBox _carEngenAndGearBox;
    WheelsController _wheelsController;
    float _defaultSpr = 0;
    float _defaultDamp = 0;

    [Header("UI")]
    [SerializeField] Slider _fRRSlider;
    [SerializeField] Text _fRRText;
    [SerializeField] Slider _sasSlider;
    [SerializeField] Text _sasText;

    private void OnEnable()
    {
        _playerCar = GameObject.FindWithTag("PlayerCar");
        _carEngenAndGearBox = _playerCar.GetComponent<CarEngenAndGearBox>();
        _wheelsController = _playerCar.GetComponent<WheelsController>();
        StartFRRSetting();
        StartSas();
    }

    void StartFRRSetting()
    {
        _fRRSlider.maxValue = 1.2f;
        _fRRSlider.minValue = 0.8f;
        _fRRSlider.value = 1f;
        _fRRSlider.onValueChanged.AddListener(ChangeFRR);
        ChangeFRR(_fRRSlider.value);
    }
    void ChangeFRR(float Rate)
    {
        _carEngenAndGearBox._fRR = _carEngenAndGearBox._fRRDefault * Rate;
        _fRRText.text = $"{Rate:F2}";
    }

    void StartSas()
    {
        if(_defaultSpr == 0 && _defaultDamp == 0)
        {
            _defaultSpr = _wheelsController._wheels[0].collider.suspensionSpring.spring;
            _defaultDamp = _wheelsController._wheels[0].collider.suspensionSpring.damper;
        }
        _sasSlider.maxValue = 1.5f;
        _sasSlider.minValue = 0.5f;
        _sasSlider.value = 1f;
        _sasSlider.onValueChanged.AddListener(ChengeSas);
        ChengeSas(_sasSlider.value);

    }

    void ChengeSas(float rate)
    {
        foreach(var wheel in _wheelsController._wheels)
        {
            JointSpring spring = wheel.collider.suspensionSpring;
            spring.spring = _defaultSpr * rate;
            spring.damper = _defaultDamp * rate;
            wheel.collider.suspensionSpring = spring;
        }
    }
}
