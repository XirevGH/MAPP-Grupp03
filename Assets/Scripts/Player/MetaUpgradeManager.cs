using System;
using System.IO;
using UnityEngine;

public class MetaUpgradeManager : MonoBehaviour
{
    public static MetaUpgradeManager Instance { get; private set; }

    private MetaSaveData saveData = new MetaSaveData();
    private string saveFilePath;

    public event Action<UpgradeDefinitionSO, int> OnUpgradePurchased;
    public event Action<int> OnCurrencyChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        saveFilePath = Path.Combine(Application.persistentDataPath, "meta_upgrades.json");
        LoadData();
    }

    public int GetUpgradeRank(UpgradeDefinitionSO upgrade)
    {
        return saveData.GetRank(upgrade.UpgradeID);
    }

    public float GetStatValue(UpgradeDefinitionSO upgrade)
    {
        int rank = GetUpgradeRank(upgrade);
        return upgrade.GetCalculatedValue(rank);
    }

    public bool CanAfford(UpgradeDefinitionSO upgrade)
    {
        int currentRank = GetUpgradeRank(upgrade);
        if (currentRank >= upgrade.MaxRank) return false;

        int cost = upgrade.GetCostForRank(currentRank);
        return saveData.currency >= cost;
    }

    public bool TryPurchaseUpgrade(UpgradeDefinitionSO upgrade)
    {
        int currentRank = GetUpgradeRank(upgrade);
        int cost = upgrade.GetCostForRank(currentRank);

        if (currentRank >= upgrade.MaxRank || saveData.currency < cost)
        {
            return false;
        }

        saveData.currency -= cost;
        int newRank = currentRank + 1;
        saveData.SetRank(upgrade.UpgradeID, newRank);

        SaveData();

        OnCurrencyChanged?.Invoke(saveData.currency);
        OnUpgradePurchased?.Invoke(upgrade, newRank);
        return true;
    }

    public void AddCurrency(int amount)
    {
        saveData.currency += amount;
        SaveData();
        OnCurrencyChanged?.Invoke(saveData.currency);
    }

    public int GetCurrency()
    {
        return saveData != null ? saveData.currency : 0;
    }

    private void SaveData()
    {
        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(saveFilePath, json);
    }

    private void LoadData()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            saveData = JsonUtility.FromJson<MetaSaveData>(json);
        }
    }
}