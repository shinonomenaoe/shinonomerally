using UnityEngine;

public class SetCOntllerMode : MonoBehaviour
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
