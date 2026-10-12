using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class TuningSetUP : MonoBehaviour
{
    GameObject _playerCar;
    CarEngenAndGearBox _carEngenAndGearBox;
    WheelsController _wheelsController;
    [SerializeField] Dropdown _customEngenDropDown;
    [SerializeField] Text _customEngenText;
    [SerializeField] Text _customEngenBuyText;

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        _playerCar = GameObject.FindGameObjectWithTag("PlayerCar");
        _carEngenAndGearBox = _playerCar.GetComponent<CarEngenAndGearBox>();
        _wheelsController = _playerCar.GetComponent<WheelsController>();
    }
    
    private void ClearDropDown()
    {
        
    }
}
