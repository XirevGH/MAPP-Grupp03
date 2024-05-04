using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeController : MonoBehaviour
{
    public static UpgradeController Instance { get; private set; }
    

    
    [SerializeField] private TextMeshProUGUI MoneyText1;

    [SerializeField] private TextMeshProUGUI[] statCostTexts;
    [SerializeField] private TextMeshProUGUI[] statNameTexts; 
    [SerializeField] private TextMeshProUGUI[] statIncreaseTexts;
    [SerializeField] private TextMeshProUGUI[] statInfoTexts;
    
    
    
    private float[] multipliers =           {   1f,         1f,      1f,         1f,         1f,         5f,         5f,             10f,       5f,      5f,         5f,         5f};
    
    public int[] levels = new int[12];

    private string[] upgradeNames =         { "Defence", "Regen", "Pierce", "Projectils", "Damage", "ProjSpeed", "HealthMult", "MoveSpeed", "AOE", "Duration", "MoneyMult", "XpMult" };
    private string[] statUnits =            {    "",       "",       "",         "",         "%",       "%",         "%",          "%",       "%",     "%",        "%",        "%" };
    private int[] perLevelPriceIncrease  =   {   100,       100,     1000,       1000,       200,        100,        100,            100,      100,     200,        500,        500 };

    public int money;
   
    
   
    public void Start()
    {  
       
        
        InitializePanel();
    }

    private void InitializePanel()
    {
        if (statCostTexts.Length < levels.Length ||
            statInfoTexts.Length < levels.Length ||
            statIncreaseTexts.Length < levels.Length ||
            statNameTexts.Length < levels.Length)
        {
            Debug.LogError("UI arrays do not match level arrays length.");
            return;
        }

        MoneyText1.SetText(money.ToString());
        for (int i = 0; i < levels.Length; i++)
        {
            statCostTexts[i].SetText(((levels[i] + 1) * perLevelPriceIncrease[i]).ToString());
            statInfoTexts[i].SetText((levels[i] * multipliers[i]) + statUnits[i]);
            statIncreaseTexts[i].SetText("+" + multipliers[i] + statUnits[i]);
            statNameTexts[i].SetText(upgradeNames[i]); 
        }
        
    }

    public void BuyUpgrade(int statIndex)
    {
        int cost = (levels[statIndex] + 1) * perLevelPriceIncrease[statIndex];
        if (money >= cost)
        {
            money -= cost;
            levels[statIndex]++;
            UpdateUI(statIndex);
            
        }
    }
    public void RefundLastUpgrade()
    {
        int totalRefund = 0;

        for (int i = 0; i < levels.Length; i++)
        {
            for(int b = 1; b <= levels[i]; b++){
                totalRefund += b * perLevelPriceIncrease[i];
            }
            

           
            levels[i] = 0;

            
            UpdateUI(i);
        }

    
    money += totalRefund;

   
    MoneyText1.SetText(money.ToString());

    
    Debug.Log($"Total refund provided: {totalRefund}. All levels reset.");
    }

    private void UpdateUI(int statIndex)
    {
        MoneyText1.SetText(money.ToString());
        statCostTexts[statIndex].SetText(((levels[statIndex] + 1) * perLevelPriceIncrease[statIndex]).ToString());
        statInfoTexts[statIndex].SetText((levels[statIndex] * multipliers[statIndex]) + statUnits[statIndex]);
        SetStats();
    }
    
    public void SetStats()
    {
        GlobalUpgrades.Instance.upgradeStats.savedLevels = (int[]) levels.Clone();
        GlobalUpgrades.Instance.upgradeStats.savedMoney = money;

        GlobalUpgrades.Instance.upgradeStats.defence = levels[0] * (int)multipliers[0];
        GlobalUpgrades.Instance.upgradeStats.regeneration = levels[1] * (int)multipliers[1];
        GlobalUpgrades.Instance.upgradeStats.pierce = levels[2] * (int)multipliers[2];
        GlobalUpgrades.Instance.upgradeStats.burstAmount = levels[3] * (int)multipliers[3];

        GlobalUpgrades.Instance.upgradeStats.damage = levels[4] * multipliers[4]/100;
        GlobalUpgrades.Instance.upgradeStats.projectileSpeed = levels[5] * multipliers[5]/100;
        GlobalUpgrades.Instance.upgradeStats.healthMultiplier = levels[6] * multipliers[6]/100;
        GlobalUpgrades.Instance.upgradeStats.movementSpeed = levels[7] * multipliers[7]/100;
        GlobalUpgrades.Instance.upgradeStats.areaOfEffectSize = levels[8] * multipliers[8]/100;
        GlobalUpgrades.Instance.upgradeStats.duration = levels[9] * multipliers[9]/100;
        GlobalUpgrades.Instance.upgradeStats.moneyMultiplier = levels[10] * multipliers[10]/100;
        GlobalUpgrades.Instance.upgradeStats.xpMultiplier = levels[11] * multipliers[11]/100;

    
    }
    


    public void AddMoney(int addedMoney)
    {
        money += addedMoney;
        // Call UpdateUI for each stat or only update the money text if that's all that's needed
        MoneyText1.SetText(money.ToString());
    }
    
    
}



     
    



    

    


