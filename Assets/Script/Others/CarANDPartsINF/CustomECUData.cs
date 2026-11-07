using UnityEngine;

[CreateAssetMenu(fileName = "CostomECU", menuName = "CarSetting/Tuning/Parts/CostomECU")]
public class CustomECUData : ScriptableObject
{
    CarPartsName _partsName = CarPartsName.ECU;
    public CarPartsName GetPartsName => _partsName;
    public string partName;
    public int price;
    public bool canUse;
    public string explanatoryText;
    public float addTorquePowRate;
}
