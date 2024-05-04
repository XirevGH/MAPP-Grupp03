using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainManager : MonoBehaviour
{

    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text enemiesDefeatedText;

    public static MainManager Instance;

    public string timeText = "";
    public int mainLevel = 1;
    public int enemiesDefeated;

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
        levelText.text = "" + mainLevel;
        timerText.text = "" + timeText;
        enemiesDefeatedText.text = "" + enemiesDefeated;
    }

}
