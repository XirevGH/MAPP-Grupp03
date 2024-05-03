using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/*
[System.Serializable]
public struct UpgradeStats
{
    public int defence, regeneration, pierce, burstAmount;
    public float damage, projectileSpeed, healthMultiplier, movementSpeed, areaOfEffectSize, duration, moneyMultiplier, xpMultiplier;
    public int[] savedLevels;
    public int savedMoney;

    public string[] statCostObjects;
    public string[] statNameObjects;
    public string[] statIncreaseObjects;
    public string[] statInfoObjects;
}
*/
public class UpgradeController : MonoBehaviour
{
    public static UpgradeController Instance { get; private set; }
    

    
    [SerializeField] private TextMeshProUGUI MoneyText1;

    [SerializeField] private TextMeshProUGUI[] statCostTexts;
    [SerializeField] private TextMeshProUGUI[] statNameTexts; 
    [SerializeField] private TextMeshProUGUI[] statIncreaseTexts;
    [SerializeField] private TextMeshProUGUI[] statInfoTexts;
    //[SerializeField] public UpgradeStats upgradeStats;
    
    
    private float[] multipliers =           {   1f,         1f,      1f,         1f,         1f,         5f,         5f,             10f,       5f,      5f,         5f,         5f};
    
    public int[] levels = new int[12];

    private string[] upgradeNames =         { "Defence", "Regen", "Pierce", "Projectils", "Damage", "ProjSpeed", "HealthMult", "MoveSpeed", "AOE", "Duration", "MoneyMult", "XpMult" };
    private string[] statUnits =            {    "",       "",       "",         "",         "%",       "%",         "%",          "%",       "%",     "%",        "%",        "%" };
    private int[] perLevelPriceIncrease  =   {   100,       100,     1000,       1000,       200,        100,        100,            100,      100,     200,        500,        500 };

    public int money;
   
   private void Awake()
    {   
        
            
            
        
        /*
        if (Instance == null)
        {   SaveTMPUI();
            transform.parent = null; // Ensure it's a root object
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadFromPlayerPrefs();
        }
        else
        {   
            
            Destroy(gameObject);
            
        } */
        
    }
    
   
    public void Start()
    {  
        /* 
       if(statCostTexts[0]){
            SetTMPUI();
        }*/
        
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
            //SaveToPlayerPrefs();  // Save whenever changes are made
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
            

            // Reset the level
            levels[i] = 0;

            // Update the UI for each upgrade
            UpdateUI(i);
        }

    // Add the total refund to the money
    money += totalRefund;

    // Update the money display separately if it's not part of UpdateUI
    MoneyText1.SetText(money.ToString());

    // Optionally, save the changes
    //SaveToPlayerPrefs();

    Debug.Log($"Total refund provided: {totalRefund}. All levels reset.");
    }

    private void UpdateUI(int statIndex)
    {
        MoneyText1.SetText(money.ToString());
        statCostTexts[statIndex].SetText(((levels[statIndex] + 1) * perLevelPriceIncrease[statIndex]).ToString());
        statInfoTexts[statIndex].SetText((levels[statIndex] * multipliers[statIndex]) + statUnits[statIndex]);
    }
    /*
    public void SaveToPlayerPrefs()
    {
        SetStats();  // Update the stats struct with current data
        string jsonData = JsonUtility.ToJson(upgradeStats);
        PlayerPrefs.SetString("UpgradeStats", jsonData);
        PlayerPrefs.Save();
    }

    public void LoadFromPlayerPrefs()
    {   
        string jsonData = PlayerPrefs.GetString("UpgradeStats", "{}");
        JsonUtility.FromJsonOverwrite(jsonData, upgradeStats);
        ApplyLoadedStats();
        
    }

    public void ApplyLoadedStats()
    {
        if (upgradeStats.savedLevels != null && upgradeStats.savedLevels.Length == levels.Length)
        {
            levels = (int[]) upgradeStats.savedLevels.Clone();
        }
        else
        {
            Debug.LogWarning("Mismatch in saved levels length; resetting to default.");
            levels = new int[12]; // Default initialization
        }

        money = upgradeStats.savedMoney;
        
        InitializePanel();
    }
    */
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
    /*
    private void SaveTMPUI(){

        if( upgradeStats.statCostObjects.Length < 12){
            upgradeStats.statCostObjects = new string[12];
            upgradeStats.statNameObjects = new string[12];
            upgradeStats.statIncreaseObjects = new string[12];
            upgradeStats.statInfoObjects = new string[12];
        }
        for (int i = 0; i < 12; i++) {
            if (statCostTexts[i] != null && statCostTexts[i].transform.parent != null) {
                upgradeStats.statCostObjects[i] = statCostTexts[i].transform.parent.gameObject.name;
            } else {
                Debug.LogError("statCostTexts[i] is null or has no parent.");
            }

            if (statNameTexts[i] != null && statNameTexts[i].transform.parent != null) {
                upgradeStats.statNameObjects[i] = statNameTexts[i].transform.parent.gameObject.name;
            } else {
                Debug.LogError("statNameTexts[i] is null or has no parent.");
            }

            if (statIncreaseTexts[i] != null && statIncreaseTexts[i].transform.parent != null) {
                upgradeStats.statIncreaseObjects[i] = statIncreaseTexts[i].transform.parent.gameObject.name;
            } else {
                Debug.LogError("statIncreaseTexts[i] is null or has no parent.");
            }

            if (statInfoTexts[i] != null && statInfoTexts[i].transform.parent != null) {
                upgradeStats.statInfoObjects[i] = statInfoTexts[i].transform.parent.gameObject.name;
            } else {
                Debug.LogError("statInfoTexts[i] is null or has no parent.");
            }
        }
    }

    public void SetTMPUI() {
    for (int i = 0; i < 12; i++) {
        int index = i;  // Local variable capturing the loop's current index value

        // Find and setup UI components
        statCostTexts[i] = GameObject.Find(upgradeStats.statCostObjects[i]).GetComponentInChildren<TextMeshProUGUI>();
        
        statIncreaseTexts[i] = GameObject.Find(upgradeStats.statIncreaseObjects[i]).GetComponentInChildren<TextMeshProUGUI>();
        statInfoTexts[i] = GameObject.Find(upgradeStats.statInfoObjects[i]).GetComponentInChildren<TextMeshProUGUI>();

        // Setup button with the correct index
        GameObject buttonGameObject = GameObject.Find(upgradeStats.statNameObjects[i]);
        if (buttonGameObject != null) {
            Button button = buttonGameObject.GetComponent<Button>();
            if (button != null) {
                button.onClick.RemoveAllListeners();  // Clear existing listeners to avoid duplicate calls
                button.onClick.AddListener(() => BuyUpgrade(index));
            } else {
                Debug.LogError("Button component not found on GameObject named: " + upgradeStats.statNameObjects[i]);
            }
        } else {
            Debug.LogError("GameObject not found: " + upgradeStats.statNameObjects[i]);
        }

        statNameTexts[i] = buttonGameObject.GetComponentInChildren<TextMeshProUGUI>();
    }
}*/



    public void AddMoney(int addedMoney)
    {
        money += addedMoney;
        // Call UpdateUI for each stat or only update the money text if that's all that's needed
        MoneyText1.SetText(money.ToString());
    }
    
    
}



     
    



    

    


