using UnityEngine;

public class SetControllerMode : MonoBehaviour
{
    [SerializeField] public ControllerINF.ControllerMode setControllerMode;
    private void Awake()
    {
        ControllerINF.controllerMode = setControllerMode;
    }

    public void ChengeControlleMode(ControllerINF.ControllerMode controller)
    {
        ControllerINF.controllerMode = controller;
    }
}
