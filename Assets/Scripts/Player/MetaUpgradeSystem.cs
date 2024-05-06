using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class MetaUpgradeSystem : MonoBehaviour
{
    [SerializeField] public int money;
    [SerializeField] private TextMeshProUGUI MoneyText1;
    [SerializeField] private TextMeshProUGUI MoneyText2;
    
    [SerializeField] private List<Item> items;

    
    

    private Dictionary<Tuple<string, string>, int> upgradeMap;

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
