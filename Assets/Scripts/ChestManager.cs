
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateChest()
    {
        //Create Chest
        int randomChest = UnityEngine.Random.Range(0, chestSOL.Chests.Length);
        int chestFull = 0;
        for(int i=0;i<ChestSlots.Length;i++)
        {
            Chest chestScript = ChestSlots[i].GetComponent<Chest>();
            if (chestScript.empty)
            {
                chestScript.SetChestData(chestSOL.Chests[randomChest]);
                chestToUnlock.Add(chestScript);
                if (chestToUnlock.Count == 1)
                    chestScript.StartTimer();
                i = ChestSlots.Length + 1;
            }
            else
                chestFull++;
        }
        if(chestFull==ChestSlots.Length)
        {
            //All chests are filled
            Debug.Log("All chest slots are full");
            StartCoroutine(DisplayMessage("All chest slots are full"));
        }
    }

    public IEnumerator DisplayMessage(string message)
    {
        Message.GetComponent<Text>().text = message;
        Message.SetActive(true);
        yield return new WaitForSeconds(2f);
        Message.SetActive(false);
    }

    public void UnlockNextChest(Chest unlockedChest)
    {
        chestToUnlock.Remove(unlockedChest);
        chestToUnlock[0].StartTimer();
    }
}
