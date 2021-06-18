using UnityEngine;

[CreateAssetMenu(fileName = "PlayerScriptableObject", menuName = "ScriptableObjects/NewPlayerScriptableObject")]
public class PlayerScriptableObject : ScriptableObject
{
    public string Name;
    public int Coins;
    public int Gems;
}
