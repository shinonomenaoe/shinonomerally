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
        _turboDropDownName = _carSpecAttach.carTuneSpec.GetParts<TurboPartsData>().Select(data => data != null ? data.partName : "None").ToArray();
        _customTurboDropDown.AddOptions(new List<string>(_turboDropDownName));
        _customECUDropDownName = _carSpecAttach.carTuneSpec.GetParts<CustomECUData>().Select(data => data != null ? data.partName : "None").ToArray();
        _customECUDropDown.AddOptions(new List<string>(_customECUDropDownName));
        _tireDropDownName = _carSpecAttach.carTuneSpec.GetParts<TirePartData>().Select(data => data != null ? data.partName : "None").ToArray();
        _tireDropDown.AddOptions(new List<string>(_tireDropDownName));
        _customWightDropDownName = _carSpecAttach.carTuneSpec.GetParts<CustomWeightData>().Select(data => data != null ? data.partName : "None").ToArray();
        _customWightDropDown.AddOptions(new List<string>(_customWightDropDownName));

    }

    private void ChengeDropDownToStart()
    {
        _customTurboDropDown.value = _carSpecAttach.carSpecJson.customSettingNam.nowTurboNam;
        if (_carSpecAttach.carTuneSpec.GetParts<TurboPartsData>()[_customTurboDropDown.value].canUse) _customTurboBuyText.text = "購入済み";
        else _customTurboBuyText.text = _carSpecAttach.carTuneSpec.GetParts<TurboPartsData>()[_customTurboDropDown.value].price.ToString();
        _customECUDropDown.value = _carSpecAttach.carSpecJson.customSettingNam.nowCustomEcuNam;
        if (_carSpecAttach.carTuneSpec.GetParts<CustomECUData>()[_customECUDropDown.value].canUse) _customECUBuyText.text = "購入済み";
        else _customECUBuyText.text = _carSpecAttach.carTuneSpec.GetParts<CustomECUData>()[_customECUDropDown.value].price.ToString();
        _tireDropDown.value = _carSpecAttach.carSpecJson.customSettingNam.nowTireNam;
        if (_carSpecAttach.carTuneSpec.GetParts<TirePartData>()[_tireDropDown.value].canUse) _tireBuyText.text = "購入済み";
        else _tireBuyText.text = _carSpecAttach.carTuneSpec.GetParts<TirePartData>()[_tireDropDown.value].price.ToString();
        _customWightDropDown.value = _carSpecAttach.carSpecJson.customSettingNam.nowWeightNam;
        if (_carSpecAttach.carTuneSpec.GetParts<CustomWeightData>()[_customWightDropDown.value].canUse) _customWightBuyText.text = "購入済み";
        else _customWightBuyText.text = _carSpecAttach.carTuneSpec.GetParts<CustomWeightData>()[_customWightDropDown.value].price.ToString();
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
        if (_carSpecAttach.carTuneSpec.GetParts<TurboPartsData>()[index].canUse)
        {
            _customTurboBuyText.text = "購入済み";
            _carSpecAttach.customSettingNam.nowTurboNam = index;
            _carSpecAttach.carSpecJson.customSettingNam.nowTurboNam = index;
            AttachTurbo(index);
            _audioSource.PlayOneShot(_canChengeSound);
        }
        else
        {
            _customTurboBuyText.text = _carSpecAttach.carTuneSpec.GetParts<TurboPartsData>()[index].price.ToString();
            _audioSource.PlayOneShot(_cantChengeSound);
        }
        _partsSpecText.text = _carSpecAttach.carTuneSpec.GetParts<TurboPartsData>()[index].explanatoryText;
        
    }
    public void AttachTurbo(int index)
    {
        _wheelsController.torqueCurve = _carSpecAttach.carTuneSpec.GetParts<TurboPartsData>()[index].customTorqueCurve;
        _wheelsController.moreTorque = _carSpecAttach._carSpec.moreTorque * _carSpecAttach.carTuneSpec.GetParts<TurboPartsData>()[index].moreTorqueBonusRate * _carSpecAttach.carTuneSpec.GetParts<CustomECUData>()[_carSpecAttach.carSpecJson.customSettingNam.nowCustomEcuNam].addTorquePowRate;
    }
    private void ChengeECU(int index)
    {
        if (_carSpecAttach.carTuneSpec.GetParts<CustomECUData>()[index].canUse)
        {
            _customECUBuyText.text = "購入済み";
            _carSpecAttach.customSettingNam.nowCustomEcuNam = index;
            _carSpecAttach.carSpecJson.customSettingNam.nowCustomEcuNam = index;
            AttachECU(index);
            _audioSource.PlayOneShot(_canChengeSound);
        }
        else
        {
            _customECUBuyText.text = _carSpecAttach.carTuneSpec.GetParts<CustomECUData>()[index].price.ToString();
            _audioSource.PlayOneShot(_cantChengeSound);
        }
        _partsSpecText.text = _carSpecAttach.carTuneSpec.GetParts<CustomECUData>()[index].explanatoryText;
    }
    public void AttachECU(int index)
    {
        _wheelsController.moreTorque = _carSpecAttach._carSpec.moreTorque * _carSpecAttach.carTuneSpec.GetParts <TurboPartsData>()[_carSpecAttach.carSpecJson.customSettingNam.nowTurboNam].moreTorqueBonusRate * _carSpecAttach.carTuneSpec.GetParts<CustomECUData>()[index].addTorquePowRate;
    }
    private void ChengeTire(int index)
    {
        if (_carSpecAttach.carTuneSpec.GetParts<TirePartData>()[index].canUse)
        {
            _tireBuyText.text = "購入済み";
            _carSpecAttach.customSettingNam.nowTireNam = index;
            _carSpecAttach.carSpecJson.customSettingNam.nowTireNam = index;
            AttachTire(index);
            _audioSource.PlayOneShot(_canChengeSound);
        }
        else
        {
            _tireBuyText.text = _carSpecAttach.carTuneSpec.GetParts<TirePartData>()[index].price.ToString();
            _audioSource.PlayOneShot(_cantChengeSound);
        }
        _partsSpecText.text = _carSpecAttach.carTuneSpec.GetParts<TirePartData>()[index].explanatoryText;
    }
    public void AttachTire(int index)
    {
        foreach(var wheel in _wheelsController.wheels)
        {
            wheel.collider.forwardFriction = _carSpecAttach.carTuneSpec.GetParts<TirePartData>()[index].forwardFriction.ToWheelFrictionCurve();
            wheel.collider.sidewaysFriction = _carSpecAttach.carTuneSpec.GetParts<TirePartData>()[index].sidewayFriction.ToWheelFrictionCurve();
        }
    }
    private void ChengeWight(int index)
    {
        if (_carSpecAttach.carTuneSpec.GetParts<CustomWeightData>()[index].canUse)
        {
            _customWightBuyText.text = "購入済み";
            _carSpecAttach.customSettingNam.nowWeightNam = index;
            _carSpecAttach.carSpecJson.customSettingNam.nowWeightNam = index;
            AttachCustomWight(index);
            _audioSource.PlayOneShot(_canChengeSound);
        }
        else 
        {
            _customWightBuyText.text = _carSpecAttach.carTuneSpec.GetParts<CustomWeightData>()[index].price.ToString();
            _audioSource.PlayOneShot(_cantChengeSound);
        }
        _partsSpecText.text = _carSpecAttach.carTuneSpec.GetParts<CustomWeightData>()[index].explanatoryText;
    }
    public void AttachCustomWight(int index)
    {
        _rb.mass = _carSpecAttach._carSpec.mass - _carSpecAttach.carTuneSpec.GetParts<CustomWeightData>()[index].cutWeight;
    }

    public void ChengeJson()
    {
        _carSpecAttach.ChengeAndSaveJson();
        _carSpecAttach.LoadJson();
    }
}
