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
    [SerializeField] Text _customTurboBuyText;
    [SerializeField] Dropdown _customECUDropDown;
    [SerializeField] Text _customECUBuyText;
    private string[] _turboDropDownName;
    private string[] _customECUDropdownName;

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
        ClearDropDown();
        AttachDropDownName();
        ChengeDropDownToStart();
    }

    private void ClearDropDown()
    {
        _customTurboDropDown.ClearOptions();
        _customECUDropDown.ClearOptions();
    }

    private void AttachDropDownName()
    {
        _turboDropDownName = _carSpecAttach.carTuneSpec.turboPartsDatas.Select(data => data != null ? data.partName : "None").ToArray();
        _customTurboDropDown.AddOptions(new List<string>(_turboDropDownName));
        _customECUDropdownName = _carSpecAttach.carTuneSpec.customECUDatas.Select(data => data != null ? data.partName : "None").ToArray();
        _customECUDropDown.AddOptions(new List<string>(_customECUDropdownName));


    }

    private void ChengeDropDownToStart()
    {
        _customTurboDropDown.value = _carSpecAttach.customSettingNam.nowTurboNam;
        if (_carSpecAttach.carTuneSpec.turboPartsDatas[_customTurboDropDown.value].canUse) _customTurboBuyText.text = "購入済み";
        else _customTurboBuyText.text = "未購入";
        _customECUDropDown.value = _carSpecAttach.customSettingNam.nowCustomEcuNam;
        if (_carSpecAttach.carTuneSpec.customECUDatas[_customECUDropDown.value].canUse) _customECUBuyText.text = "購入済み";
        else _customECUBuyText.text = "未購入";

    }

    private void AddUIMesod()
    {
        _customTurboDropDown.onValueChanged.AddListener(ChengeTurbo);
        _customECUDropDown.onValueChanged.AddListener(ChengeECU);
    }

    //以降各変更時のプログラム

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
    private void ChengeECU(int index)
    {
        if (_carSpecAttach.carTuneSpec.customECUDatas[index].canUse)
        {
            _customECUBuyText.text = "購入済み";
            _carSpecAttach.customSettingNam.nowCustomEcuNam = index;
            AttachECU(index);
        }
        else
        {
            _customECUBuyText.text = "未購入";
        }
    }
    private void AttachECU(int index)
    {
        _wheelsController.moreTorque = _wheelsController.moreTorque + _carSpecAttach.carTuneSpec.customECUDatas[index].addTorquePow;
    }
}
