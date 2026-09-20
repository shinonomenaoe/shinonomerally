using UnityEngine;
using UnityEngine.UI;

public class CostomSettingView : MonoBehaviour
{
    GameObject _playerCar;
    CarEngenAndGearBox _carEngenAndGearBox;
    WheelsController _wheelsController;
    float _defaultSas = 0;
    [Header("UI")]
    [SerializeField] Slider _fRRSlider;
    [SerializeField] Text _fRRText;


    private void Start()
    {
        StartFRRSetting();
    }
    private void OnEnable()
    {
        _playerCar = GameObject.FindWithTag("PlayerCar");
        _carEngenAndGearBox = _playerCar.GetComponent<CarEngenAndGearBox>();
        _wheelsController = _playerCar.GetComponent<WheelsController>();
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
        if(_defaultSas == 0 )
        {

        }
    }
}
