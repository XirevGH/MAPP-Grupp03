using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text enemiesDefeatedText;
    [SerializeField] private TMP_Text moneyEarnedText;

    public static ResultManager Instance;

    public string timeText;
    public int mainLevel;
    public int enemiesDefeated;
    public int moneyEarned;

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
    }
}
