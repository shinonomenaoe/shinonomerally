using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TuningSetUP : MonoBehaviour
{
    GameObject _playerCar;
    CarEngenAndGearBox _carEngenAndGearBox;
    WheelsController _wheelsController;
    CarSpecAttachAndCostomINF _carSpecAttach;
    [SerializeField] Dropdown _customTurboDropDown;
    [SerializeField] Text _customTurboText;
    [SerializeField] Text _customTurboBuyText;
    [HideInInspector]public string[] turboDropDownName;

    private void Awake()
    {
        AddUIMesod();
    }

    private void OnEnable()
    {
        _playerCar = GameObject.FindGameObjectWithTag("PlayerCar");
        _carEngenAndGearBox = _playerCar.GetComponent<CarEngenAndGearBox>();
        _wheelsController = _playerCar.GetComponent<WheelsController>();
        _carSpecAttach = _playerCar.GetComponent<CarSpecAttachAndCostomINF>();
        StartDropDown();
    }

    private void StartDropDown()
    {
        _customTurboDropDown.ClearOptions();
        AttachDropDownName();
        ChengeDropDownToStart();
    }

    private void AttachDropDownName()
    {
        turboDropDownName = _carSpecAttach.carTuneSpec.turboPartsDatas.Select(data => data != null ? data.partName : "None").ToArray();
        _customTurboDropDown.AddOptions(new List<string>(turboDropDownName));

    }

    private void ChengeDropDownToStart()
    {
        _customTurboDropDown.value = _carSpecAttach.customSettingNam.nowTurboNam;
        if (_carSpecAttach.carTuneSpec.turboPartsDatas[_customTurboDropDown.value].canUse) _customTurboBuyText.text = "購入済み";
        else _customTurboBuyText.text = "未購入";

    }

    private void AddUIMesod()
    {
        _customTurboDropDown.onValueChanged.AddListener(ChengeTurbo);
    }

    private void ChengeTurbo(int index)
    {
        if (_carSpecAttach.carTuneSpec.turboPartsDatas[index].canUse)
        {
            _customTurboBuyText.text = "購入済み";
            _carSpecAttach.customSettingNam.nowTurboNam = index;
            AttachTurbo(index);
        }
        else
        {
            _customTurboBuyText.text = "未購入";
        }

    }
    private void AttachTurbo(int index)
    {
        _wheelsController.torqueCurve = _carSpecAttach.carTuneSpec.turboPartsDatas[index].customTorqueCurve;
        _wheelsController.moreTorque = _carSpecAttach._carSpec.moreTorque * _carSpecAttach.carTuneSpec.turboPartsDatas[index].moreTorqueBonusRate;
    }
}
