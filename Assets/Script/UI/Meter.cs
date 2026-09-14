using UnityEngine;

public class Meter : MonoBehaviour
{
    [SerializeField] private RectTransform _meterTransfrom;
    [SerializeField] private MeterName _meterName;
    [SerializeField] private float _meterFirstAngle;
    [SerializeField] public float _meterEndAngle;
    [SerializeField] public float _meterMaxSpeed;
    GameObject _playercar;
    CarEngenAndGearBox _carEngen;
    

    void Start()
    {
        _playercar = GameObject.FindWithTag("PlayerCar");
        _carEngen = _playercar.GetComponent<CarEngenAndGearBox>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_meterName == MeterName.RPM) RPMMeter();
        if (_meterName == MeterName.Hour) HourMeter();
    }

    void RPMMeter()
    {
        float nowRPM = _carEngen._nowRPM;
        float rPMRate = Mathf.InverseLerp(_carEngen._idelRPM, _carEngen._maxRPM, nowRPM);
        if (rPMRate > 0.95) rPMRate += Random.Range(-0.1f, 0.1f);
        float nowAngle = Mathf.Lerp(_meterFirstAngle, _meterEndAngle, rPMRate);
        _meterTransfrom.localEulerAngles = new Vector3(0, 0, nowAngle);
    }

    void HourMeter()
    {
        float speedRate = Mathf.InverseLerp(0f, _meterMaxSpeed, _carEngen._nowSpeed);
        float nowAngle = Mathf.Lerp(_meterFirstAngle, _meterEndAngle, speedRate);
        _meterTransfrom.localEulerAngles = new Vector3(0, 0, nowAngle);
    }

    public enum MeterName
    {
        RPM,
        Hour,
    }
}
