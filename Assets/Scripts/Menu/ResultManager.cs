using TMPro;
using UnityEditor.SearchService;
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
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void DestroyInstance()
    {
        Destroy(gameObject);
        Instance = null;
    }

    public void CompileText() {
        moneyEarnedText.text = "" + moneyEarned;
        levelText.text = "" + mainLevel;
        timerText.text = "" + timeText;
        enemiesDefeatedText.text = "" + enemiesDefeated;
    }
   

}
