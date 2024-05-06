using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    
    [SerializeField] private GameObject play;
    [SerializeField] private GameObject upgradesPanel;
    [SerializeField] private GameObject itemsPanel;
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject creditPanel;
    


    [SerializeField] private int levelToload;

 

    public void Startgame()
    {
      
        ChangeScene();
        SoundManager.Instance.GetComponent<SoundManager>().StartGame();
        SoundManager.Instance.GetComponent<SoundManager>().Click();
        MainManager.Instance.enemiesDefeated = 0;
        MainManager.Instance.mainLevel = 1;
    }

    public void ShowCredits()
    {
        creditPanel.SetActive(true);
        mainMenu.SetActive(false);
        SoundManager.Instance.GetComponent<SoundManager>().Click(); 
    }

    public void CloseCredits()
    {
        creditPanel.SetActive(false);
        mainMenu.SetActive(true);
        SoundManager.Instance.GetComponent<SoundManager>().Click();
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
        SoundManager.Instance.GetComponent<SoundManager>().Click();

    }
    public void CloseSetting()
    {
        settingPanel.SetActive(false);
        mainMenu.SetActive(true);
        SoundManager.Instance.GetComponent<SoundManager>().Click();
    }

    public void CloseItems()
    {
        itemsPanel.SetActive(false);
        mainMenu.SetActive(true);
        SoundManager.Instance.GetComponent<SoundManager>().Click();
    }

    public void ShowItems()
    {
        itemsPanel.SetActive(true);
        mainMenu.SetActive(false);
        SoundManager.Instance.GetComponent<SoundManager>().Click();
    }
    public void ShowUpgrades()
    {
        upgradesPanel.SetActive(true);
        itemsPanel.SetActive(false);
        SoundManager.Instance.GetComponent<SoundManager>().Click();
    }

    public void CloseUpgrades()
    {
        upgradesPanel.SetActive(false);
        itemsPanel.SetActive(true);
        SoundManager.Instance.GetComponent<SoundManager>().Click();
    }

   



}
