using UnityEngine;
using UnityEngine.UI;

public class Player : MonoSingleton<Player>
{
    [SerializeField] PlayerScriptableObject playerData;
    [SerializeField] Text NameText;
    [SerializeField] Text CoinsText;
    [SerializeField] Text GemsText;
    string Name;
    int Coins;
    int Gems;
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
        NameText.text =Name;
        CoinsText.text =Coins.ToString();
        GemsText.text =Gems.ToString();
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
