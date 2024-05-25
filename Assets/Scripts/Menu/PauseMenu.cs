using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject pauseButton;
    public GameObject upgradeScreen;

    [SerializeField] private GameObject settingPanel;

    public string mainMenuSceneName = "MainMenu";


    void Update()
    {
        if (Input.GetButtonDown("PauseButton"))
        {
            PauseButton();
        }
    }

    public void UpdatePauseButtonPosition(Vector3 newPosition)
    {
        pauseButton.transform.localPosition = newPosition;

    }

    public void PauseButton()
    {

        if (GameIsPaused)
        {
            Resume();
        }
        else
        {
            PauseGame();
        }
    }
    public void Resume()
    {
        JoystickController.Instance.ToggleJoysticks(true);
        TriggerController.Instance.ToggleTrigger(true);
        SoundManager.Instance.ToggleMusicPause(true);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        SoundManager.Instance.Click();

    }
    void PauseGame()
    {

        if (upgradeScreen.activeInHierarchy == false) { 
            JoystickController.Instance.ToggleJoysticks(false);
            TriggerController.Instance.ToggleTrigger(false);
            SoundManager.Instance.ToggleMusicPause(false);
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
            SoundManager.Instance.Click();
        }
    }

    public void LoadMenu()
    {
        GameIsPaused = false;
        JoystickController.Instance.ToggleJoysticks(true);
        Time.timeScale = 1f;
        SoundManager.Instance.Click();
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void Settings()
    {
        Debug.Log("Settings meny");
        settingPanel.SetActive(true);
        SoundManager.Instance.Click();
    }

    public void CloseSetting()
    {
        settingPanel.SetActive(false);
        SoundManager.Instance.Click();

    }
}
