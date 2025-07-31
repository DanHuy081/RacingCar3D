
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int coin = 0;
    private int star = 0;

    void Awake()
    {
        Instance = this;
    }

    public void AddCoin(int amount)
    {
        coin += amount;
        Debug.Log("Coins: " + coin);
    }

    public void AddStar(int amount)
    {
        star += amount;
        Debug.Log("Stars: " + star);
    }
}
