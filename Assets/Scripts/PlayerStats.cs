using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] public int money;
    [SerializeField] public float moneyMultiplier;
    [SerializeField] public int damage;
    [SerializeField] public int areaOfEffectSize;
    [SerializeField] public int pierce;
    [SerializeField] public float xpMultiplier;
    [SerializeField] public int health;
    [SerializeField] public int defence;
    [SerializeField] public float movementSpeed;

    [SerializeField] public float xpHeld;
    [SerializeField] public float xpToLevel;
    [SerializeField] public int level;

    [SerializeField] public short burstAmount;


    public string SaveToString()
    {
        return JsonUtility.ToJson(this);
    }

    public void CreateFromJSON(string jsonString)
    {
        JsonUtility.FromJsonOverwrite(jsonString, playerStats);
    }
}
