using UnityEngine;

public class PartsShop : MonoBehaviour
{
    GameObject _playerCar;
    CarSpecAttachAndCostomINF _carSpecAttachAndCostomINF;
    private void OnEnable()
    {
        _playerCar = GameObject.FindWithTag("PlayerCar");
        _carSpecAttachAndCostomINF = _playerCar.GetComponent<CarSpecAttachAndCostomINF>();
    }
}
