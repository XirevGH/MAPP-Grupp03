using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainManager : MonoBehaviour
{

    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text enemiesDefeatedText;
    [SerializeField] private TMP_Text moneyEarnedText;


    public static MainManager Instance;

    public string timeText = "";
    public int mainLevel = 1;
    public int enemiesDefeated;
    public int moneyEarned;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Update()
    {   
        moneyEarnedText.text = "" + moneyEarned;
        levelText.text = "" + mainLevel;
        timerText.text = "" + timeText;
        enemiesDefeatedText.text = "" + enemiesDefeated;
    }

}
