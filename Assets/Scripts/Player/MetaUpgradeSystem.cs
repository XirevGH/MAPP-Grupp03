using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class MetaUpgradeSystem : MonoBehaviour
{
    [SerializeField] private int currency;

    private static bool initialUpgrades = true;

    private Item[] items;
    private Dictionary<Tuple<string, string>, int> upgradeMap;
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

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
            initialUpgrades = true;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        items = FindObjectsOfType<Item>();
        upgradeStatsFile = Application.persistentDataPath + "/upgradeInfo.json";
        ReadFile(upgradeStatsFile);
        Debug.Log("Start");
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
        SaveFile();
    }

    private void SaveFile()
    {
        File.WriteAllText(upgradeStatsFile, SaveToString());
    }

    public void AddCurrency(int addedCurrency){
        currency += addedCurrency;
        SaveFile();
    }

    public int GetCurrencyAmount()
    {
        return currency;
    }

    public void DeductCurrency(int cost)
    {
        currency -= cost;
        CurrencyTextHandler.Instance.UpdateCurrency();
        SaveFile();
    }

    public Item[] GetItems() 
    { 
        return items;
    }
}
