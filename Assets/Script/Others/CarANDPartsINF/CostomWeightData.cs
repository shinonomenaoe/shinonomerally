using UnityEngine;

[CreateAssetMenu(fileName = "CostomWeight", menuName = "CarSetting/Tuning/Parts/CostomWeight")]
public class CostomWeightData : ScriptableObject
{
    public string partName;
    public int price;
    public float Cutweight;
}
