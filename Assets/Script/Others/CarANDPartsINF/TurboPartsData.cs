using UnityEngine;

[CreateAssetMenu(fileName = "TurboPart", menuName = "CarSetting/Tuning/Parts/Turbo")]
public class TurboPartsData : CarParts
{
    public CarPartsName carPartsName => base.carPartsName;
    public AnimationCurve customTorqueCurve;
    public float moreTorqueBonusRate;
}
