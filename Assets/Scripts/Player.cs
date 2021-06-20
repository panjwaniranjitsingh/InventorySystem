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
        transform.GetChild(0).gameObject.GetComponent<Text>().text ="Name:"+ Name;
        transform.GetChild(1).gameObject.GetComponent<Text>().text ="Coins:"+Coins.ToString();
        transform.GetChild(2).gameObject.GetComponent<Text>().text ="Gems:"+ Gems.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToPlayer(int coinsToAdd,int GemsToAdd)
    {
        Coins += coinsToAdd;
        Gems += GemsToAdd;
        ShowPlayerData();
    }

    public void RemoveFromPlayer(int GemsToSub)
    {
        Gems -= GemsToSub;
        ShowPlayerData();
    }
}
