using UnityEngine;

public class PartsShopSys : MonoBehaviour
{
    GameObject _mGM;
    PlayINF _playINF;
    private void Awake()
    {
        _mGM = GameObject.FindGameObjectWithTag("MainGameManager");
        _playINF = _mGM.GetComponent<PlayINF>();
    }
    private void OnEnable()
    {
        _mGM = GameObject.FindGameObjectWithTag("MainGameManager");
        _playINF = _mGM.GetComponent<PlayINF>();
    }
    public void BuyParts(TurboPartsData part)
    {
        if (part.canUse == false)
        {
            if (_playINF.money > part.price)
            {
                _playINF.money -= part.price;
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
            if (_playINF.money > part.price)
            {
                _playINF.money -= part.price;
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
            if (_playINF.money > part.price)
            {
                _playINF.money -= part.price;
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
            if (_playINF.money > part.price)
            {
                _playINF.money -= part.price;
                part.canUse = true;
            }
            else
            {

            }
        }
    }
}
