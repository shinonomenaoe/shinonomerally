using UnityEngine;
using UnityEngine.UI;
public class MoneyUi : MonoBehaviour
{
    Text moneyText;
    GameObject _mGM;
    MoneySystem _moneySystem;
    private void Awake()
    {
        moneyText = GetComponent<Text>();
        _mGM = GameObject.FindGameObjectWithTag("MainGameManager");
        _moneySystem = _mGM.GetComponent<MoneySystem>();
        moneyText.text = _moneySystem.money.ToString();
    }
}
