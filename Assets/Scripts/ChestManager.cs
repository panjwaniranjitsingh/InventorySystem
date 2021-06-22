
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestManager : MonoSingleton<ChestManager>
{
    [SerializeField] Chest[] ChestSlots;
    [SerializeField] ChestScriptableObjectList chestSOL;
    [SerializeField] GameObject Message;
    [SerializeField] List<Chest> chestToUnlock;
    [SerializeField] int AllowedChestToAdd = 3;
    [SerializeField] Sprite[] chestSprites;
    public bool timerStarted = false;

    public void CreateChest()
    {
        //Create Chest
        int randomChest = Random.Range(0, chestSOL.Chests.Length);
        for(int i=0;i<ChestSlots.Length;i++)
        {
            Chest chestScript = ChestSlots[i].GetComponent<Chest>();
            
            if (chestToUnlock.Count == 0)
            {
                AddChestToSlot(randomChest, chestScript);
                chestScript.canUnlock = true;
                i = ChestSlots.Length + 1;
            }
            else
            {
                Debug.Log("Chest to unlock is 1");
                if (timerStarted && chestToUnlock.Count == AllowedChestToAdd)
                {
                    Debug.Log("Allowed chest reached");
                    StartCoroutine(DisplayMessage("Allowed chest reached"));
                    return;
                }
                if (chestScript.empty)
                {
                    chestScript.SetChestData(chestSOL.Chests[randomChest],chestSprites[randomChest]);
                    chestToUnlock.Add(chestScript);
                    i = ChestSlots.Length + 1;
                }
            }
        }

        

        if (chestToUnlock.Count == ChestSlots.Length)
        {
            //All chests are filled
            Debug.Log("All chest slots are full");
            StartCoroutine(DisplayMessage("All chest slots are full"));
        }
    }

    private void AddChestToSlot(int randomChest,Chest chestScript)
    {
        if (chestScript.empty)
        {
            chestScript.SetChestData(chestSOL.Chests[randomChest],chestSprites[randomChest]);
            chestToUnlock.Add(chestScript);
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
        chestToUnlock[1].canUnlock=true;
        timerStarted = false;
    }

    public void RemoveChestFromSlot(Chest unlockedChest)
    {
        chestToUnlock.Remove(unlockedChest);
    }
}
