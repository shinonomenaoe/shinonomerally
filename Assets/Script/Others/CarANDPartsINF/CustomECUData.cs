using UnityEngine;

[CreateAssetMenu(fileName = "CostomECU", menuName = "CarSetting/Tuning/Parts/CostomECU")]
public class CustomECUData : CarParts
{
    public CarPartsName _partsName => carPartsName;
    public float addTorquePowRate;
}
