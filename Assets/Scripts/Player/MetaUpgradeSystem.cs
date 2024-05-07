using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Data.SqlTypes;

public class MetaUpgradeSystem : MonoBehaviour
{
    [SerializeField] private int currency;
    //[SerializeField] private TextMeshProUGUI MoneyText1;
    //[SerializeField] private TextMeshProUGUI MoneyText2;

    private static bool initialUpgrades = true;

    [SerializeField] private List<Item> items;
    private Dictionary<Tuple<string, string>, int> upgradeMap;
    private Dictionary<Tuple<string, string>, int> costMap;
    string upgradeStatsFile;

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
    [SerializeField] private int personalSpaceIncreaseRadius;
    [SerializeField] private int personalSpaceIncreaseForce;

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
                /*Debug.Log("Variable name: " + variableName);
                Debug.Log("Variable value: " + (int)typeof(MetaUpgradeSystem).GetField(variableName, BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this));
                Debug.Log("Item name: " + item.GetName());
                Debug.Log("Upgrade method :" + upgradeMethod);*/
                upgradeMap.Add(Tuple.Create(item.GetName(), upgradeMethod), (int)typeof(MetaUpgradeSystem).GetField(variableName, BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this));
            }
        }
    }

    private void CreateCostMap()
    {
        costMap = new Dictionary<Tuple<string, string>, int>();
        foreach (Item item in items)
        {
            /*Debug.Log(item.GetName());
            Debug.Log(item.GetUpgradeOptions().Count);*/
            foreach (string upgradeMethod in item.GetUpgradeOptions())
            {
                
                string costMethod = "Get" + upgradeMethod + "Cost";
                /*Debug.Log(item.GetName());
                Debug.Log(upgradeMethod);
                Debug.Log(costMethod);*/
                costMap.Add(Tuple.Create(item.GetName(), upgradeMethod), (int)item.GetType().GetMethod(costMethod).Invoke(item, null));
            }
        }
    }

    private void Start()
    {
        Debug.Log("upgrade awake");
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        UpdateMoney();
        upgradeStatsFile = Application.persistentDataPath + "/upgradeInfo.json";
        ReadFile(upgradeStatsFile);
        CreateCostMap();
        CreateUpgradeMap();
        if (initialUpgrades)
        {
            foreach (Item item in items)
            {
                foreach (string upgradeMethod in item.GetUpgradeOptions())
                {
                    int rank = upgradeMap[Tuple.Create(item.GetName(), upgradeMethod)];
                    for (int i = 0; i < rank; i++)
                    {
                        item.GetType().GetMethod(upgradeMethod).Invoke(item, null);
                    }
                }
            }
            initialUpgrades = false;
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

    private Tuple<string, string> SplitClassMethodString(string classAndMethod)
    {
        string[] parts = classAndMethod.Split(',');
        string className = parts[0];
        string methodName = parts[1];
        return Tuple.Create(className, methodName);
    }    
    public string GetUpgradeCost(string classAndMethod)
    {
        (string className, string methodName) = SplitClassMethodString(classAndMethod);
        Debug.Log(className + " " + methodName);
        return costMap[Tuple.Create(className, methodName)].ToString();
    }
    
    public void UpgradeRank(string classAndMethod)
    {
        (string className, string methodName) = SplitClassMethodString(classAndMethod);
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

    

    private bool CheckIfSufficientCurrency(int price)
    {
        if(currency >= price){
            currency -= price;
            UpdateMoney();
            return true;
        }
        return false;
    }

    private void UpdateMoney(){
        //MoneyText1.SetText(currency.ToString());
        //MoneyText2.SetText(currency.ToString());
    }

    public void AddCurrency(int addedCurrency){
        currency += addedCurrency;
    }

    public List<Item> GetItems() 
    { 
        return items;
    }

    
}
