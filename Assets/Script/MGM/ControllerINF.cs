using System;
using UnityEngine;
public class ControllerINF : MonoBehaviour
{
    public static ControllerMode controllerMode = ControllerMode.KeyBord;
    public static float varticalInput;
    public static float horizontalInput;
    public static bool isBrake = false;
    public static bool upGear = false;
    public static bool downGear = false;
    public static bool isSideBrake = false;
    public static bool isClutch = false;
    public enum ControllerMode
    {
        KeyBord,
        Button,
        Controller,
    }
}
