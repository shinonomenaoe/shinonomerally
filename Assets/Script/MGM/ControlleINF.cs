using System;
using UnityEngine;

[Serializable]
public class ControlleINF : MonoBehaviour
{
    [Range(0, 1)] public static float varticalInput;
    [Range(-1, 1)] public static float horizontalInput;
    public static bool isBrake = false;
}
