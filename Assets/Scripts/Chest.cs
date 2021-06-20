using System;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    [SerializeField] string Type;
    [SerializeField] int Coins;
    [SerializeField] int Gems;
    [SerializeField] int TimeToUnlock;
    [SerializeField] string Status;
    [SerializeField] bool locked;
    [SerializeField] bool startTimer;
    [SerializeField] DateTime startTimeStamp;
    public bool empty;
    // Start is called before the first frame update
    void Start()
    {
        empty = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(empty)
        {
            Type = "Empty";
            Coins = 0;
            Gems = 0;
            TimeToUnlock = 0;
            Status = "Empty";
            DisplayChestData();
        }
        if(startTimer)
        {
            StartUnlockingChest();
        }
    }

    private void StartUnlockingChest()
    {
        DateTime curtimer = DateTime.Now;
        int timer = GetSubSeconds(startTimeStamp, curtimer);
        //Debug.Log(timer);
        int timeLeft = TimeToUnlock - timer;
        transform.GetChild(0).GetComponent<Text>().text = "Timer:" + timeLeft;
        if (timer >= TimeToUnlock)
        {
            Debug.Log("Chest Unlocked");
            startTimer = false;
            locked = false;
            Status = "Unlocked";
            Debug.Log("Chest Unlocked at " + curtimer);
            TimeToUnlock = 0;
            DisplayChestData();
            ChestManager.GetInstance().UnlockNextChest(this);
        }
    }

    public void SetChestData(ChestScriptableObject chestSO)
    {
        //Debug.Log("SetChestData"+chestSO);
        locked = true;
        empty = false;
        Type =chestSO.Type;
        Coins = UnityEngine.Random.Range(chestSO.minCoins,chestSO.maxCoins);
        Gems = UnityEngine.Random.Range(chestSO.minGems, chestSO.maxGems);
        TimeToUnlock = chestSO.TimeToUnlockInSeconds;
        Status = "Locked";
        DisplayChestData();
    }

    public void DisplayChestData()
    {
        transform.GetChild(0).GetComponent<Text>().text = "Timer:" + TimeToUnlock.ToString();
        transform.GetChild(1).GetComponent<Text>().text = "Type:" + Type;
        transform.GetChild(2).GetComponent<Text>().text = "Gems:" + Gems.ToString();
        transform.GetChild(3).GetComponent<Text>().text = "Coins:" + Coins.ToString();
        transform.GetChild(4).GetComponent<Text>().text = "Status:" + Status;
    }
    public int GetSubSeconds(DateTime startTimer, DateTime endTimer)
    {
        TimeSpan startSpan = new TimeSpan(startTimer.Ticks);

        TimeSpan nowSpan = new TimeSpan(endTimer.Ticks);

        TimeSpan subTimer = nowSpan.Subtract(startSpan).Duration();

        //Return the time difference (the return value is the number of seconds of the difference)
        return subTimer.Seconds;
    }

    public void StartTimer()
    {
        startTimeStamp = DateTime.Now;
        Debug.Log("StartTimeStamp=" + startTimeStamp);
        startTimer = true;
    }

    public void ChestClicked()
    {
        if (empty)
            StartCoroutine(ChestManager.GetInstance().DisplayMessage("Chest is Empty"));

        if (!locked)
        {
            Player.GetInstance().AddToPlayer(Coins, Gems);
            empty = true;
            
        }
        else
        { 
            StartCoroutine(ChestManager.GetInstance().DisplayMessage("Chest is Locked"));
        }
    }
}
