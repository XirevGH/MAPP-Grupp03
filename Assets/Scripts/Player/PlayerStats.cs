using System;
using UnityEngine;

[Serializable]
public struct PlayerStatsStruct
{
    public float health;
    public int money;
    public float xpToLevel;
    public int level;
    public float xpHeld;
}

public class PlayerStats : MonoBehaviour
{
    public PlayerStatsStruct stats = new PlayerStatsStruct
    {
        health = 100,
        money = 0,
        xpToLevel = 100,
        level = 1,
        xpHeld = 0
    };

    public string SaveToString() => JsonUtility.ToJson(this, true);
    public void CreateFromJSON(string jsonString) => JsonUtility.FromJsonOverwrite(jsonString, this);
}