using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PopUpManager : MonoBehaviour
{
    [SerializeField] ChestManager chestFrom;
    [SerializeField] Text Message;
    [SerializeField] Button firstButton;
    [SerializeField] Button secondButton;
    [SerializeField] Text firstButtonText;
    [SerializeField] Text secondButtonText;
    Coroutine PopUpCoroutine;
    public void DisplayPopUp(ChestManager chest, string message, int gems)
    {
        if(PopUpCoroutine!=null)
            StopCoroutine(PopUpCoroutine);
        chestFrom = chest;
        if(!ChestSlotManager.GetInstance().timerStarted)
            firstButtonText.text = "Start CountDown";
        else
            firstButtonText.text = "Add Chest to Unlocking Queue";

        secondButtonText.text = "Unlock using Gems:" + gems.ToString();
        Message.text = message;
        if(chestFrom.addedToQueue)
            firstButton.transform.gameObject.SetActive(false);
        else
            firstButton.transform.gameObject.SetActive(true);
        secondButton.transform.gameObject.SetActive(true);
    }

    public void FirstButtonSelected()
    {
        gameObject.SetActive(false);
        if (!ChestSlotManager.GetInstance().timerStarted)
        {
            Debug.Log("First Chest to be added to Unlocking Queue.");
            ChestSlotManager.GetInstance().unlockingQueue.Add(chestFrom);
            chestFrom.addedToQueue = true;
            chestFrom.StartTimer();
        }
        else
            ChestSlotManager.GetInstance().AddChestToUnlockingQueue(chestFrom);
    }

    public void UnlockChestSelected()
    {
        gameObject.SetActive(false);
        chestFrom.UnlockChestUsingGems();
    }

    public void OnlyDisplay(string message)
    {
        Message.text = message;
        firstButton.transform.gameObject.SetActive(false);
        secondButton.transform.gameObject.SetActive(false);
        PopUpCoroutine = StartCoroutine(DisablePopUp());
    }

    IEnumerator DisablePopUp()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
