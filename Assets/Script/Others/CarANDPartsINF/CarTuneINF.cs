using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName = "CarTuneINF", menuName = "CarSetting/CarTuneINF")]
public class CarTuneINF : ScriptableObject
{
    [HideInInspector] public SaveDataTemp carTuneSpec = new();
    public TurboPartsData[] turboPartsDatas;
    public CustomECUData[] customECUDatas;
    public TirePartData[] tirePartDatas;
    public CustomWeightData[] customWeightDatas;

    private void Awake()
    {
        SetSaveDataTemp();
    }
    private void OnEnable()
    {
        SetSaveDataTemp();
    }

    private void OnDisable()
    {
        SetSaveDataTemp();
    }

    private void SetSaveDataTemp()
    {
        carTuneSpec = new SaveDataTemp();
        carTuneSpec.Add(turboPartsDatas);
        carTuneSpec.Add(customECUDatas);
        carTuneSpec.Add(tirePartDatas);
        carTuneSpec.Add(customWeightDatas);
    }
}

[Serializable]
public class SaveDataTemp : ISerializationCallbackReceiver
{
    private Dictionary<Type, kvp> _inst = new();
    [SerializeField] private kvp[] _arrays = Array.Empty<kvp>();

    public CarParts[] this[Type t] => _inst.TryGetValue(t, out var val) ? val.array : Array.Empty<CarParts>();

    public void Add<T>(T[] array) where T : CarParts
    {
        if (_inst == null) { _inst = new(); }

        Type t = typeof(T);
        kvp p = new kvp(
            t.AssemblyQualifiedName,
            array ?? Array.Empty<CarParts>()
            );
        _inst[t] = p;
    }

    public CarParts[] GetAllCustomParts()
    {
        if (_inst == null || _inst.Count == 0) { return Array.Empty<CarParts>(); }

        IEnumerable<CarParts> p = Array.Empty<CarParts>();
        foreach (var a in _inst.Values)
        {
            if (a.array != null)
            {
                p = p.Concat(a.array);
            }
        }
        return p.ToArray();
    }

    public T[] GetParts<T>() where T : CarParts
    {
        if (_inst != null && _inst.TryGetValue(typeof(T), out var val) && val.array != null)
        {
            return val.array.OfType<T>().ToArray();
        }
        return Array.Empty<T>();
    }

    public void OnBeforeSerialize()
    {
        _arrays = _inst != null ? _inst.Values.ToArray() : Array.Empty<kvp>();
    }

    public void OnAfterDeserialize()
    {
        _inst = new Dictionary<Type, kvp>();
        if (_arrays == null) { return; }

        foreach (var item in _arrays)
        {
            if (string.IsNullOrEmpty(item.type)) { continue; }

            Type t = Type.GetType(item.type);
            if (t != null)
            {
                _inst[t] = item;
            }
        }
    }

    [Serializable]
    public struct kvp
    {
        public kvp(string t, CarParts[] a) => (type, array) = (t, a);
        public string type;
        public CarParts[] array;
    }
}

public enum CarPartsName
{
    Turbo,
    ECU,
    Tire,
    Weight,
}

//[Serializable]
//public struct CarTune
//{
//    public void Set(CarParts[] parts)
//    {
//        SaveDataTemp a = new();
//        Type t = typeof(TurboPartsData);

//        CarParts[] p = a[t];

//        TurboPartsData[] tb = turboPartsDatas;
//        tb = a.GetParts<TurboPartsData>();
//        CarParts[] all = a.GetAllCustomParts();
//    }

//    public TurboPartsData[] turboPartsDatas;
//    public CustomECUData[] customECUDatas;
//    public TirePartData[] tirePartsDatas;
//    public CustomWeightData[] customWeightDatas;
//    public CarParts[] customParts => (turboPartsDatas ?? Array.Empty<TurboPartsData>())
//        .Cast<CarParts>()
//        .Concat(customECUDatas ?? Array.Empty<CustomECUData>())
//        .Concat(tirePartsDatas ?? Array.Empty<TirePartData>())
//        .Concat(customWeightDatas ?? Array.Empty<CustomWeightData>())
//        .ToArray();


//}