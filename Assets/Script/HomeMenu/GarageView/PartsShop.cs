using UnityEngine;
using UnityEngine.UI;

public class PartsShop : MonoBehaviour
{
    GameObject _playerCar;
    CarSpecAttachAndCostomINF _carSpecAttachAndCostomINF;
    [SerializeField] Dropdown _carDropDown;
    [SerializeField] Dropdown _carPartDropDown;
    [SerializeField] Dropdown _carpartsDropDown;
    ScriptableObject[]  _carPart;
    string[] _carsName;
    [SerializeField] string[] _carPartName = { "ターボ","カスタムECU","タイヤ","軽量化" };
    string[] _carPartsName;
    private void Awake()
    {
        AddUIMesod();
    }
    private void OnEnable()
    {
        _playerCar = GameObject.FindWithTag("PlayerCar");
        _carSpecAttachAndCostomINF = _playerCar.GetComponent<CarSpecAttachAndCostomINF>();
        ClearDropDown();
        TakePartsInfoDropDown();
    }

    private void AddUIMesod()
    {
        //_carDropDown.onValueChanged.AddListener();
        //_carPartDropDown.onValueChanged.AddListener();
        //_carpartsDropDown.onValueChanged.AddListener();
    }

    private void ClearDropDown()
    {
        _carDropDown.ClearOptions();
        _carpartsDropDown.ClearOptions();
    }

    private void TakePartsInfoDropDown()
    {
   
    }


}
