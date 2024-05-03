using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[System.Serializable]
public struct UpgradeStats
{
    public int defence, regeneration, pierce, burstAmount;
    public float damage, projectileSpeed, healthMultiplier, movementSpeed, areaOfEffectSize, duration, moneyMultiplier, xpMultiplier;
    public int[] savedLevels;
    public int savedMoney;

}
public class GlobalUpgrades : MonoBehaviour
{
    public static GlobalUpgrades Instance { get; private set; }

    [SerializeField] public UpgradeStats upgradeStats;

    private void Awake()
    {   
        
            
            
        
       
        if (Instance == null)
        {   transform.parent = null; // Ensure it's a root object
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadFromPlayerPrefs();
            upgradeStats.savedLevels = new int[12];
        }
        else
        {   
            
            Destroy(gameObject);
            
        }

    }
        
    

    public void SaveToPlayerPrefs()
    {
            
        string jsonData = JsonUtility.ToJson(upgradeStats);
        PlayerPrefs.SetString("UpgradeStats", jsonData);
        PlayerPrefs.Save();
    }

    public void LoadFromPlayerPrefs()
    {   
        string jsonData = PlayerPrefs.GetString("UpgradeStats", "{}");
        JsonUtility.FromJsonOverwrite(jsonData, upgradeStats);
        
        
    }
    
}
