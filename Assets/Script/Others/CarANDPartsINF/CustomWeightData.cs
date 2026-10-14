using UnityEngine;

[CreateAssetMenu(fileName = "CostomWeight", menuName = "CarSetting/Tuning/Parts/CostomWeight")]
public class CustomWeightData : ScriptableObject
{
    public string partName;
    public int price;
    public bool canUse;
    public string explanatoryText;
    public float cutWeight;
}
