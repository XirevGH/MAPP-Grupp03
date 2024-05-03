using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
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
    [SerializeField] private GameObject upgradeControler;
    

    
    
   //  [SerializeField] private AudioSource audioSource;
   // [SerializeField] private AudioClip clickSound;


    [SerializeField] private int levelToload;

    private void Start()
    {   
        GlobalUpgrades.Instance.LoadFromPlayerPrefs();
        upgradeControler.GetComponent<UpgradeController>().levels = GlobalUpgrades.Instance.upgradeStats.savedLevels;
        upgradeControler.GetComponent<UpgradeController>().money = GlobalUpgrades.Instance.upgradeStats.savedMoney;
       
        CloseCredits();
        CloseSetting();
        CloseUppgrades();

    }

    private void Update()
    {
        
    }

    public void Startgame()
    {
        
        
        
        ChangeScene();
        SoundManager.instance.GetComponent<SoundManager>().StartGame();
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
        upgradeControler.GetComponent<UpgradeController>().SetStats();
        GlobalUpgrades.Instance.SaveToPlayerPrefs();
        
        
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
        
        //UpgradeController.Instance.SaveToPlayerPrefs();
        
        Application.Quit();
       
    }



}
