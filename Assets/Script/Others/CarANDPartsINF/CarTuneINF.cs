using System;
using System.Linq;
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
    public CustomECUData[] customECUDatas;
    public TirePartData[] tirePartsDatas;
    public CustomWeightData[] customWeightDatas;
    public CarParts[] customParts => (turboPartsDatas ?? Array.Empty<TurboPartsData>())
        .Cast<CarParts>()
        .Concat(customECUDatas ?? Array.Empty<CustomECUData>())
        .Concat(tirePartsDatas ?? Array.Empty<TirePartData>())
        .Concat(customWeightDatas ?? Array.Empty<CustomWeightData>())
        .ToArray();
}

public enum CarPartsName
{
    Turbo,
    ECU,
    Tire,
    Weight,
}