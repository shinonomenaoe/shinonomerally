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
    static float _steerChengeSpeed;
    [Range(-1, 1)] static float _nowSteerPow;
    static bool _nowRightSteer = false;
    static bool _nowLeftSteer = false;
    void Start()
    {
        _playerCar = GameObject.FindWithTag("PlayerCar");
        _carEngenBox = _playerCar.GetComponent<CarEngenAndGearBox>();
        _sideBrake = _playerCar.GetComponent<SideBrake>();
        _steerChengeSpeed = _originalSteerChengePow;
    }
    private void Update()
    {
        if(controllerMode != ControllerINF.ControllerMode.Button)return;
        if (_nowRightSteer && _buttonMode == Button.RightSteer)
        {
            _nowSteerPow = Mathf.MoveTowards(_nowSteerPow, 1, _steerChengeSpeed * Time.deltaTime);
            ControllerINF.horizontalInput = _nowSteerPow;
        }
        if (_nowLeftSteer && _buttonMode == Button.LeftSteer)
        {
            _nowSteerPow = Mathf.MoveTowards(_nowSteerPow, -1, _steerChengeSpeed * Time.deltaTime);
            ControllerINF.horizontalInput = _nowSteerPow;
        }
        if(!_nowLeftSteer && !_nowRightSteer && (_buttonMode == Button.LeftSteer || _buttonMode == Button.RightSteer))
        {
            _nowSteerPow = Mathf.MoveTowards(_nowSteerPow, 0, _steerChengeSpeed * Time.deltaTime);
            ControllerINF.horizontalInput = _nowSteerPow;
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (ControllerINF.controllerMode == ControllerINF.ControllerMode.Button)
        {
            switch (_buttonMode)
            {
                case Button.Accelerator :ControllerINF.varticalInput = 1; break;
                case Button.Brake : ControllerINF.isBrake = true; break;
                case Button.RightSteer: _nowRightSteer = true; break;
                case Button.LeftSteer: _nowLeftSteer = true; break;
            }
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (ControllerINF.controllerMode == ControllerINF.ControllerMode.Button)
        {
            switch (_buttonMode)
            {
                case Button.Accelerator: ControllerINF.varticalInput = 0; break;
                case Button.Brake: ControllerINF.isBrake = false; break;
                case Button.RightSteer: _nowRightSteer = false; break;
                case Button.LeftSteer: _nowLeftSteer = false; break;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        switch (_buttonMode)
        {
            case Button.Accelerator: ControllerINF.varticalInput = 0; break;
            case Button.Brake: ControllerINF.isBrake = false; break;
            case Button.RightSteer: _nowRightSteer = false; break;
            case Button.LeftSteer: _nowLeftSteer = false; ; break;
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
        _steerChengeSpeed = _originalSteerChengePow;
    }
    void OnDestroy()
    {
        ControllerINF.varticalInput = 0;
        ControllerINF.horizontalInput = 0;
        ControllerINF.isBrake = false;
    }
}
