using UnityEngine;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    GameObject _mainGameManager;
    GameObject _playerCar;
    CarEngenAndGearBox _carEngenBox;
    SideBrake _sideBrake;
    ControlleINF _controlleINF;
    [SerializeField] public ControllerMode controllerMode = ControllerMode.Button;
    [SerializeField] Button _buttonMode;
    void Start()
    {
        _playerCar = GameObject.FindWithTag("PlayerCar");
        _carEngenBox = _playerCar.GetComponent<CarEngenAndGearBox>();
        _sideBrake = _playerCar.GetComponent<SideBrake>();
    }
    private void Update()
    {
        if(controllerMode  == ControllerMode.Button)return;
        if(controllerMode == ControllerMode.KeyBord)
        {
            if(Input.GetKey(KeyCode.W)) ControlleINF.varticalInput = 1;
            else ControlleINF.varticalInput = 0;
            if(Input.GetKey(KeyCode.A)) ControlleINF.horizontalInput = -1;
            else if(Input.GetKey(KeyCode.D)) ControlleINF.horizontalInput = 1;
            else ControlleINF.horizontalInput = 0;
            if(Input.GetKey(KeyCode.Space))ControlleINF.isBrake = true;
            else ControlleINF.isBrake = false;
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (controllerMode == ControllerMode.Button)
        {
            switch (_buttonMode)
            {
                case Button.Accelerator :ControlleINF.varticalInput = 1; break;
                case Button.Brake : ControlleINF.isBrake = true; break;
                case Button.RightSteer : ControlleINF.horizontalInput = 1; break;
                case Button.LeftSteer: ControlleINF.horizontalInput = -1; break;
            }
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (controllerMode == ControllerMode.Button)
        {
            switch (_buttonMode)
            {
                case Button.Accelerator: ControlleINF.varticalInput = 0; break;
                case Button.Brake: ControlleINF.isBrake = false; break;
                case Button.RightSteer: ControlleINF.horizontalInput = 0; break;
                case Button.LeftSteer: ControlleINF.horizontalInput = 0; break;
            }
        }
    }
    public void ChengeGear(bool isUp)
    {
        if(controllerMode == ControllerMode.Button)
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
    public enum ControllerMode
    {
        KeyBord,
        Button,
        Controller,
    }
    void OnEnable()
    {
        ControlleINF.varticalInput = 0;
        ControlleINF.horizontalInput = 0;
        ControlleINF.isBrake = false;
        _playerCar = GameObject.FindWithTag("PlayerCar");
        _carEngenBox = _playerCar.GetComponent<CarEngenAndGearBox>();
        _sideBrake = _playerCar.GetComponent<SideBrake>();
    }
    void OnDestroy()
    {
        ControlleINF.varticalInput = 0;
        ControlleINF.horizontalInput = 0;
        ControlleINF.isBrake = false;
    }
}
