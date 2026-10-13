using UnityEngine;

[CreateAssetMenu(fileName = "TurboPart", menuName = "CarSetting/Tuning/Parts/Turbo")]
public class TurboPartsData : ScriptableObject
{
    public string partName;
    public int price;
    public bool canUse;
    public AnimationCurve customTorqueCurve;
    public float moreTorqueBonusRate;
}
