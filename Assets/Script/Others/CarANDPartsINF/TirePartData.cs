using UnityEngine;

[System.Serializable]
public class FrictionCurveData
{
    public float extremumSlip = 0.2f;
    public float extremumValue = 1.0f;
    public float asymptoteSlip = 0.5f;
    public float asymptoteValue = 0.75f;
    public float stiffness = 1.0f;

    public WheelFrictionCurve ToWheelFrictionCurve()
    {
        WheelFrictionCurve curve = new WheelFrictionCurve();
        curve.extremumSlip = extremumSlip;
        curve.extremumValue = extremumValue;
        curve.asymptoteSlip = asymptoteSlip;
        curve.asymptoteValue = asymptoteValue;
        curve.stiffness = stiffness;
        return curve;
    }
}
[CreateAssetMenu(fileName = "TirePart", menuName = "CarSetting/Tuning/Parts/TirePart")]
public class TirePartData : ScriptableObject
{
    private CarPartsName _partsName = CarPartsName.Tire;
    public CarPartsName getPartsName => _partsName;
    public string partName;
    public int price;
    public bool canUse;
    public string explanatoryText;
    public FrictionCurveData forwardFriction;
    public FrictionCurveData sidewayFriction;
}

