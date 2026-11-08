using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class PartsShop : MonoBehaviour
{
    GameObject _playerCar;
    CarSpecAttachAndCostomINF _carSpecAttachAndCostomINF;
    CarTune _tune;
    [SerializeField] Dropdown _carDropDown;
    private string[] _carName;
    [SerializeField] Dropdown _carPartDropDown;
    private string[] _carPartName = Enum.GetNames(typeof(CarPartsName));
    [SerializeField] Dropdown _carPartsDropDown;
    private string[] _carPartsName;
    string[] _carsName;
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
        //_carPartDropDown.onValueChanged.AddListener();
        //_carpartsDropDown.onValueChanged.AddListener();
    }

    private void ClearDropDown()
    {
        _carDropDown.ClearOptions();
        _carPartsDropDown.ClearOptions();
    }

    private void TakePartsInfoDropDown()
    {
        _carPartDropDown.AddOptions(new List<string>(_carPartsName));
        //_carPartsName = _carSpecAttachAndCostomINF.carSpecJson.canUse.Where(x => x.)
        //_carPartsDropDown.AddOptions();
    }

    public List<string> TuningPartsSelector()
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
            FieldInfo typeField = part.GetType().GetField("getPartsName");
        });
    }
}
