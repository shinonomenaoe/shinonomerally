using UnityEngine;

[CreateAssetMenu(fileName = "CostomWeight", menuName = "CarSetting/Tuning/Parts/CostomWeight")]
public class CustomWeightData : ScriptableObject
{
    CarPartsName _partsName = CarPartsName.Weight;
    public CarPartsName GetPartsName => _partsName;
    public string partName;
    public int price;
    public bool canUse;
    public string explanatoryText;
    public float cutWeight;
}
