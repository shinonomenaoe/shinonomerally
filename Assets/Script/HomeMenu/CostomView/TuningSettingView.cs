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
    Rigidbody _rb;
    CarSpecAttachAndCostomINF _carSpecAttach;
    //[SerializeField] GameObject buyWindow;
    //[SerializeField] Text buyWindwPartNameText;
    //[SerializeField] Text buyWindwPricetext;
    [SerializeField] Dropdown _customTurboDropDown;
    [SerializeField] Text _customTurboBuyText;
    [SerializeField] Dropdown _customECUDropDown;
    [SerializeField] Text _customECUBuyText;
    [SerializeField] Dropdown _tireDropDown;
    [SerializeField] Text _tireBuyText;
    [SerializeField] Dropdown _customWightDropDown;
    [SerializeField] Text _customWightBuyText;
    [SerializeField] Text _partsSpecText;
    private string[] _turboDropDownName;
    private string[] _customECUDropDownName;
    private string[] _tireDropDownName;
    private string[] _customWightDropDownName;
    AudioSource _audioSource;
    [SerializeField] AudioClip _canChengeSound;
    [SerializeField] AudioClip _cantChengeSound;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        AddUIMesod();
    }

    private void OnEnable()
    {
        _playerCar = GameObject.FindGameObjectWithTag("PlayerCar");
        _carEngenAndGearBox = _playerCar.GetComponent<CarEngenAndGearBox>();
        _wheelsController = _playerCar.GetComponent<WheelsController>();
        _carSpecAttach = _playerCar.GetComponent<CarSpecAttachAndCostomINF>();
        _rb = _playerCar.GetComponent<Rigidbody>();
        StartDropDown();
        _partsSpecText.text = null;
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
        _tireDropDown.ClearOptions();
        _customWightDropDown.ClearOptions();
    }

    private void AttachDropDownName()
    {
        _turboDropDownName = _carSpecAttach.carTuneSpec.turboPartsDatas.Select(data => data != null ? data.partName : "None").ToArray();
        _customTurboDropDown.AddOptions(new List<string>(_turboDropDownName));
        _customECUDropDownName = _carSpecAttach.carTuneSpec.customECUDatas.Select(data => data != null ? data.partName : "None").ToArray();
        _customECUDropDown.AddOptions(new List<string>(_customECUDropDownName));
        _tireDropDownName = _carSpecAttach.carTuneSpec.tirePartsDatas.Select(data => data != null ? data.partName : "None").ToArray();
        _tireDropDown.AddOptions(new List<string>(_tireDropDownName));
        _customWightDropDownName = _carSpecAttach.carTuneSpec.customWeightDatas.Select(data => data != null ? data.partName : "None").ToArray();
        _customWightDropDown.AddOptions(new List<string>(_customWightDropDownName));

    }

    private void ChengeDropDownToStart()
    {
        _customTurboDropDown.value = _carSpecAttach.customSettingNam.nowTurboNam;
        if (_carSpecAttach.carTuneSpec.turboPartsDatas[_customTurboDropDown.value].canUse) _customTurboBuyText.text = "購入済み";
        else _customTurboBuyText.text = _carSpecAttach.carTuneSpec.turboPartsDatas[_customTurboDropDown.value].price.ToString();
        _customECUDropDown.value = _carSpecAttach.customSettingNam.nowCustomEcuNam;
        if (_carSpecAttach.carTuneSpec.customECUDatas[_customECUDropDown.value].canUse) _customECUBuyText.text = "購入済み";
        else _customECUBuyText.text = _carSpecAttach.carTuneSpec.customECUDatas[_customECUDropDown.value].price.ToString();
        _tireDropDown.value = _carSpecAttach.customSettingNam.nowTireNam;
        if (_carSpecAttach.carTuneSpec.tirePartsDatas[_tireDropDown.value].canUse) _tireBuyText.text = "購入済み";
        else _tireBuyText.text = _carSpecAttach.carTuneSpec.tirePartsDatas[_tireDropDown.value].price.ToString();
        _customWightDropDown.value = _carSpecAttach.customSettingNam.nowWeightNam;
        if (_carSpecAttach.carTuneSpec.customWeightDatas[_customWightDropDown.value].canUse) _customWightBuyText.text = "購入済み";
        else _customWightBuyText.text = _carSpecAttach.carTuneSpec.customWeightDatas[_customWightDropDown.value].price.ToString();
    }

    private void AddUIMesod()
    {
        _customTurboDropDown.onValueChanged.AddListener(ChengeTurbo);
        _customECUDropDown.onValueChanged.AddListener(ChengeECU);
        _tireDropDown.onValueChanged.AddListener(ChengeTire);
        _customWightDropDown.onValueChanged.AddListener(ChengeWight);
    }

    //以降各変更時のプログラム

    private void ChengeTurbo(int index)
    {
        if (_carSpecAttach.carTuneSpec.turboPartsDatas[index].canUse)
        {
            _customTurboBuyText.text = "購入済み";
            _carSpecAttach.customSettingNam.nowTurboNam = index;
            AttachTurbo(index);
            _audioSource.PlayOneShot(_canChengeSound);
        }
        else
        {
            _customTurboBuyText.text = _carSpecAttach.carTuneSpec.turboPartsDatas[index].price.ToString();
            _audioSource.PlayOneShot(_cantChengeSound);
        }
        _partsSpecText.text = _carSpecAttach.carTuneSpec.turboPartsDatas[index].explanatoryText;
        
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
            _audioSource.PlayOneShot(_canChengeSound);
        }
        else
        {
            _customECUBuyText.text = _carSpecAttach.carTuneSpec.customECUDatas[index].price.ToString();
            _audioSource.PlayOneShot(_cantChengeSound);
        }
        _partsSpecText.text = _carSpecAttach.carTuneSpec.customECUDatas[index].explanatoryText;
    }
    private void AttachECU(int index)
    {
        _wheelsController.moreTorque = _wheelsController.moreTorque + _carSpecAttach.carTuneSpec.customECUDatas[index].addTorquePow;
    }
    private void ChengeTire(int index)
    {
        if (_carSpecAttach.carTuneSpec.tirePartsDatas[index].canUse)
        {
            _tireBuyText.text = "購入済み";
            _carSpecAttach.customSettingNam.nowTireNam = index;
            AttachTire(index);
            _audioSource.PlayOneShot(_canChengeSound);
        }
        else
        {
            _tireBuyText.text = _carSpecAttach.carTuneSpec.tirePartsDatas[index].price.ToString();
            _audioSource.PlayOneShot(_cantChengeSound);
        }
        _partsSpecText.text = _carSpecAttach.carTuneSpec.tirePartsDatas[index].explanatoryText;
    }
    private void AttachTire(int index)
    {
        foreach(var wheel in _wheelsController.wheels)
        {
            wheel.collider.forwardFriction = _carSpecAttach.carTuneSpec.tirePartsDatas[index].forwardFriction.ToWheelFrictionCurve();
            wheel.collider.sidewaysFriction = _carSpecAttach.carTuneSpec.tirePartsDatas[index].sidewayFriction.ToWheelFrictionCurve();
        }
    }
    private void ChengeWight(int index)
    {
        if (_carSpecAttach.carTuneSpec.customWeightDatas[index].canUse)
        {
            _customWightBuyText.text = "購入済み";
            _carSpecAttach.customSettingNam.nowWeightNam = index;
            AttachCustomWight(index);
            _audioSource.PlayOneShot(_canChengeSound);
        }
        else 
        {
            _customWightBuyText.text = _carSpecAttach.carTuneSpec.customWeightDatas[index].price.ToString();
            _audioSource.PlayOneShot(_cantChengeSound);
        }
        _partsSpecText.text = _carSpecAttach.carTuneSpec.customWeightDatas[index].explanatoryText;
    }
    private void AttachCustomWight(int index)
    {
        _rb.mass = _carSpecAttach._carSpec.mass - _carSpecAttach.carTuneSpec.customWeightDatas[index].cutWeight;
    }
}
