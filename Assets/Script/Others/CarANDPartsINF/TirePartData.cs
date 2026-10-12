using UnityEngine;

[CreateAssetMenu(fileName = "TirePart", menuName = "CarSetting/Tuning/Parts/TirePart")]
public class TirePartData : ScriptableObject
{
    public string partName;
    public int price;
    public WheelFrictionCurve forwardWardFriction;
    public WheelFrictionCurve sideWardFriction;
}
