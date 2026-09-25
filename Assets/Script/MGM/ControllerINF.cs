using System;
using UnityEngine;
public class ControllerINF : MonoBehaviour
{
    public static ControllerMode controllerMode = ControllerMode.KeyBord;
    public static float varticalInput;
    public static float horizontalInput;
    public static bool isBrake = false;
    public enum ControllerMode
    {
        KeyBord,
        Button,
        Controller,
    }
}
