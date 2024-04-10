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
       CloseAbout();
        Closesetting();
    }

    public void Startgame()
    {
        ChangeScene();
    }

    public void ShowAbout()
    {
        creditPanel.SetActive(true);

    }

    public void CloseAbout()
    {
        creditPanel.SetActive(false);

    }


    public void ChangeScene()
    {
        SceneManager.LoadScene(levelToload);
    }

    public void Showsetting()
    {
        settingPanel.SetActive(true);

    }
    public void Closesetting()
    {
        settingPanel.SetActive(false);

    }


    public void Endgame()

    {
        Application.Quit();
    }



}
