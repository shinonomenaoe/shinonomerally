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
            ControllerINF.isBrake = _carController.InGameKeyMap.Brake.IsPressed();
            ControllerINF.varticalInput = _carController.InGameKeyMap.Throttle.ReadValue<float>() - _carController.InGameKeyMap.Brake.ReadValue<float>();
            ControllerINF.horizontalInput = _carController.InGameKeyMap.Steer.ReadValue<float>();
            ControllerINF.isSideBrake = _carController.InGameKeyMap.SideBrake.IsPressed();
            ControllerINF.upGear = _carController.InGameKeyMap.UpGear.WasPressedThisFrame();
            ControllerINF.downGear = _carController.InGameKeyMap.DownGear.WasPressedThisFrame();
            ControllerINF.isClutch = _carController.InGameKeyMap.Clutch.IsPressed();
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
