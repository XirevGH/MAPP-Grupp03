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
  //  [SerializeField] private AudioSource audioSource;
  //  [SerializeField] private AudioClip clickSound;



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
        TriggerController.Instance.GetComponent<TriggerController>().ToggleTrigger();
        SoundManager.Instance.GetComponent<SoundManager>().ToggleMusicPause();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

    }
    void PauseGame()
    {
        ToggleJoysticks(false);
        TriggerController.Instance.GetComponent<TriggerController>().ToggleTrigger();
        SoundManager.Instance.GetComponent<SoundManager>().ToggleMusicPause();
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
       // audioSource.PlayOneShot(clickSound);
    }

    public void LoadMenu()
    {
        SoundManager.Instance.GetComponent<SoundManager>().GoBackToMain();
        GameIsPaused = false;
        ToggleJoysticks(true);
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName); 
    }

    public void Settings()
    {
        Debug.Log("Settings meny");
        settingPanel.SetActive(true);
    
    }

    public void CloseSetting()
    {
        settingPanel.SetActive(false);
      //  audioSource.PlayOneShot(clickSound);

    }

    private void ToggleJoysticks(bool state)
    {
        foreach (GameObject joystick in joysticks)
        {
            joystick.SetActive(state);
        }
    }
}
