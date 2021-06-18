using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestManager : MonoBehaviour
{
    [SerializeField] GameObject[] ChestSlots;
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
        ChestSlots[0].transform.GetChild(0).GetComponent<Text>().text = chestSOL.Chests[randomChest].TimeToUnlockInSeconds.ToString();
        ChestSlots[0].transform.GetChild(1).GetComponent<Text>().text = chestSOL.Chests[randomChest].Type.ToString();
    }
}
