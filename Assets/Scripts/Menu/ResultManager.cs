using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class ResultManager : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText; 
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text enemiesDefeatedText;
    [SerializeField] private TMP_Text moneyEarnedText;
    [SerializeField] private Image[] iconPictures;

    public static ResultManager Instance;

    public string timeText;
    public int mainLevel;
    public int enemiesDefeated;
    public int moneyEarned;

    public List<Item> currentItems;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Main") 
        { 
            if (Instance != this && Instance != null)
            {
                Destroy(Instance.gameObject);
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Instance.levelText = this.levelText;
            Instance.timerText = this.timerText;
            Instance.enemiesDefeatedText = this.enemiesDefeatedText;
            Instance.moneyEarnedText = this.moneyEarnedText;
            Instance.iconPictures = this.iconPictures;
            Instance.SetImages();
            Instance.CompileText();
            Destroy(this);
        }
    }

    private void CompileText()
    {
        moneyEarnedText.text = "" + moneyEarned;
        levelText.text = "" + mainLevel;
        timerText.text = "" + timeText;
        enemiesDefeatedText.text = "" + enemiesDefeated;
    }

        private void SetImages()
    {
        int itemAmount = currentItems.Count;
        for(int i = 0; i < iconPictures.Length; i++)
        {
            if (itemAmount > 0)
            {
                Debug.Log("Tried putting image");
                string itemName = String.Concat(currentItems[i].GetName().Where(c => !Char.IsWhiteSpace(c)));
                iconPictures[i].sprite = Resources.Load<Sprite>("Icons/" + itemName + "Pixel");
                iconPictures[i].color = new Color32(255, 255, 255, 255);
            }
            else
            {
                iconPictures[i].sprite = null;
                iconPictures[i].color = new Color32(255, 255, 255, 0);
            }
            itemAmount--;
        }
    } 
}

