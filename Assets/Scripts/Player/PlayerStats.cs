using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
[System.Serializable]
public struct Stats
{
    public int baseHealth, money;
    [Range(-1, 10)] public float damage, projectileSpeed, healthMultiplier;
    [Range(-1, 5)] public float movementSpeed, areaOfEffectSize, duration, moneyMultiplier, xpMultiplier;
    [Range(-1, 10)] public int defence, regeneration;
    [Range(-1, 5)] public int pierce, burstAmount;

    


}
public class PlayerStats : MonoBehaviour
{   [SerializeField] private GlobalUpgrades upgradeController;
    
    [SerializeField] public Stats stats = new Stats{
        baseHealth = 100, money = 0, damage = 1, projectileSpeed = 1, healthMultiplier = 1,
        movementSpeed = 1, areaOfEffectSize = 1, duration = 1, moneyMultiplier = 1, xpMultiplier = 1,  
        defence = 0, regeneration = 0,
        pierce = 0, burstAmount = 0, 
    };
    public int money;


    private void Start(){

    
    stats.damage += upgradeController.upgradeStats.damage;
    stats.projectileSpeed += upgradeController.upgradeStats.projectileSpeed;
    stats.healthMultiplier += upgradeController.upgradeStats.healthMultiplier;
    stats.movementSpeed += upgradeController.upgradeStats.movementSpeed;
    stats.areaOfEffectSize += upgradeController.upgradeStats.areaOfEffectSize;
    stats.duration += upgradeController.upgradeStats.duration;
    stats.moneyMultiplier += upgradeController.upgradeStats.moneyMultiplier;
    stats.xpMultiplier += upgradeController.upgradeStats.xpMultiplier;
    stats.defence += upgradeController.upgradeStats.defence;
    stats.regeneration += upgradeController.upgradeStats.regeneration;
    stats.pierce += upgradeController.upgradeStats.pierce;
    stats.burstAmount += upgradeController.upgradeStats.burstAmount;
    }
    
    
    
    
    

    
    
    
    public string SaveToString()
    {
        return JsonUtility.ToJson(stats);
    }

    public void CreateFromJSON(string jsonString)
    {
        JsonUtility.FromJsonOverwrite(jsonString, stats);
    }
}
