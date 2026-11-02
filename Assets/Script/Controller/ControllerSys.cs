using UnityEngine;
using UnityEngine.InputSystem;
public class ControllerSys : MonoBehaviour
{
    private CarController _carController;
    private void Awake()
    {
        _carController = new CarController();   
    }
    
    void Update()
    {
        Control();
    }

    void Control()
    {
        if (ControllerINF.controllerMode != ControllerINF.ControllerMode.Button)
        {
            ControllerINF.varticalInput = _carController.InGameKeyMap.Throttle.ReadValue<float>();
            ControllerINF.horizontalInput = _carController.InGameKeyMap.Steer.ReadValue<float>();
            ControllerINF.isBrake = _carController.InGameKeyMap.Brake.ReadValue<bool>();
            ControllerINF.isSideBrake = _carController.InGameKeyMap.SideBrake.ReadValue<bool>();
            ControllerINF.upGear = _carController.InGameKeyMap.UpGear.ReadValue<bool>();
            ControllerINF.downGear = _carController.InGameKeyMap.DownGear.ReadValue<bool>();
            ControllerINF.isClutch = _carController.InGameKeyMap.Clutch.ReadValue<bool>();
        }
    }
    private void OnEnable()
    {
        _carController.InGameKeyMap.Enable();
    }
    private void OnDisable()
    {
        _carController.InGameKeyMap.Disable();
    }
}
