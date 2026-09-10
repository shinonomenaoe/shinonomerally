using UnityEngine;

public class CarEngenAndGearBox : MonoBehaviour
{
    [SerializeField] public float[] _gearPow = { 0, 3.724f, 2.246f, 1.541f, 1.205f, 1.000f, 0.818f };
    [SerializeField] public float[] _maxGearSpeed = { 0, 67, 111, 162, 207, 250, 305 };
    [SerializeField] public float _maxBaxkSpeed = 77;
    int _nowGearNam = 0;
    [SerializeField] public int _maxGear;
    float _nowGearMaxSpeed;
    private void Start()
    {
        _maxGear = _gearPow.Length - 1;
    }
    void Update()
    {
        NowMaxSpeed();
        ChengeGear();
    }

    void ChengeGear()
    {
        if (Input.GetKeyDown(KeyCode.Q) && _nowGearNam > -1) _nowGearNam--;
        if (Input.GetKeyDown(KeyCode.E) && _nowGearNam < _maxGear) _nowGearNam++;
    }

    void NowMaxSpeed()
    {
        if(_nowGearNam == -1) _nowGearMaxSpeed = _maxBaxkSpeed;
        else _nowGearMaxSpeed = _maxGearSpeed[_nowGearNam];
    }

}
