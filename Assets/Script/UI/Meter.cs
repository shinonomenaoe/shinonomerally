using UnityEngine;

public class Meter : MonoBehaviour
{
    [SerializeField] private RectTransform _RPMmeterTransfrom;
    [SerializeField] private RectTransform _HourmeterTransfrom;
    [SerializeField] private float _RPMMeterFirstAngle;
    [SerializeField] public float _RPMMeterEndAngle;
    [SerializeField] private float _HourMeterFirstAngle;
    [SerializeField] public float _HourMeterEndAngle;
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
         RPMMeter();
         HourMeter();
    }

    void RPMMeter()
    {
        float nowRPM = _carEngen._nowRPM;
        float rPMRate = Mathf.InverseLerp(_carEngen._idelRPM, _carEngen._maxRPM, nowRPM);
        if (rPMRate > 0.95) rPMRate += Random.Range(-0.1f, 0.1f);
        float nowAngle = Mathf.Lerp(_RPMMeterFirstAngle, _RPMMeterEndAngle, rPMRate);
        _RPMmeterTransfrom.localEulerAngles = new Vector3(0, 0, nowAngle);
    }

    void HourMeter()
    {
        float speedRate = Mathf.InverseLerp(0f, _meterMaxSpeed, _carEngen._nowSpeed);
        float nowAngle = Mathf.Lerp(_HourMeterFirstAngle, _HourMeterEndAngle, speedRate);
        _HourmeterTransfrom.localEulerAngles = new Vector3(0, 0, nowAngle);
    }
}
