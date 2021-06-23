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
    [SerializeField] int UnlockGems;
    [SerializeField] bool locked;
    public bool startTimer;
    [SerializeField] DateTime startTimeStamp;
    [SerializeField] GameObject BuyMessage;
    public bool empty;
    [SerializeField] Sprite emptySprite;
    public bool addedToQueue;
    // Start is called before the first frame update
    void Start()
    {
        empty = true;
        addedToQueue = false;
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
            addedToQueue = false;
            gameObject.GetComponent<Image>().sprite = emptySprite;
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
        UnlockGems = CountGemsToUnlock(timeLeft);
        transform.GetChild(5).GetComponent<Text>().text = "GemsToUnlock:" + UnlockGems.ToString();
        if (timer >= TimeToUnlock)
        {
            Debug.Log("Chest Unlocked at " + curtimer);
            ChestUnlocked();
        }
    }

    private void ChestUnlocked()
    {
        Debug.Log("Chest Unlocked");
        startTimer = false;
        locked = false;
        Status = "Unlocked";
        TimeToUnlock = 0;
        UnlockGems = 0;
        DisplayChestData();
        ChestManager.GetInstance().UnlockNextChest();
    }

    public void SetChestData(ChestScriptableObject chestSO,Sprite chestSprite)
    {
        //Debug.Log("SetChestData"+chestSO);
        locked = true;
        empty = false;
        Type =chestSO.Type;
        Coins = UnityEngine.Random.Range(chestSO.minCoins,chestSO.maxCoins);
        Gems = UnityEngine.Random.Range(chestSO.minGems, chestSO.maxGems);
        TimeToUnlock = chestSO.TimeToUnlockInSeconds;
        Status = "Locked";
        UnlockGems = CountGemsToUnlock(TimeToUnlock);
        DisplayChestData();
        gameObject.GetComponent<Image>().sprite = chestSprite;
    }

    private int CountGemsToUnlock(int timeToUnlock)
    {
        int noOfGems = 0;
        int unlockTimeInMin = timeToUnlock / 60;
        noOfGems = unlockTimeInMin / 10;
        return noOfGems+1;
    }

    public void DisplayChestData()
    {
        transform.GetChild(0).GetComponent<Text>().text = "Timer:" + TimeToUnlock.ToString();
        transform.GetChild(1).GetComponent<Text>().text = "Type:" + Type;
        transform.GetChild(2).GetComponent<Text>().text = "Gems:" + Gems.ToString();
        transform.GetChild(3).GetComponent<Text>().text = "Coins:" + Coins.ToString();
        transform.GetChild(4).GetComponent<Text>().text = "Status:" + Status;
        transform.GetChild(5).GetComponent<Text>().text = "GemsToUnlock:" + UnlockGems.ToString();
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
        ChestManager.GetInstance().timerStarted = true;
    }

    public void ChestClicked()
    {
        BuyMessage.SetActive(true);
        string message;
        if (empty)
        {
            message="Chest Slot is Empty";
            BuyMessage.GetComponent<PopUpManager>().OnlyDisplay(message);
            return;
        }
        if (!locked)
        {
            Player.GetInstance().AddToPlayer(Coins, Gems);
            empty = true;
            message = "Added " + Coins + " coins and " + Gems + " gems";
            ChestManager.GetInstance().RemoveChestFromSlot(this);
            addedToQueue = false;
            BuyMessage.GetComponent<PopUpManager>().OnlyDisplay(message);
        }
        else
        {
            BuyMessage.SetActive(true);
            message = "Chest is Locked.";
            BuyMessage.GetComponent<PopUpManager>().SetChest(this,message,UnlockGems);
        }
    }

    public void UnlockChestUsingGems()
    {
        bool chestCanBeUnlocked=Player.GetInstance().RemoveFromPlayer(UnlockGems);
        if (chestCanBeUnlocked)
            ChestUnlocked();
        else
        {
            BuyMessage.SetActive(true);
            BuyMessage.GetComponent<PopUpManager>().OnlyDisplay("Unsufficient Gems. Cannot Unlock Chest");
        }
    }
}
