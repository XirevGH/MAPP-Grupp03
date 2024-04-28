using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject[] joysticks;
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI, BeatSpawnerController;
    public GameObject pauseButton; 

    [SerializeField] private GameObject settingPanel;
    private AudioSource audioSource;
    private AudioClip clickSound;



    public string mainMenuSceneName = "MainMenu";

    private void Start()
    {
       
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
        BeatSpawnerController.GetComponent<TriggerController>().ToggleTrigger();
        SoundManager.instance.GetComponent<SoundManager>().ToggleMusicPause();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void PauseGame()
    {
        ToggleJoysticks(false);
        BeatSpawnerController.GetComponent<TriggerController>().ToggleTrigger();
        SoundManager.instance.GetComponent<SoundManager>().ToggleMusicPause();
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        audioSource.PlayOneShot(clickSound);
    }

    public void LoadMenu()
    {
        SoundManager.instance.GetComponent<SoundManager>().GoBackToMain();
        GameIsPaused = false;
        ToggleJoysticks(true);
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName); 
    }

    public void Settings()
    {
        Debug.Log("Settings meny");
        settingPanel.SetActive(true);
        //audioSource.PlayOneShot(clickSound);
    }

    public void CloseSetting()
    {
        settingPanel.SetActive(false);
        //audioSource.PlayOneShot(clickSound);

    }

    private void ToggleJoysticks(bool state)
    {
        foreach (GameObject joystick in joysticks)
        {
            joystick.SetActive(state);
        }
    }
}
