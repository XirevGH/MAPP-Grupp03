using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
public struct PlayerStatsStruct
{
    public float health;
    public int money;
    public float xpToLevel;
    public int level;
    public float xpHeld;

    //wepons
    
    
}
public class PlayerStats : MonoBehaviour
{
    [SerializeField] public static PlayerStatsStruct playerStatsStruct = new PlayerStatsStruct {health = 100, money = 0, xpToLevel = 100, level = 0, xpHeld = 0, };

    private void Awake() {
        playerStatsStruct = new PlayerStatsStruct {health = 100, money = 0, xpToLevel = 100, level = 0, xpHeld = 0, };
    }
    

    public string SaveToString()
    {
        return JsonUtility.ToJson(this);
    }

    public void CreateFromJSON(string jsonString)
    {
        JsonUtility.FromJsonOverwrite(jsonString, this);
    }
}
