using UnityEngine;

public class RaceINF : MonoBehaviour
{
    [SerializeField] public RaceType _raceType;

    public enum RaceType
    {
        Race,
        TimeAttack,
        Story,
    }
}
