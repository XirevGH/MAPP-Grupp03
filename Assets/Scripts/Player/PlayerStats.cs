using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
[System.Serializable]
public struct CharacterStats
{
    public int baseHealth, money; 
    [Range(-1, 10)] public float basemovementSpeed;
    [Range(-1, 10)] public float damage, projectileSpeed, healthMultiplier;
    [Range(-1, 5)] public float movementSpeed, areaOfEffectSize, duration, moneyMultiplier, xpMultiplier;
    [Range(-1, 10)] public int defence, regeneration;
    [Range(-1, 5)] public int pierce, burstAmount;

    


}
public class PlayerStats : MonoBehaviour
{   private UpgradeStats menuUpgradeStats;
    
    [SerializeField] public CharacterStats stats = new CharacterStats{
        baseHealth = 100, money = 0, basemovementSpeed = 5, damage = 1, projectileSpeed = 1, healthMultiplier = 1,
        movementSpeed = 1, areaOfEffectSize = 1, duration = 1, moneyMultiplier = 1, xpMultiplier = 1,  
        defence = 0, regeneration = 0,
        pierce = 0, burstAmount = 0, 
    };

    private void Start() {
        menuUpgradeStats = GlobalUpgrades.Instance.upgradeStats;
        AddMenuStats();
        GetComponent<Player>().InitializePlayerStats();
        GetComponent<PlayerMovement>().movementSpeed = stats.basemovementSpeed;
    }
    public void AddMenuStats() {
        stats.damage += menuUpgradeStats.damage;
        stats.projectileSpeed += menuUpgradeStats.projectileSpeed;
        stats.healthMultiplier += menuUpgradeStats.healthMultiplier;
        stats.movementSpeed += menuUpgradeStats.movementSpeed;
        stats.areaOfEffectSize += menuUpgradeStats.areaOfEffectSize;
        stats.duration += menuUpgradeStats.duration;
        stats.moneyMultiplier += menuUpgradeStats.moneyMultiplier;
        stats.xpMultiplier += menuUpgradeStats.xpMultiplier;
        stats.defence += menuUpgradeStats.defence;
        stats.regeneration += menuUpgradeStats.regeneration;
        stats.pierce += menuUpgradeStats.pierce;
        stats.burstAmount += menuUpgradeStats.burstAmount;
    }

    
    
    
    public void DamageUpgrade(float increase){
        stats.damage += increase;
    }
    public void ProjectileSpeedUpgrade(float increase){
        stats.projectileSpeed += increase;
    }
    public void HealthMultiplierUpgrade(float increase){
        stats.healthMultiplier += increase;
    }
    public void MovementSpeedUpgrade(float increase){
        stats.movementSpeed += increase;
    }
    public void AreaOfEffectSizeUpgrade(float increase){
        stats.areaOfEffectSize += increase;
    }
    public void DurationUpgrade(float increase){
        stats.duration += increase;
    }
    public void MoneyMultiplierUpgrade(float increase){
        stats.moneyMultiplier += increase;
    }
    public void XpMultiplierUpgrade(float increase){
        stats.xpMultiplier += increase;
    }
    public void DefenceUpgrade(int increase){
        stats.defence += increase;
    }
    public void RegenerationUpgrade(int increase){
        stats.regeneration += increase;
    }
    public void PierceUpgrade(int increase){
       stats.pierce += increase;
    }
    public void BurstAmountUpgrade(int increase){
        stats.burstAmount += increase;
    }

    public void AddMoneyStast(int increase){
        stats.money +=(int) (increase * stats.moneyMultiplier);
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
