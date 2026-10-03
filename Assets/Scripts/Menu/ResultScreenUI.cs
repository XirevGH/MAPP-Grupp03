using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultScreenUI : MonoBehaviour
{
    [Header("UI Text References")]
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text enemiesDefeatedText;
    [SerializeField] private TMP_Text moneyEarnedText;

    [Header("Inventory Icons")]
    [SerializeField] private Image[] iconPictures;

    private void Start()
    {
        DisplayResults();
    }

    private void DisplayResults()
    {
        if (ResultManager.Instance == null)
        {
            Debug.LogWarning("[ResultScreenUI] No ResultManager instance found!");
            return;
        }

        var data = ResultManager.Instance;

        // 1. Populate Text Fields
        if (timerText != null) timerText.text = data.timeText;
        if (levelText != null) levelText.text = data.mainLevel.ToString();
        if (enemiesDefeatedText != null) enemiesDefeatedText.text = data.enemiesDefeated.ToString();
        if (moneyEarnedText != null) moneyEarnedText.text = data.moneyEarned.ToString();

        // 2. Populate Inventory Icons
        PopulateIcons(data.savedItemData, data.fallbackItemNames);
    }

    private void PopulateIcons(List<ItemDefinitionSO> itemDataList, List<string> nameList)
    {
        if (iconPictures == null) return;

        int totalItems = Mathf.Max(itemDataList.Count, nameList.Count);

        for (int i = 0; i < iconPictures.Length; i++)
        {
            if (i < totalItems)
            {
                Sprite iconSprite = null;

                // 1. Try from ScriptableObject
                if (i < itemDataList.Count && itemDataList[i] != null && itemDataList[i].Icon != null)
                {
                    iconSprite = itemDataList[i].Icon;
                }

                // 2. Fallback: Resources/Icons/
                if (iconSprite == null && i < nameList.Count)
                {
                    string cleanName = String.Concat(nameList[i].Where(c => !Char.IsWhiteSpace(c)));
                    iconSprite = Resources.Load<Sprite>("Icons/" + cleanName + "Pixel");
                }

                if (iconSprite != null)
                {
                    iconPictures[i].sprite = iconSprite;
                    iconPictures[i].color = Color.white;
                }
                else
                {
                    iconPictures[i].sprite = null;
                    iconPictures[i].color = Color.clear;
                }
            }
            else
            {
                iconPictures[i].sprite = null;
                iconPictures[i].color = Color.clear;
            }
        }
    }
}