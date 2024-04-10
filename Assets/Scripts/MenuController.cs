using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ManuController : MonoBehaviour
{
    [SerializeField] private GameObject exit;
    [SerializeField] private GameObject play;
    [SerializeField] private GameObject uppfrades;
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject creditPanel;
  

    [SerializeField] private int levelToload;

    private void Start()
    {
        CloseCredits();
        CloseSetting();
        CloseUppgrades();
    }

    public void Startgame()
    {
        ChangeScene();
    }

    public void ShowCredits()
    {
        creditPanel.SetActive(true);

    }

    public void CloseCredits()
    {
        creditPanel.SetActive(false);

    }

    


    public void ChangeScene()
    {
        SceneManager.LoadScene(levelToload); // by till spel scennens
    }

    public void ShowSetting()
    {
        settingPanel.SetActive(true);

    }
    public void CloseSetting()
    {
        settingPanel.SetActive(false);

    }

    public void ShowUppgrades()
    {
        settingPanel.SetActive(true);

    }

    public void CloseUppgrades()
    {
        uppfrades.SetActive(false);

    }

    


    public void Endgame()

    {
        Application.Quit();
    }



}
