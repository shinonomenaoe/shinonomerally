using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.VolumeComponent;

public class PartsShop : MonoBehaviour
{
    GameObject _playerCar;
    CarSpecAttachAndCostomINF _carSpecAttachAndCostomINF;
    CarTune _tune;
    [SerializeField] Text _explanatoryText;
    [SerializeField] Text _priceText;
    [SerializeField] Dropdown _carDropDown;
    private string[] _carName;
    [SerializeField] Dropdown _carPartDropDown;
    private string[] _carPartName = Enum.GetNames(typeof(CarPartsName));
    [SerializeField] Dropdown _carPartsDropDown;
    private string[] _carPartsName;
    string[] _carsName;
    CarPartsName _carPartsNamEnum = CarPartsName.Turbo;
    private void Awake()
    {
        AddUIMesod();
    }
    private void OnEnable()
    {
        _playerCar = GameObject.FindWithTag("PlayerCar");
        _carSpecAttachAndCostomINF = _playerCar.GetComponent<CarSpecAttachAndCostomINF>();
        _tune = _carSpecAttachAndCostomINF.carSpecJson.canUse;
        ClearDropDown();
        TakePartsInfoDropDown();
    }

    private void AddUIMesod()
    {
        //_carDropDown.onValueChanged.AddListener();
        _carPartDropDown.onValueChanged.AddListener(ChengePart);
        _carPartsDropDown.onValueChanged.AddListener(ChengeParts);
    }

    private void ClearDropDown()
    {
        _carDropDown.ClearOptions();
        _carPartDropDown.ClearOptions();
        _carPartsDropDown.ClearOptions();
    }

    private void TakePartsInfoDropDown()
    {
        //車の指定を後で作る
        //_carDropDown.AddOptions();
        _carPartDropDown.AddOptions(new List<string>(_carPartName));
        _carPartsDropDown.AddOptions(PartsDropDownSet(_carPartsNamEnum));
        FirstSet();
        _carPartsDropDown.RefreshShownValue();
    }

    private void FirstSet()
    {
        CarPartDropDownFirstSet();
        CarPartsDropDownFirstSet();
    }

    private void CarPartDropDownFirstSet()
    {
        _carPartDropDown.value = 0;
        _carPartsNamEnum = (CarPartsName)_carPartDropDown.value;
    }

    private void CarPartsDropDownFirstSet()
    {
        _carPartsDropDown.value = 0;
        _explanatoryText.text = PartsTextSet(_carPartsNamEnum)[0];
        _priceText.text = PartsPriceSet(_carPartsNamEnum)[0];
        _carPartsDropDown.RefreshShownValue();
    }

    private List<string> PartsDropDownSet(CarPartsName partsName)
    {
        var list = new List<string>();
        list = _tune.customParts.Where(x => x.carPartsName == partsName && x != null).Select(x => x.partName).ToList();
        return list;
    }

    private List<string> PartsTextSet(CarPartsName partsName)
    {
        var list = new List<string>();
        list = _tune.customParts.Where(x => x.carPartsName == partsName && x != null).Select(x => x.explanatoryText).ToList();
        return list;
    }

    private List<string> PartsPriceSet(CarPartsName partsName)
    {
        var list = new List<string>();
        list = _tune.customParts.Where(x => x.carPartsName == partsName && x != null).Select(x => x.price.ToString()).ToList();
        return list;
    }
    //以下ドロップダウン変更時の処理
    private void ChengeCar()
    {

    }

    private void ChengePart(int index)
    {
        _carPartsNamEnum = (CarPartsName)index;
        _carPartsDropDown.ClearOptions();
        _carPartsDropDown.AddOptions(PartsDropDownSet(_carPartsNamEnum));
        PartsDropDownSet(_carPartsNamEnum);
        CarPartsDropDownFirstSet();
        _carPartsDropDown.RefreshShownValue();
    }

    private void ChengeParts(int index)
    {
        _explanatoryText.text = PartsTextSet(_carPartsNamEnum)[index];
        _priceText.text = PartsPriceSet(_carPartsNamEnum)[index];
    }

    public void BuyParts()
    {

    }
}
