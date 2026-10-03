using System.Collections.Generic;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    public static ResultManager Instance { get; private set; }

    // Match Stats
    public string timeText = "00:00";
    public int mainLevel = 1;
    public int enemiesDefeated = 0;
    public int moneyEarned = 0;

    // Saved Items for the Results Screen
    public List<ItemDefinitionSO> savedItemData = new List<ItemDefinitionSO>();
    public List<string> fallbackItemNames = new List<string>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ResetData()
    {
        timeText = "00:00";
        mainLevel = 1;
        enemiesDefeated = 0;
        moneyEarned = 0;
        savedItemData.Clear();
        fallbackItemNames.Clear();
    }
}