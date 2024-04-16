using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI, BeatSpawnerController, soundManager;

    private void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("SoundManager");
    }

    void Update()
    {
        // Check if the game should be paused based on button input
        if (Input.GetButtonDown("PauseButton"))
        {
            PauseButton();
        }
    }

    public void PauseButton()
    {
        // Toggle beat spawner and music pause
        BeatSpawnerController.GetComponent<BeatSpawnerController>().ToggleBeatSpawn();
        soundManager.GetComponent<SoundManager>().ToggleMusicPause();

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
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        //fixa så enemies, player och animationer pausas också

        //SoundManager.Instance.Pause();
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); // fixa så det inte är hard coded in - gör en variabel
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }
}
