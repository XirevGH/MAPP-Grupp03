using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ManuController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject exit;
    [SerializeField] private GameObject play;
    [SerializeField] private GameObject uppgradesPanel;
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject creditPanel;
    private GameObject soundManager;
  

    [SerializeField] private int levelToload;

    private void Start()
    {
        CloseCredits();
        CloseSetting();
        CloseUppgrades();
    }

    private void Update()
    {
        soundManager = GameObject.FindGameObjectWithTag("SoundManager");
    }

    public void Startgame()
    {
      
        ChangeScene();
    }

    public void ShowCredits()
    {
        creditPanel.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void CloseCredits()
    {
        creditPanel.SetActive(false);
        mainMenu.SetActive(true);

    }



    


    public void ChangeScene()
    {
        soundManager.GetComponent<SoundManager>().ToggleMusicPause();
        soundManager.GetComponent<SoundManager>().musicSource1.Play();
        SceneManager.LoadScene(levelToload); // by till spel scennens
    }

    public void ShowSetting()
    {
        settingPanel.SetActive(true);
        mainMenu.SetActive(false);
    }
    public void CloseSetting()
    {
        settingPanel.SetActive(false);
        mainMenu.SetActive(true);

    }

    public void ShowUppgrades()
    {
        uppgradesPanel.SetActive(true);
        mainMenu.SetActive(false);

    }

    public void CloseUppgrades()
    {
        uppgradesPanel.SetActive(false);
        mainMenu.SetActive(true);

    }

    


    public void Endgame()

    {
        Application.Quit();
    }



}
