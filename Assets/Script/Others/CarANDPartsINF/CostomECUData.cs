using UnityEngine;

[CreateAssetMenu(fileName = "CostomECU", menuName = "CarSetting/Tuning/Parts/CostomECU")]
public class CostomECUData : ScriptableObject
{
    public string partName;
    public int prace;
    public bool canUse;
    public float addTorquePow;
}
