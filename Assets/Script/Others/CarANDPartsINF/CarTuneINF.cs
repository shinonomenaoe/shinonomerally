using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CarTuneINF", menuName = "CarSetting/CarTuneINF")]
public class CarTuneINF : ScriptableObject
{
    public CarTune carTuneSpec;
}

[Serializable]
public struct CarTune
{
    public TurboPartsData[] turboPartsDatas;
    public TirePartData[] tirePartsDatas;
    public CostomWeightData[] costomWeightDatas;
    public CostomECUData[] costomECUDatas;
}