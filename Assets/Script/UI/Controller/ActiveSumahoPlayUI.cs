using UnityEngine;

public class ActiveSumahoPlayUI : MonoBehaviour
{
    void Update()
    {
        if(ControllerINF.controllerMode == ControllerINF.ControllerMode.Button)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
