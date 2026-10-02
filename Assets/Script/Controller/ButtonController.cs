using UnityEngine;
using UnityEngine.EventSystems;
using static ControllerINF;

public class ButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    GameObject _mainGameManager;
    GameObject _playerCar;
    CarEngenAndGearBox _carEngenBox;
    SideBrake _sideBrake;
    [SerializeField] Button _buttonMode;
    [SerializeField] float _originalSteerChengePow = 3.0f;
    [SerializeField] float _originalVaticalInputPow = 3.0f;
    static float steerChengeSpeed;
    static float vaticalInputChengeSpeed;
    [Range(-1, 1)] static float nowSteerPow;
    [Range(0, 1)] static float nowAccelePow;
    static bool nowRightSteer = false;
    static bool nowLeftSteer = false;
    static bool nowAccele = false;
    void Start()
    {
        _playerCar = GameObject.FindWithTag("PlayerCar");
        _carEngenBox = _playerCar.GetComponent<CarEngenAndGearBox>();
        _sideBrake = _playerCar.GetComponent<SideBrake>();
        steerChengeSpeed = _originalSteerChengePow;
        vaticalInputChengeSpeed = _originalVaticalInputPow;
    }
    private void Update()
    {
        if(controllerMode != ControllerINF.ControllerMode.Button)return;
        InputAccele();
        InputSteer();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (ControllerINF.controllerMode == ControllerINF.ControllerMode.Button)
        {
            switch (_buttonMode)
            {
                case Button.Accelerator : nowAccele = true; break;
                case Button.Brake : ControllerINF.isBrake = true; break;
                case Button.RightSteer: nowRightSteer = true; break;
                case Button.LeftSteer: nowLeftSteer = true; break;
            }
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (ControllerINF.controllerMode == ControllerINF.ControllerMode.Button)
        {
            switch (_buttonMode)
            {
                case Button.Accelerator: nowAccele = false; break;
                case Button.Brake: ControllerINF.isBrake = false; break;
                case Button.RightSteer: nowRightSteer = false; break;
                case Button.LeftSteer: nowLeftSteer = false; break;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        switch (_buttonMode)
        {
            case Button.Accelerator: nowAccele = false; break;
            case Button.Brake: ControllerINF.isBrake = false; break;
            case Button.RightSteer: nowRightSteer = false; break;
            case Button.LeftSteer: nowLeftSteer = false; ; break;
        }
    }
    public void ChengeGear(bool isUp)
    {
        if(ControllerINF.controllerMode == ControllerINF.ControllerMode.Button)
        {
            if (isUp && _carEngenBox._mission == CarEngenAndGearBox.Mission.MT) _carEngenBox._nowGearNam++;
            if (!isUp && _carEngenBox._mission == CarEngenAndGearBox.Mission.MT) _carEngenBox._nowGearNam--;
            if (isUp && _carEngenBox._mission == CarEngenAndGearBox.Mission.AT) _carEngenBox._isRevers = true;
            if (!isUp && _carEngenBox._mission == CarEngenAndGearBox.Mission.AT) _carEngenBox._isRevers = false;
        }
    }
    public void HandBrake()
    {
        _sideBrake.OnBrales();
    }

    private void InputAccele()
    {
        if (nowAccele && _buttonMode == Button.Accelerator)
        {
            nowAccelePow = Mathf.MoveTowards(nowAccelePow, 1, vaticalInputChengeSpeed * Time.deltaTime);
            ControllerINF.varticalInput = nowAccelePow;
        }
        if (!nowAccele && _buttonMode == Button.Accelerator)
        {
            nowAccelePow = Mathf.MoveTowards(nowAccelePow, 0, vaticalInputChengeSpeed * Time.deltaTime);
            ControllerINF.varticalInput = 0;
        }
    }
    private void InputSteer()
    {
        if (nowRightSteer && _buttonMode == Button.RightSteer)
        {
            nowSteerPow = Mathf.MoveTowards(nowSteerPow, 1, steerChengeSpeed * Time.deltaTime);
            ControllerINF.horizontalInput = nowSteerPow;
        }
        if (nowLeftSteer && _buttonMode == Button.LeftSteer)
        {
            nowSteerPow = Mathf.MoveTowards(nowSteerPow, -1, steerChengeSpeed * Time.deltaTime);
            ControllerINF.horizontalInput = nowSteerPow;
        }
        if (!nowLeftSteer && !nowRightSteer && (_buttonMode == Button.LeftSteer || _buttonMode == Button.RightSteer))
        {
            nowSteerPow = Mathf.MoveTowards(nowSteerPow, 0, steerChengeSpeed * Time.deltaTime);
            ControllerINF.horizontalInput = 0;
        }
    }
    public enum Button
    {
        Accelerator,
        Brake,
        RightSteer,
        LeftSteer,
    }
    void OnEnable()
    {
        ControllerINF.varticalInput = 0;
        ControllerINF.horizontalInput = 0;
        ControllerINF.isBrake = false;
        _playerCar = GameObject.FindWithTag("PlayerCar");
        _carEngenBox = _playerCar.GetComponent<CarEngenAndGearBox>();
        _sideBrake = _playerCar.GetComponent<SideBrake>();
        steerChengeSpeed = _originalSteerChengePow;
        vaticalInputChengeSpeed = _originalVaticalInputPow;
    }
    void OnDestroy()
    {
        ControllerINF.varticalInput = 0;
        ControllerINF.horizontalInput = 0;
        ControllerINF.isBrake = false;
    }
}
