using UnityEngine;

public class PartsShopSys : MonoBehaviour
{
    GameObject _mGM;
    MoneySystem _moneySystem;
    private void Awake()
    {
        _mGM = GameObject.FindGameObjectWithTag("MainGameManager");
        _moneySystem = _mGM.GetComponent<MoneySystem>();
    }
    private void OnEnable()
    {
        _mGM = GameObject.FindGameObjectWithTag("MainGameManager");
        _moneySystem = _mGM.GetComponent<MoneySystem>();
    }
    public void BuyParts(TurboPartsData part)
    {
        if (part.canUse == false)
        {
            if (_moneySystem.money > part.price)
            {
                _moneySystem.money -= part.price;
                part.canUse = true;
            }
            else
            {

            }
        }
    }
    public void BuyParts(CustomECUData part)
    {
        if (part.canUse == false)
        {
            if (_moneySystem.money > part.price)
            {
                _moneySystem.money -= part.price;
                part.canUse = true;
            }
            else
            {

            }
        }
    }
    public void BuyParts(TirePartData part)
    {
        if (part.canUse == false)
        {
            if (_moneySystem.money > part.price)
            {
                _moneySystem.money -= part.price;
                part.canUse = true;
            }
            else
            {

            }
        }
    }
    public void BuyPurts(CustomWeightData part)
    {
        if (part.canUse == false)
        {
            if (_moneySystem.money > part.price)
            {
                _moneySystem.money -= part.price;
                part.canUse = true;
            }
            else
            {

            }
        }
    }
}
