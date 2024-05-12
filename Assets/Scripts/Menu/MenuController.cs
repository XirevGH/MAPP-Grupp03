using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class MenuController : MonoBehaviour
{
    [SerializeField] private SceneTransition transition;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject play;
    [SerializeField] private GameObject upgradesPanel;
    [SerializeField] private GameObject itemsPanel;
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject creditPanel;

    public void Startgame()
    {
        transition.StartGame();
        SoundManager.Instance.GetComponent<SoundManager>().StartGame();
        SoundManager.Instance.GetComponent<SoundManager>().Click();
    }

    public void ShowCredits()
    {
        creditPanel.SetActive(true);
        mainMenu.GetComponent<TweenUI>().OnClose();
        SoundManager.Instance.GetComponent<SoundManager>().Click(); 
    }

    public void CloseCredits()
    {
        creditPanel.GetComponent<TweenUI>().OnClose();
        mainMenu.SetActive(true);
        SoundManager.Instance.GetComponent<SoundManager>().Click();
    }

    public void ShowSetting()
    {
        settingPanel.SetActive(true);
        mainMenu.GetComponent<TweenUI>().OnClose();
        SoundManager.Instance.GetComponent<SoundManager>().Click();

    }
    public void CloseSetting()
    {
        settingPanel.GetComponent<TweenUI>().OnClose();
        mainMenu.SetActive(true);
        SoundManager.Instance.GetComponent<SoundManager>().Click();
    }

    public void CloseItems()
    {
        itemsPanel.GetComponent<TweenUI>().OnClose();
        mainMenu.SetActive(true);
        SoundManager.Instance.GetComponent<SoundManager>().Click();
    }

    public void ShowItems()
    {
        itemsPanel.SetActive(true);
        mainMenu.GetComponent<TweenUI>().OnClose();
        SoundManager.Instance.GetComponent<SoundManager>().Click();
    }
    public void ShowUpgrades()
    {
        upgradesPanel.SetActive(true);
        itemsPanel.GetComponent<TweenUI>().OnClose();
        SoundManager.Instance.GetComponent<SoundManager>().Click();
    }

    public void CloseUpgrades()
    {
        upgradesPanel.GetComponent<TweenScroll>().OnClose();
        itemsPanel.SetActive(true);
        SoundManager.Instance.GetComponent<SoundManager>().Click();
    }
}
