
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestManager : MonoSingleton<ChestManager>
{
    [SerializeField] Chest[] ChestSlots;
    [SerializeField] ChestScriptableObjectList chestSOL;
    [SerializeField] GameObject Message;
    public List<Chest> chestToUnlock;
    [SerializeField] int AllowedChestToUnlock = 3;
    [SerializeField] Sprite[] chestSprites;
    public bool timerStarted = false;

    public void CreateChest()
    {
        int chestSlotAlreadyOccupied=0;
        //Create Chest
        int randomChest = Random.Range(0, chestSOL.Chests.Length);
        for(int i=0;i<ChestSlots.Length;i++)
        {
            Chest chestScript = ChestSlots[i].GetComponent<Chest>();
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

    public void AddChestToUnlockingQueue(Chest chestScript)
    {
            if (timerStarted && chestToUnlock.Count == AllowedChestToUnlock)
            {
                Debug.Log("Unlocking Queue Limit Reached");
                StartCoroutine(DisplayMessage("Unlocking Queue Limit Reached"));
            }
            else
            {
                Debug.Log("Chest added to Unlocking Queue.");
                chestToUnlock.Add(chestScript);
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
        if (chestToUnlock.Count > 1)
            chestToUnlock[1].StartTimer();
        else
            timerStarted = false;
    }

    public void RemoveChestFromSlot(Chest unlockedChest)
    {
        chestToUnlock.Remove(unlockedChest);
    }
}
