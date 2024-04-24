using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject[] joysticks;
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI, BeatSpawnerController, soundManager;

    public string mainMenuSceneName = "MainMenu";

    private void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("SoundManager");
    }

    void Update()
    {
        if (Input.GetButtonDown("PauseButton"))
        {
            PauseButton();
        }
    }

    public void UpdatePauseButtonPosition(Vector3 newPosition)
    {
        pauseMenuUI.transform.localPosition = newPosition;
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
        BeatSpawnerController.GetComponent<TriggerController>().ToggleTrigger();
        soundManager.GetComponent<SoundManager>().ToggleMusicPause();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void PauseGame()
    {
        ToggleJoysticks(false);
        BeatSpawnerController.GetComponent<TriggerController>().ToggleTrigger();
        soundManager.GetComponent<SoundManager>().ToggleMusicPause();
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        soundManager.GetComponent<SoundManager>().GoBackToMain();
        GameIsPaused = false;
        ToggleJoysticks(true);
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName); 
    }

    public void Settings()
    {
        Debug.Log("Settings meny");
    }

    private void ToggleJoysticks(bool state)
    {
        foreach (GameObject joystick in joysticks)
        {
            joystick.SetActive(state);
        }
    }
}
