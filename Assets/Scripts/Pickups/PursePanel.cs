using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PursePanel : MonoBehaviour
{
    private UpgradeSystem upgradeAbility;
    private Item item;
    private string upgradeText = "";
    private int moneyAmount;
    public Sprite weaponPanel;
    public Sprite utilityPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //SetPanel();
    }
    private void SetPanel()
    {

        GetComponentInChildren<TMP_Text>().text =  upgradeText;

    }


    public void SetAmountOfMoney(int amouth)
    {
        moneyAmount = amouth;
    }

    public void SetUpgradeText(String text)
    {
        upgradeText = text;
    }

    public void OpenPurseWindow()
    {
        Time.timeScale = 0f;
        gameObject.gameObject.SetActive(true);
        TriggerController.Instance.ToggleTrigger();
        SoundManager.Instance.ToggleMusicPause();
    }

    public void ClosePurseWindow()
    {
        Time.timeScale = 1f;
        gameObject.gameObject.SetActive(false);
        TriggerController.Instance.ToggleTrigger();
        SoundManager.Instance.ToggleMusicPause();
    }

  
}
