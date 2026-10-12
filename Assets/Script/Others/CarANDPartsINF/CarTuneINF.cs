using UnityEngine;

[CreateAssetMenu(fileName = "CarTuneINF", menuName = "CarSetting/CarTuneINF")]
public class CarTuneINF : ScriptableObject
{
    public TurboPartsData[] turboPartsDatas;
    public TirePartData[] tirePartsDatas;
    public CostomWeightData[] costomWeightDatas; 
}