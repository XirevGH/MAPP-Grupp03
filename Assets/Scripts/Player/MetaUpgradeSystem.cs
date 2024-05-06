using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class MetaUpgradeSystem : MonoBehaviour
{
    [SerializeField] private int money;
    [SerializeField] private TextMeshProUGUI MoneyText1;
    [SerializeField] private TextMeshProUGUI MoneyText2;
    
    [SerializeField] private TextMeshProUGUI DamageCost;
    [SerializeField] private TextMeshProUGUI DamageStats;

    [SerializeField] private TextMeshProUGUI BPMCost;
    [SerializeField] private TextMeshProUGUI BPMStats;

    [SerializeField] private TextMeshProUGUI SizeCost;
    [SerializeField] private TextMeshProUGUI SizeStats;

    [SerializeField] private TextMeshProUGUI PierceCost;
    [SerializeField] private TextMeshProUGUI PierceStats;

    [SerializeField] private TextMeshProUGUI XpMultCost;
    [SerializeField] private TextMeshProUGUI XpMultStats;

    [SerializeField] private TextMeshProUGUI HPCost;
    [SerializeField] private TextMeshProUGUI HPStats;

    [SerializeField] private TextMeshProUGUI DefenceCost;
    [SerializeField] private TextMeshProUGUI DefenceStats;

    [SerializeField] private TextMeshProUGUI MovSpeedCost;
    [SerializeField] private TextMeshProUGUI MovSpeedStats;

    [SerializeField] private TextMeshProUGUI InvincibilityCost;
    [SerializeField] private TextMeshProUGUI InvincibilityStats;

    [SerializeField] private TextMeshProUGUI MoneyMultCost;
    [SerializeField] private TextMeshProUGUI MoneyMultStats;

    [SerializeField] private List<Item> items;


    private List<int> upgrades = new List<int>();

    private Dictionary<Tuple<string, string>, int> upgradeMap;
    public int perLevelPriceIncrease;
    private int levelDamage;
    private int levelBPM;
    private int levelSize;
    private int levelPrice;
    private int levelXpMultiplier;
    private int levelHP;
    private int levelDefence;
    private int levelMoveSpeed;
    private int levelInvincibilityFrames;
    private int levelMoneyMult;

    string upgradeStatsFile;

    private MetaUpgradeSystem instance;

    [SerializeField] private int bassGuitarIncreaseDamage;
    [SerializeField] private int yoyoIncreaseProjectileCount;
    [SerializeField] private int yoyoIncreaseDamage;
    [SerializeField] private int discoBallIncreaseProjectileCount;
    [SerializeField] private int discoBallIncreasePenetrationAmount;
    [SerializeField] private int discoBallIncreaseDamage;
    [SerializeField] private int electricGuitarIncreaseTetherAmount;
    [SerializeField] private int electricGuitarIncreaseDamage;
    [SerializeField] private int saxophoneIncreaseProjectileCount;
    [SerializeField] private int saxophoneIncreasePenetrationAmount;
    [SerializeField] private int saxophoneIncreaseDamage;
    [SerializeField] private int synthwaveBlastIncreaseProjectileCount;
    [SerializeField] private int synthwaveBlastIncreasePenetrationAmount;
    [SerializeField] private int synthwaveBlastIncreaseDamage;
    [SerializeField] private int vinylRecordIncreaseProjectileCount;
    [SerializeField] private int vinylRecordIncreasePenetrationAmount;
    [SerializeField] private int vinylRecordIncreaseDamage;
    [SerializeField] private int chillVibeIncreaseRadius;
    [SerializeField] private int chillVibeIncreaseSlow;
    [SerializeField] private int decoyIncreaseDecoyAmount;
    [SerializeField] private int decoyIncreaseDecoyHealth;
    [SerializeField] private int grooveArmorIncreaseHealth;
    [SerializeField] private int rollerSkatesIncreaseMovementSpeed;
    [SerializeField] private int stagePresenceIncreaseDamage;
    [SerializeField] private int stagePresenceIncreaseRadius;

    public static MetaUpgradeSystem Instance
    {
        get; private set;
    }
    private void CreateUpgradeMap()
    {
        upgradeMap = new Dictionary<Tuple<string, string>, int>();
        foreach (Item item in items)
        {
            foreach(string upgradeMethod in item.GetUpgradeOptions()) 
            { 
                string variableName = char.ToLower(item.GetName()[0]) + item.GetName().Substring(1).Replace(" ", "") + upgradeMethod;
                Debug.Log("Variable name: " + variableName);
                Debug.Log("Variable value: " + (int)typeof(MetaUpgradeSystem).GetField(variableName, BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this));
                Debug.Log("Item name: " + item.GetName());
                Debug.Log("Upgrade method :" + upgradeMethod);
                upgradeMap.Add(Tuple.Create(item.GetName(), upgradeMethod), (int)typeof(MetaUpgradeSystem).GetField(variableName, BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this));
            }

        }
    }

    private void Awake()
    {
        Debug.Log("upgrade awake");
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        InitialisingPanel();
        upgradeStatsFile = Application.persistentDataPath + "/upgradeInfo.json";
        ReadFile(upgradeStatsFile);
        CreateUpgradeMap();
        foreach (Item item in items)
        {
            foreach(string upgradeMethod in item.GetUpgradeOptions())
            {
                int rank = upgradeMap[Tuple.Create(item.GetName(), upgradeMethod)];
                for(int i = 0; i < rank; i++)
                {
                    item.GetType().GetMethod(upgradeMethod).Invoke(item, null);
                }
            }
        }
    }

    private void ReadFile(string saveFile)
    {
        if (File.Exists(saveFile))
        {
            string fileContents = File.ReadAllText(saveFile);
            CreateFromJSON(fileContents);
        }
    }

    public void CreateFromJSON(string jsonString)
    {
        JsonUtility.FromJsonOverwrite(jsonString, this);
    }

    public string SaveToString()
    {
        return JsonUtility.ToJson(this);
    }


    public void InitialisingPanel()
    {
    
        levelDamage = 0;
        levelBPM = 0;
        levelSize = 0;
        levelPrice = 0;
        levelXpMultiplier = 0;
        levelHP = 0;
        levelDefence = 0;
        levelMoveSpeed = 0;
        levelInvincibilityFrames = 0;
        levelMoneyMult = 0;
    
/*

        MoneyText1.SetText(money.ToString());
        MoneyText2.SetText(money.ToString());

        DamageCost.SetText(((levelDamage + 1) * perLevelPriceIncrease).ToString());
        DamageStats.SetText(levelDamage * 5 + "%");

        BPMCost.SetText(((levelBPM + 1) * perLevelPriceIncrease).ToString());
        BPMStats.SetText(levelBPM * 5 + "%");

        SizeCost.SetText(((levelSize + 1) * perLevelPriceIncrease).ToString());
        SizeStats.SetText(levelSize * 10 + "%");

        
        PierceCost.SetText(((levelPrice + 1) * perLevelPriceIncrease).ToString());
        PierceStats.SetText(levelPrice + "");

        XpMultCost.SetText(((levelXpMultiplier + 1) * perLevelPriceIncrease).ToString());
        XpMultStats.SetText(levelXpMultiplier * 2.5 + "%");

        HPCost.SetText(((levelHP + 1) * perLevelPriceIncrease).ToString());
        HPStats.SetText(levelHP * 10 + "%");

        DefenceCost.SetText(((levelDefence + 1) * perLevelPriceIncrease).ToString());
        DefenceStats.SetText(levelDefence * 5 + "");

        MovSpeedCost.SetText(((levelMoveSpeed + 1) * perLevelPriceIncrease).ToString());
        MovSpeedStats.SetText(levelMoveSpeed * 0.1 + "");

        InvincibilityCost.SetText(((levelInvincibilityFrames + 1) * perLevelPriceIncrease).ToString());
        InvincibilityStats.SetText(levelInvincibilityFrames * 0.1 + "s");

        MoneyMultCost.SetText(((levelMoneyMult + 1) * perLevelPriceIncrease).ToString());
        MoneyMultStats.SetText(levelMoneyMult * 2.5 + "%");
*/
        
    }

    public void ResetUppgrades(){
        upgrades.AddRange(new int[] { levelDamage, levelBPM, levelSize, levelPrice, levelXpMultiplier, levelHP, levelDefence, levelMoveSpeed, levelInvincibilityFrames, levelMoneyMult });
        foreach (int level in upgrades){

            for (int i = 1; i <= level; ++i)
            {
                money += i * perLevelPriceIncrease; 
            }
        }
        upgrades.Clear();
        
        InitialisingPanel();
    }

    public void UpgradeRank(string classAndMethod)
    {
        string[] parts = classAndMethod.Split(','); 
        string className = parts[0];
        string methodName = parts[1];
        string variableName = char.ToLower(className[0]) + className.Substring(1).Replace(" ", "") + methodName;
        int variableValueBefore = (int)typeof(MetaUpgradeSystem).GetField(variableName, (BindingFlags) 36).GetValue(this);
        int variableValueAfter = variableValueBefore + 1;
        typeof(MetaUpgradeSystem).GetField(variableName, BindingFlags.NonPublic | BindingFlags.Instance).SetValue(this, variableValueAfter);
        
        foreach (Item item in items)
        {
            if (item.GetName().Equals(className))
            {
                item.GetType().GetMethod(methodName).Invoke(item, null);
            }
        }
        File.WriteAllText(upgradeStatsFile, SaveToString());
    }

    public void ByDameg()
    {
        if(EnothMoney((levelDamage + 1) * perLevelPriceIncrease)){
            levelDamage += 1;
            DamageCost.SetText(((levelDamage + 1) * perLevelPriceIncrease).ToString());
            DamageStats.SetText(levelDamage * 5 + "%");
        }
    }

    public void ByBPM()
    {
        if(EnothMoney((levelBPM + 1) * perLevelPriceIncrease)){
            levelBPM += 1;
            BPMCost.SetText(((levelBPM + 1) * perLevelPriceIncrease).ToString());
            BPMStats.SetText(levelBPM * 5 + "%");
        }
    }

    public void BySize()
    {
        if(EnothMoney((levelSize + 1) * perLevelPriceIncrease)){
            levelSize += 1;
            SizeCost.SetText(((levelSize + 1) * perLevelPriceIncrease).ToString());
            SizeStats.SetText(levelSize * 10 + "%");
        }
    }

    public void ByPirce()
    {
        if(EnothMoney((levelPrice + 1) * perLevelPriceIncrease)){
            levelPrice += 1;
            PierceCost.SetText(((levelPrice + 1) * perLevelPriceIncrease).ToString());
            PierceStats.SetText(levelPrice + "");
        }
    }

    public void ByXpMultiplayer()
    {
        if(EnothMoney((levelXpMultiplier + 1) * perLevelPriceIncrease)){
            levelXpMultiplier += 1;
            XpMultCost.SetText(((levelXpMultiplier + 1) * perLevelPriceIncrease).ToString());
            XpMultStats.SetText(levelXpMultiplier * 2.5 + "%");
        }
    }

    public void ByHP()
    {
        if(EnothMoney((levelHP + 1) * perLevelPriceIncrease)){
            levelHP += 1;
            HPCost.SetText(((levelHP + 1) * perLevelPriceIncrease).ToString());
            HPStats.SetText(levelHP * 10 + "%");
            
        }
    }

    public void ByDefence()
    {
        if(EnothMoney((levelDefence + 1)  * perLevelPriceIncrease)){
            levelDefence += 1;
            DefenceCost.SetText(((levelDefence + 1) * perLevelPriceIncrease).ToString());
            DefenceStats.SetText(levelDefence * 5 + "");
        }
    }

    public void ByMovSpeed()
    {
        if(EnothMoney((levelMoveSpeed + 1) * perLevelPriceIncrease)){
            levelMoveSpeed += 1;
            MovSpeedCost.SetText(((levelMoveSpeed + 1) * perLevelPriceIncrease).ToString());
            MovSpeedStats.SetText(levelMoveSpeed * 0.1 + "");
        }
    }

    public void ByInvinsebiletyFrames()
    {
       if(EnothMoney((levelInvincibilityFrames + 1) * perLevelPriceIncrease)){
            levelInvincibilityFrames += 1;
            InvincibilityCost.SetText(((levelInvincibilityFrames + 1) * perLevelPriceIncrease).ToString());
            InvincibilityStats.SetText(levelInvincibilityFrames * 0.1 + "s");
        }
    }

    public void ByMoneyMult()
    {
       if(EnothMoney((levelMoneyMult + 1) * perLevelPriceIncrease)){
            levelMoneyMult += 1;
            MoneyMultCost.SetText(((levelMoneyMult + 1) * perLevelPriceIncrease).ToString());
            MoneyMultStats.SetText(levelMoneyMult * 2.5 + "%");
        }
    }

    public bool EnothMoney(int price)
    {
        if(money >= price){
            money -= price;
            MoneyText1.SetText(money.ToString());
            MoneyText2.SetText(money.ToString());
            return true;
        }
        return false;
    }

    public List<Item> GetItems() 
    { 
        return items;
    }
}
