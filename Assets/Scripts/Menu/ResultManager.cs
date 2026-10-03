using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text enemiesDefeatedText;
    [SerializeField] private TMP_Text moneyEarnedText;
    [SerializeField] private Image[] iconPictures;

    public string timeText;
    public int mainLevel;
    public int enemiesDefeated;
    public int moneyEarned;
    public List<Item> currentItems;

    public static ResultManager Instance;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Main")
        {
            if (Instance != null && Instance != this)
            {
                Destroy(Instance.gameObject);
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Binds UI references from the GameOver scene onto the persistent Instance data
            Instance.levelText = this.levelText;
            Instance.timerText = this.timerText;
            Instance.enemiesDefeatedText = this.enemiesDefeatedText;
            Instance.moneyEarnedText = this.moneyEarnedText;
            Instance.iconPictures = this.iconPictures;

            Instance.SetImages();
            Instance.CompileText();
            Destroy(gameObject);
        }
    }

    private void CompileText()
    {
        if (moneyEarnedText != null) moneyEarnedText.text = moneyEarned.ToString();
        if (levelText != null) levelText.text = mainLevel.ToString();
        if (timerText != null) timerText.text = timeText;
        if (enemiesDefeatedText != null) enemiesDefeatedText.text = enemiesDefeated.ToString();
    }

    private void SetImages()
    {
        if (iconPictures == null || currentItems == null) return;

        for (int i = 0; i < iconPictures.Length; i++)
        {
            if (i < currentItems.Count && currentItems[i] != null && currentItems[i].BaseItemData != null)
            {
                iconPictures[i].sprite = currentItems[i].BaseItemData.Icon;
                iconPictures[i].color = Color.white;
            }
            else
            {
                iconPictures[i].sprite = null;
                iconPictures[i].color = new Color(1f, 1f, 1f, 0f);
            }
        }
    }
}