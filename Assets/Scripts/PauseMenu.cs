using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

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
        BeatSpawnerController.GetComponent<BeatSpawnerController>().ToggleBeatSpawn();
        soundManager.GetComponent<SoundManager>().ToggleMusicPause();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void PauseGame()
    {
        BeatSpawnerController.GetComponent<BeatSpawnerController>().ToggleBeatSpawn();
        soundManager.GetComponent<SoundManager>().ToggleMusicPause();
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        soundManager.GetComponent<SoundManager>().GoBackToMain();
        GameIsPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName); 
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
