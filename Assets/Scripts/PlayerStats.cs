using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Health;
    public static int Money;

    public int StartHealh;
    public int StartMoney = 100;

    private void Start()
    {
        Health = StartHealh;
        Money = StartMoney;
    }
}
