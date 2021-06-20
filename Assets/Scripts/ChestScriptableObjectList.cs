using UnityEngine;
using System;

[CreateAssetMenu(fileName = "ChestScriptableObjectList", menuName = "ScriptableObjects/NewChestScriptableObjectList")]
public class ChestScriptableObjectList : ScriptableObject
{
    public ChestScriptableObject[] Chests;
}

[Serializable]
public class ChestScriptableObject 
{
    public string Type;
    public int minCoins;
    public int maxCoins;
    public int minGems;
    public int maxGems;
    public int TimeToUnlockInSeconds;
}
