using UnityEngine;

[CreateAssetMenu(fileName = "CostomECU", menuName = "CarSetting/Tuning/Parts/CostomECU")]
public class CustomECUData : ScriptableObject
{
    public string partName;
    public int price;
    public bool canUse;
    public string explanatoryText;
    public float addTorquePow;
}
