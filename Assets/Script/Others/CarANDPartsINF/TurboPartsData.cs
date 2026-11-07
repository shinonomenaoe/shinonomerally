using UnityEngine;

[CreateAssetMenu(fileName = "TurboPart", menuName = "CarSetting/Tuning/Parts/Turbo")]
public class TurboPartsData : ScriptableObject
{
    private CarPartsName _partsName = CarPartsName.Turbo;
    public CarPartsName GetPartsName => _partsName;
    public string partName;
    public int price;
    public bool canUse;
    public string explanatoryText;
    public AnimationCurve customTorqueCurve;
    public float moreTorqueBonusRate;
}
