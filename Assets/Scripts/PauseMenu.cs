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
        if (Input.GetKeyDown(KeyCode.Escape)) // fixa en paus knapp
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

        //fixa så enemies, player och animationer pausas också

        //SoundManager.Instance.Pause();
    }

    public void LoadMenu()
    {
        soundManager.GetComponent<SoundManager>().ToggleMusicPause();
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); // fixa så det inte är hard coded in - gör en variabel
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }
}
