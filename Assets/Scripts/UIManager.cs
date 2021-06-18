using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] PlayerScriptableObject playerData;

    // Start is called before the first frame update
    void Start()
    {
        ShowPlayerData();
    }

    private void ShowPlayerData()
    {
        player.transform.GetChild(0).gameObject.GetComponent<Text>().text ="Name:"+ playerData.Name;
        player.transform.GetChild(1).gameObject.GetComponent<Text>().text ="Coins:"+ playerData.Coins.ToString();
        player.transform.GetChild(2).gameObject.GetComponent<Text>().text ="Gems:"+ playerData.Gems.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
