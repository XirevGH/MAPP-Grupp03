using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class Purse : Pickup
{
    public AudioClip SFX;
    public GameObject confettiLeft, confettiRight, confettiCenter;
    private UpgradeSystem upgradeAbility;
    private Item item;
    private string upgradeText = "";
    private int moneyAmount;
    private GameObject panelGameObject;
    PursePanel panel;
    GameController gameController;
    private void Start()
    {
        upgradeAbility = GameObject.FindGameObjectWithTag("UpgradeSystem").GetComponent<UpgradeSystem>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        moneyAmount = GiveRandomAmountOfMoney();
        panelGameObject = gameController.pursePanel;
        panel = FindObjectOfType<PursePanel>(true);

    }

    protected override void IndividualPickupAction()
    {
        panel.OpenPurseWindow();
        GameObject confettiLeftClone = Instantiate(confettiLeft, gameController.canvasWorldSpace.transform);
        GameObject confettiRightClone = Instantiate(confettiRight, gameController.canvasWorldSpace.transform);
        GameObject confettiCenterClone = Instantiate(confettiCenter, gameController.canvasWorldSpace.transform);

        var mainModuleLeft = confettiLeftClone.GetComponent<ParticleSystem>().main;
        mainModuleLeft.useUnscaledTime = true;

        var mainModuleRight = confettiRightClone.GetComponent<ParticleSystem>().main;
        mainModuleRight.useUnscaledTime = true;

        var mainModuleCenter = confettiCenterClone.GetComponent<ParticleSystem>().main;
        mainModuleCenter.useUnscaledTime = true;

        SoundManager.Instance.PlaySFX(SFX, 1);
        player.currency += moneyAmount;
        UpgradeRandomItem();
        upgradeAbility.SetPanelContent(panelGameObject, item, upgradeAbility.GetUpgradeDescription(item, "Upgrade", upgradeText), "Upgrade");
        
    }

    private void UpgradeRandomItem()
    {
        List<Item> currentPlayerItems = upgradeAbility.GetItems();
        upgradeAbility.InitializeUpgradeOptions(currentPlayerItems);
        (item, upgradeText) = upgradeAbility.ChooseRandomUpgrade();
        upgradeAbility.PerformRandomizedUpgrade(item, upgradeText);
    }

    private int GiveRandomAmountOfMoney()
    {
        int money = UnityEngine.Random.Range(50, 501);
        return money;
    }

    protected override void ResetThis()
    {   upgradeText = "";
        moneyAmount = GiveRandomAmountOfMoney();
    }
    

}
