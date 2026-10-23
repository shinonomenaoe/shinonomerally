using UnityEngine;
using UnityEngine.UI;
public class MoneySystem : MonoBehaviour
{
    public int money = 20000;

    public void UseMoney(int price)
    {
        money -= price;
    }
    public void GetMoney(int price)
    {
        money += price;
    }

    public void UpdateMoney(Text moneyText)
    {
        moneyText.text = money.ToString();
    }
}
