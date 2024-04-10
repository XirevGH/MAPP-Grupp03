using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ManuController : MonoBehaviour
{

    [SerializeField] private GameObject creditPanel;
    [SerializeField] private GameObject settingPanel;
  

    [SerializeField] private int levelToload;

    private void Start()
    {
       CloseCredits();
        CloseSetting();
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
        SceneManager.LoadScene(levelToload);
    }

    public void ShowSetting()
    {
        settingPanel.SetActive(true);

    }
    public void CloseSetting()
    {
        settingPanel.SetActive(false);

    }


    public void Endgame()

    {
        Application.Quit();
    }



}
