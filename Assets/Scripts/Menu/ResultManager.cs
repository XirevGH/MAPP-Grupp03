using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text enemiesDefeatedText;
    [SerializeField] private TMP_Text moneyEarnedText;
    [SerializeField] private TMP_Text playerItemsText;

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
            Destroy(this);
            Instance.CompileText();
        }
    }

    public void CompileText() {
        moneyEarnedText.text = "" + moneyEarned;
        levelText.text = "" + mainLevel;
        timerText.text = "" + timeText;
        enemiesDefeatedText.text = "" + enemiesDefeated;
          
    StringBuilder playerItemsBuilder = new StringBuilder();
        for(int i = 0; i<currentItems.Count; i++)
        {
            playerItemsBuilder.Append(currentItems[i].name);
            if(i<currentItems.Count - 1)
            {
                playerItemsBuilder.Append(", ");
            }
 
        }
        playerItemsText.text = playerItemsBuilder.ToString();
    }
}

