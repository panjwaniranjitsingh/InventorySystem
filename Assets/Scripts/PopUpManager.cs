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
        firstButton.text = "Start CountDown";
        secondButton.text = "Unlock using Gems:" + gems.ToString();
        if (chestFrom.canUnlock)
        {
            transform.GetChild(0).GetComponent<Text>().text = message;
            if (!chestFrom.startTimer)
            {
                transform.GetChild(1).gameObject.SetActive(true);
            }
            else
                transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(0).GetComponent<Text>().text = message + " Can not unlock";
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
            StartCoroutine(DisablePopUp());
        }
    }

    public void YesButton()
    {
        gameObject.SetActive(false);
        chestFrom.StartTimer();
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
