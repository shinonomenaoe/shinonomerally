using UnityEngine;

[CreateAssetMenu(fileName = "CarParts", menuName = "Scriptable Objects/CarParts")]
public class CarParts : ScriptableObject
{
    public int price;
    public bool canUse;
    public string partName;
    public string explanatoryText;
    [SerializeField] private CarPartsName _carPartsName;
    [HideInInspector]public CarPartsName carPartsName => _carPartsName;
}
