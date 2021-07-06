
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestSlotManager : MonoSingleton<ChestSlotManager>
{
    [SerializeField] ChestManager[] ChestSlots;
    [SerializeField] ChestScriptableObjectList chestSOL;
    [SerializeField] GameObject Message;
    public List<ChestManager> unlockingQueue;
    [SerializeField] int AllowedChestToUnlock = 3;
    [SerializeField] Sprite[] chestSprites;
    public bool timerStarted = false;

    public void CreateRandomChest()
    {
        int randomChest = Random.Range(0, chestSOL.Chests.Length);
        CreateChest(randomChest);
    }

    public void CreateChest(int randomChest)
    {
        //Create Chest
        int chestSlotAlreadyOccupied = 0;
        for (int i = 0; i < ChestSlots.Length; i++)
        {
            ChestManager chestScript = ChestSlots[i].GetComponent<ChestManager>();
            if (chestScript.empty)
            {
                chestScript.SetChestData(chestSOL.Chests[randomChest], chestSprites[randomChest]);
                i = ChestSlots.Length + 1;
            }
            else
                chestSlotAlreadyOccupied++;
        }
        if (chestSlotAlreadyOccupied == ChestSlots.Length)
        {
            //All chest slots are filled
            Debug.Log("All chest slots are occupied");
            StartCoroutine(DisplayMessage("All chest slots are occupied"));
        }
    }

    public void AddChestToUnlockingQueue(ChestManager chestScript)
    {
            if (timerStarted && unlockingQueue.Count == AllowedChestToUnlock)
            {
                Debug.Log("Unlocking Queue Limit Reached");
                StartCoroutine(DisplayMessage("Unlocking Queue Limit Reached"));
            }
            else
            {
                Debug.Log("Chest added to Unlocking Queue.");
                unlockingQueue.Add(chestScript);
                chestScript.addedToQueue = true;
            }
    }

    public IEnumerator DisplayMessage(string message)
    {
        Message.GetComponent<Text>().text = message;
        Message.SetActive(true);
        yield return new WaitForSeconds(2f);
        Message.SetActive(false);
    }

    public void UnlockNextChest()
    {
        if (unlockingQueue.Count > 1)
            unlockingQueue[1].StartTimer();
        else
            timerStarted = false;
    }

    public void RemoveChestFromSlot(ChestManager unlockedChest)
    {
        unlockingQueue.Remove(unlockedChest);
    }
}
