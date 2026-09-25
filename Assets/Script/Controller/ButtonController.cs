using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    GameObject _mainGameManager;
    GameObject _playerCar;
    CarEngenAndGearBox _carEngenBox;
    SideBrake _sideBrake;
    [SerializeField] Button _buttonMode;
    void Start()
    {
        _playerCar = GameObject.FindWithTag("PlayerCar");
        _carEngenBox = _playerCar.GetComponent<CarEngenAndGearBox>();
        _sideBrake = _playerCar.GetComponent<SideBrake>();
    }
    private void Update()
    {
        if(ControllerINF.controllerMode  == ControllerINF.ControllerMode.Button)return;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (ControllerINF.controllerMode == ControllerINF.ControllerMode.Button)
        {
            switch (_buttonMode)
            {
                case Button.Accelerator :ControllerINF.varticalInput = 1; break;
                case Button.Brake : ControllerINF.isBrake = true; break;
                case Button.RightSteer : ControllerINF.horizontalInput = 1; break;
                case Button.LeftSteer: ControllerINF.horizontalInput = -1; break;
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
                case Button.RightSteer: ControllerINF.horizontalInput = 0; break;
                case Button.LeftSteer: ControllerINF.horizontalInput = 0; break;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        switch (_buttonMode)
        {
            case Button.Accelerator: ControllerINF.varticalInput = 0; break;
            case Button.Brake: ControllerINF.isBrake = false; break;
            case Button.RightSteer: ControllerINF.horizontalInput = 0; break;
            case Button.LeftSteer: ControllerINF.horizontalInput = 0; break;
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
    }
    void OnDestroy()
    {
        ControllerINF.varticalInput = 0;
        ControllerINF.horizontalInput = 0;
        ControllerINF.isBrake = false;
    }
}
