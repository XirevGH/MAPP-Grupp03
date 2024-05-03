using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject exit;
    [SerializeField] private GameObject play;
    [SerializeField] private GameObject upgradesPanel;
    [SerializeField] private GameObject itemsPanel;
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject creditPanel;
   //  [SerializeField] private AudioSource audioSource;
   // [SerializeField] private AudioClip clickSound;


    [SerializeField] private int levelToload;

    private void Start()
    {
        CloseCredits();
        CloseSetting();
        CloseUpgrades();
        CloseItems();

    }

    public void Startgame()
    {
      
        ChangeScene();
        SoundManager.Instance.GetComponent<SoundManager>().StartGame();
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
        //soundManager.GetComponent<SoundManager>().ToggleMusicPause();
        //soundManager.GetComponent<SoundManager>().musicSource1.Play();
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

    public void CloseItems()
    {
        itemsPanel.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void ShowItems()
    {
        itemsPanel.SetActive(true);
        mainMenu.SetActive(false);
    }
    public void ShowUpgrades()
    {
        upgradesPanel.SetActive(true);
        itemsPanel.SetActive(false);
     
    }

    public void CloseUpgrades()
    {
        upgradesPanel.SetActive(false);
        itemsPanel.SetActive(true);
      
    }

    


    public void Endgame()

    {
        Application.Quit();
       
    }



}
