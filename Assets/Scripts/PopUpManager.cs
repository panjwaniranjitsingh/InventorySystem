using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PopUpManager : MonoBehaviour
{
    [SerializeField] Chest chestFrom;
    [SerializeField] Text firstButton;
    [SerializeField] Text secondButton;
    Coroutine disableCoRoutine;
    public void SetChest(Chest chest, string message, int gems)
    {
        if(disableCoRoutine!=null)
            StopCoroutine(disableCoRoutine);
        chestFrom = chest;
        transform.GetChild(0).GetComponent<Text>().text = message;
        if(!ChestManager.GetInstance().timerStarted)
            firstButton.text = "Start CountDown";
        else
            firstButton.text = "Add Chest to Unlocking Queue";

        secondButton.text = "Unlock using Gems:" + gems.ToString();      
        transform.GetChild(0).GetComponent<Text>().text = message;
        if(chestFrom.addedToQueue)
            transform.GetChild(1).gameObject.SetActive(false);
        else
            transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(2).gameObject.SetActive(true);
    }

    public void YesButton()
    {
        gameObject.SetActive(false);
        if (!ChestManager.GetInstance().timerStarted)
        {
            Debug.Log("First Chest to be added to Unlocking Queue.");
            ChestManager.GetInstance().chestToUnlock.Add(chestFrom);
            chestFrom.addedToQueue = true;
            chestFrom.StartTimer();
        }
        else
            ChestManager.GetInstance().AddChestToUnlockingQueue(chestFrom);
    }

    public void NoButton()
    {
        gameObject.SetActive(false);
        chestFrom.UnlockChestUsingGems();
    }

    public void OnlyDisplay(string message)
    {
        transform.GetChild(0).GetComponent<Text>().text = message;
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
        disableCoRoutine = StartCoroutine(DisablePopUp());
    }

    IEnumerator DisablePopUp()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
