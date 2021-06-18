using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    [SerializeField] string Type;
    [SerializeField] int Coins;
    [SerializeField] int Gems;
    [SerializeField] float TimeToUnlock;
    [SerializeField] bool locked;
    public bool empty;
    // Start is called before the first frame update
    void Start()
    {
        empty = true;
        transform.GetChild(1).GetComponent<Text>().text = "Empty";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetChestData(ChestScriptableObject chestSO)
    {
        Debug.Log("SetChestData"+chestSO);
        locked = true;
        empty = false;
        Type =chestSO.Type;
        Coins = Random.Range(chestSO.minCoins,chestSO.maxCoins);
        Gems = Random.Range(chestSO.minGems, chestSO.maxGems);
        TimeToUnlock = chestSO.TimeToUnlockInSeconds;
        transform.GetChild(0).GetComponent<Text>().text = "Timer:" + TimeToUnlock.ToString();
        transform.GetChild(1).GetComponent<Text>().text = "Type:" + Type.ToString();
        transform.GetChild(2).GetComponent<Text>().text = "Gems:" + Gems.ToString();
        transform.GetChild(3).GetComponent<Text>().text = "Coins:" + Coins.ToString();
    }
}
