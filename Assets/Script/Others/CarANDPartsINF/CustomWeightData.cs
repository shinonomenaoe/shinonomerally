using UnityEngine;

[CreateAssetMenu(fileName = "CostomWeight", menuName = "CarSetting/Tuning/Parts/CostomWeight")]
public class CustomWeightData : CarParts
{
    public CarPartsName _partsName => carPartsName;
    public float cutWeight;
}
