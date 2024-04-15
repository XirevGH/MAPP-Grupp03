using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // fixa en paus knapp
        {
            if (GameIsPaused)
            {
                Resume();
            }else
            {
                PauseGame();
            }
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
