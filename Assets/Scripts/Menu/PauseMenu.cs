using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject[] joysticks;
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject pauseButton; 

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
        ToggleJoysticks(true);
        TriggerController.Instance.GetComponent<TriggerController>().ToggleTrigger();
        SoundManager.Instance.GetComponent<SoundManager>().ToggleMusicPause();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        SoundManager.Instance.GetComponent<SoundManager>().Click();

    }
    void PauseGame()
    {
        ToggleJoysticks(false);
        TriggerController.Instance.GetComponent<TriggerController>().ToggleTrigger();
        SoundManager.Instance.GetComponent<SoundManager>().ToggleMusicPause();
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        SoundManager.Instance.GetComponent<SoundManager>().Click();
    }

    public void LoadMenu()
    {
        SoundManager.Instance.GetComponent<SoundManager>().GoBackToMain();
        GameIsPaused = false;
        ToggleJoysticks(true);
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
        SoundManager.Instance.GetComponent<SoundManager>().Click();
    }

    public void Settings()
    {
        Debug.Log("Settings meny");
        settingPanel.SetActive(true);
        SoundManager.Instance.GetComponent<SoundManager>().Click();
    }

    public void CloseSetting()
    {
        settingPanel.SetActive(false);
        SoundManager.Instance.GetComponent<SoundManager>().Click();

    }

    private void ToggleJoysticks(bool state)
    {
        foreach (GameObject joystick in joysticks)
        {
            joystick.SetActive(state);
        }
    }
}
