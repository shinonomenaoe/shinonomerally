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
        FirstSet();
        _carPartsDropDown.AddOptions(TuningPartsSelector(_carPartsNamEnum));
        _carPartsDropDown.RefreshShownValue();
    }

    private void FirstSet()
    {
        CarPartDropDownFirstSet();
    }

    private void CarPartDropDownFirstSet()
    {
        _carPartDropDown.value = 0;
        _carPartsNamEnum = (CarPartsName)_carPartDropDown.value;
        _explanatoryText.text = TuningPartsTextSelector(_carPartsNamEnum)[0];
        _priceText.text = TuningPartsPriceSelector(_carPartsNamEnum)[0];
    }

    //パーツの選択時のドロップダウンの表示
    public List<string> TuningPartsSelector(CarPartsName targetType)
    {
        FieldInfo[] fields = typeof(CarTune).GetFields(BindingFlags.Instance | BindingFlags.Public);
        List<ScriptableObject> allPartsList = new List<ScriptableObject>();
        foreach (FieldInfo field in fields)
        {
            if (field.FieldType.IsArray)
            {
                Array array = field.GetValue(_tune) as Array;
                if (array == null) continue;
                foreach (var item in array)
                {
                    if (item is ScriptableObject so)
                    {
                        allPartsList.Add(so);
                    }
                }
            }
        }
         List<string> resultPartName = allPartsList.Where(part =>
        {
            if (part == null) return false;
            PropertyInfo typePropety = part.GetType().GetProperty("getPartsName");
            if (typePropety != null)
            {
                var val = typePropety.GetValue(part);
                return val != null && (CarPartsName)val == targetType;
            }
            return false;
        })
        .Select(x =>
        {
            FieldInfo typePropety = x.GetType().GetField("partName");
            return typePropety != null ? (string)typePropety.GetValue(x) : x.name;
        })    
        .ToList();
        return resultPartName;
    }
    //パーツ選択時の説明文
    public List<string> TuningPartsTextSelector(CarPartsName targetType)
    {
        FieldInfo[] fields = typeof(CarTune).GetFields(BindingFlags.Instance | BindingFlags.Public);
        List<ScriptableObject> allPartsList = new List<ScriptableObject>();
        foreach (FieldInfo field in fields)
        {
            if (field.FieldType.IsArray)
            {
                Array array = field.GetValue(_tune) as Array;
                if (array == null) continue;
                foreach (var item in array)
                {
                    if (item is ScriptableObject so)
                    {
                        allPartsList.Add(so);
                    }
                }
            }
        }
        List<string> resultPartName = allPartsList.Where(part =>
        {
            if (part == null) return false;
            PropertyInfo typePropety = part.GetType().GetProperty("getPartsName");
            if (typePropety != null)
            {
                var val = typePropety.GetValue(part);
                return val != null && (CarPartsName)val == targetType;
            }
            return false;
        })
       .Select(x =>
       {
           FieldInfo typePropety = x.GetType().GetField("explanatoryText");
           return typePropety != null ? (string)typePropety.GetValue(x) : x.name;
       })
       .ToList();
        return resultPartName;
    }

    //パーツ選択時の値段表示
    public List<string> TuningPartsPriceSelector(CarPartsName targetType)
    {
        FieldInfo[] fields = typeof(CarTune).GetFields(BindingFlags.Instance | BindingFlags.Public);
        List<ScriptableObject> allPartsList = new List<ScriptableObject>();
        foreach (FieldInfo field in fields)
        {
            if (field.FieldType.IsArray)
            {
                Array array = field.GetValue(_tune) as Array;
                if (array == null) continue;
                foreach (var item in array)
                {
                    if (item is ScriptableObject so)
                    {
                        allPartsList.Add(so);
                    }
                }
            }
        }
        List<string> resultPartName = allPartsList.Where(part =>
        {
            if (part == null) return false;
            PropertyInfo typePropety = part.GetType().GetProperty("getPartsName");
            if (typePropety != null)
            {
                var val = typePropety.GetValue(part);
                return val != null && (CarPartsName)val == targetType;
            }
            return false;
        })
       .Select(x =>
       {
           FieldInfo typePropety = x.GetType().GetField("price"); 
           return typePropety != null ? typePropety.GetValue(x)?.ToString() ?? "0" : "0";
       })
       .ToList();
        return resultPartName;
    }
    //以下ドロップダウン変更時の処理
    private void ChengeCar()
    {

    }

    private void ChengePart(int index)
    {
        _carPartsNamEnum = (CarPartsName)index;
        _carPartsDropDown.ClearOptions();
        var options = TuningPartsSelector(_carPartsNamEnum);
        if(options.Count == 0) options.Add("None");
        _carPartsDropDown.AddOptions(options);
        _carPartsDropDown.SetValueWithoutNotify(0);
        _carPartsDropDown.RefreshShownValue();
        _explanatoryText.text = TuningPartsTextSelector(_carPartsNamEnum)[index];
        _priceText.text = TuningPartsPriceSelector(_carPartsNamEnum)[index];
    }

    private void ChengeParts(int index)
    {
        _explanatoryText.text = TuningPartsTextSelector(_carPartsNamEnum)[index];
        _priceText.text = TuningPartsPriceSelector(_carPartsNamEnum)[index];
    }

    public void BuyParts()
    {

    }
}
