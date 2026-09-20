using UnityEngine;
using UnityEngine.UI;

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

    private void Awake()
    {
        SetUpSliders();
        _fRRSlider.onValueChanged.AddListener(ChangeFRR);
        _sasSlider.onValueChanged.AddListener(ChengeSas);
        _lSDLateSlider.onValueChanged.AddListener(ChengeLSDRate);
    }
    private void OnEnable()
    {
        _playerCar = GameObject.FindWithTag("PlayerCar");
        _carEngenAndGearBox = _playerCar.GetComponent<CarEngenAndGearBox>();
        _wheelsController = _playerCar.GetComponent<WheelsController>();
        StartSliderSetUp();
    }

    void ChangeFRR(float rate)
    {
        _carEngenAndGearBox._fRR = _carEngenAndGearBox._fRRDefault * rate;
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

    void SetUpSliders()
    {
        _fRRSlider.maxValue = 1.2f;
        _fRRSlider.minValue = 0.8f;
        _fRRSlider.value = 1f;
        _lSDLateSlider.maxValue = 1f;
        _lSDLateSlider.minValue = 0.1f;
        _sasSlider.maxValue = 1.5f;
        _sasSlider.minValue = 0.5f;
        _sasSlider.value = 1f;
    }
    void StartSliderSetUp()
    {
        ChangeFRR(_fRRSlider.value);
        ChengeSas(_sasSlider.value);
        _lSDLateSlider.value = _wheelsController._lSDLockLate;
        ChengeLSDRate(_lSDLateSlider.value);
    }
}
