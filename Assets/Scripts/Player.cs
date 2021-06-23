using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoSingleton<Player>
{
    [SerializeField] PlayerScriptableObject playerData;
    [SerializeField] string Name;
    [SerializeField] int Coins;
    [SerializeField] int Gems;
    // Start is called before the first frame update
    void Start()
    {
        SetPlayerData(playerData);
        ShowPlayerData();
    }

    private void SetPlayerData(PlayerScriptableObject playerData)
    {
        Name = playerData.Name;
        gameObject.name = Name;
        Coins = playerData.Coins;
        Gems = playerData.Gems;
    }

    private void ShowPlayerData()
    {
        transform.GetChild(0).gameObject.GetComponent<Text>().text =Name;
        transform.GetChild(1).gameObject.GetComponent<Text>().text =Coins.ToString();
        transform.GetChild(2).gameObject.GetComponent<Text>().text =Gems.ToString();
    }

    public void AddToPlayer(int coinsToAdd,int GemsToAdd)
    {
        Coins += coinsToAdd;
        Gems += GemsToAdd;
        ShowPlayerData();
    }

    public bool RemoveFromPlayer(int GemsToSub)
    {
        bool sufficientGems = true;
        if (Gems >= GemsToSub)
            Gems -= GemsToSub;
        else
            sufficientGems = false;
        ShowPlayerData();
        return sufficientGems;
    }
}
