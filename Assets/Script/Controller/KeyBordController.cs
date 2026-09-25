using UnityEngine;

public class KeyBordController : MonoBehaviour
{
    void Update()
    {
        AccelAndSteer();
    }

    void AccelAndSteer()
    {
        if (ControllerINF.controllerMode == ControllerINF.ControllerMode.KeyBord)
        {
            if (Input.GetKey(KeyCode.W)) ControllerINF.varticalInput = 1;
            else ControllerINF.varticalInput = 0;
            if (Input.GetKey(KeyCode.A)) ControllerINF.horizontalInput = -1;
            else if (Input.GetKey(KeyCode.D)) ControllerINF.horizontalInput = 1;
            else ControllerINF.horizontalInput = 0;
            if (Input.GetKey(KeyCode.Space)) ControllerINF.isBrake = true;
            else ControllerINF.isBrake = false;
        }
    }
}
