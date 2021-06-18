using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestManager : MonoBehaviour
{
    [SerializeField] Chest[] ChestSlots;
    [SerializeField] ChestScriptableObjectList chestSOL;
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
        for(int i=0;i<ChestSlots.Length;i++)
        {
            Chest chestScript = ChestSlots[i].GetComponent<Chest>();
            if (chestScript.empty)
            {
                chestScript.SetChestData(chestSOL.Chests[randomChest]);
                i = ChestSlots.Length+1;
            }
        }
        
    }
}
