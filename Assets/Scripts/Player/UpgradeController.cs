using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public struct UpgradeStats
{
    public int defence, regeneration, pierce, burstAmount;
    public float damage, projectileSpeed, healthMultiplier, movementSpeed, areaOfEffectSize, duration, moneyMultiplier, xpMultiplier;
    public int[] savedLevels;
    public int savedMony;

}

public class UpgradeController : MonoBehaviour
{
    [SerializeField] public UpgradeStats upgradeStats;
    [SerializeField] private int money;
    [SerializeField] private TextMeshProUGUI MoneyText1;
    [SerializeField] private TextMeshProUGUI MoneyText2;

    [SerializeField] private TextMeshProUGUI[] statCostTexts;
    [SerializeField] private TextMeshProUGUI[] statNameTexts; 
    [SerializeField] private TextMeshProUGUI[] statIncreaseTexts;
    [SerializeField] private TextMeshProUGUI[] statInfoTexts;
    
    
    
    private float[] multipliers = { 1f,0.1f,1f,1f,5f,5f,10f,5f,5f,5f,5f,5f};
    private int[] levels = {0,0,0,0,0,0,0,0,0,0,0,0 };

    private string[] uppgradeName =         { "Defence", "Regen", "Pierce", "Projectils", "Damage", "ProjSpeed", "HealthMult", "MoveSpeed", "AOE", "Duration", "MoneyMult", "XpMult" };
    private string[] statUnits =            {    "",       "",       "",         "",         "%",       "%",         "%",          "%",       "%",     "%",        "%",        "%" };
    private int[] perLevelPriceIncrease =   {   100,       100,     1000,       1000,       200,        100,        100,            100,      100,     200,        500,        500 };

    private void Start()
    {
        
        if(upgradeStats.savedLevels.Length > 0){
            money = upgradeStats.savedMony;
            levels = upgradeStats.savedLevels;
        }else{
            levels = new int[statCostTexts.Length];
        }
       
        
        InitializePanel();
    }

    private void InitializePanel()
    {
        MoneyText1.SetText(money.ToString());
        MoneyText2.SetText(money.ToString());

        for (int i = 0; i < statCostTexts.Length; i++)
        {
            statCostTexts[i].SetText(((levels[i] + 1) * perLevelPriceIncrease[i]).ToString());
            statInfoTexts[i].SetText((levels[i] * multipliers[i]) + statUnits[i]);
            statIncreaseTexts[i].SetText("+"+ multipliers[i] + statUnits[i]);
            statNameTexts[i].SetText(uppgradeName[i]); 
        }
    }

    public void BuyUpgrade(int statIndex)
    {
        if (EnoughMoney((levels[statIndex] + 1) * perLevelPriceIncrease[statIndex]))
        {
            levels[statIndex]++;
            money -= (levels[statIndex]) * perLevelPriceIncrease[statIndex];
            UpdateUI(statIndex);
        }
    }

    private void UpdateUI(int statIndex)
    {
        MoneyText1.SetText(money.ToString());
        MoneyText2.SetText(money.ToString());
        statCostTexts[statIndex].SetText(((levels[statIndex] + 1) * perLevelPriceIncrease[statIndex]).ToString());
        statInfoTexts[statIndex].SetText((levels[statIndex] * multipliers[statIndex]) + statUnits[statIndex]);
        
    }

    private bool EnoughMoney(int price)
    {
        return money >= price;
    }
    public void RefundAllUpgrades()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            
            for (int level = levels[i]; level > 0; level--)
            {
                money += level * perLevelPriceIncrease[i];
            }
            
            levels[i] = 0;
            
            UpdateUI(i);
        }

        
        MoneyText1.SetText(money.ToString());
        MoneyText2.SetText(money.ToString());
    }
    public void SetStats(){

        upgradeStats.savedLevels = levels;
        upgradeStats.savedMony = money;

        upgradeStats.defence = levels[0] * (int)multipliers[0];
        upgradeStats.regeneration = levels[1] * (int)multipliers[1];
        upgradeStats.pierce = levels[2] * (int)multipliers[2];
        upgradeStats.burstAmount = levels[3] * (int)multipliers[3];

        upgradeStats.damage = levels[4] * multipliers[4]/100;
        upgradeStats.projectileSpeed = levels[5] * multipliers[5]/100;
        upgradeStats.healthMultiplier = levels[6] * multipliers[6]/100;
        upgradeStats.movementSpeed = levels[7] * multipliers[7]/100;
        upgradeStats.areaOfEffectSize = levels[8] * multipliers[8]/100;
        upgradeStats.duration = levels[9] * multipliers[9]/100;
        upgradeStats.moneyMultiplier = levels[10] * multipliers[10]/100;
        upgradeStats.xpMultiplier = levels[11] * multipliers[11]/100;
    }
    public string SaveToString()
    {   SetStats();
        return JsonUtility.ToJson(upgradeStats, true);
    }

    public void CreateFromJSON(string jsonString)
    {
        JsonUtility.FromJsonOverwrite(jsonString, upgradeStats);
        InitializePanel();
    }
    public void OnSaveButtonClicked()
    {
        string jsonData = SaveToString();
        PlayerPrefs.SetString("UpgradeData", jsonData);
        PlayerPrefs.Save(); 
    }

    public void OnLoadButtonClicked()
    {
        string jsonData = PlayerPrefs.GetString("UpgradeData", "{}"); 
        CreateFromJSON(jsonData);
    }

}
    

    

     
    



    

    


