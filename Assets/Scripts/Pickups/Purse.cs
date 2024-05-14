using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class Purse : Pickup
{
    private UpgradeSystem upgradeAbility;
    private Item item;
    private string upgradeText = "";
    private int moneyAmount;
    public GameObject text;
    private PursePanel panel;

    private void Start()
    {
        upgradeAbility = GameObject.FindGameObjectWithTag("UpgradeSystem").GetComponent<UpgradeSystem>();
        moneyAmount = GiveRandomAmountOfMoney();
        panel = FindObjectOfType<PursePanel>(true);
    }
    protected override void IndividualPickupAction()
    {
        player.currency += moneyAmount;
        UpgradeRandomItem();
        Debug.Log(item + upgradeText);
        GameObject textClone = Instantiate(text, new Vector3(transform.position.x, transform.position.y + 2, -0.5f), Quaternion.identity);
        //panel.OpenPurseWindow();
        //panel.SetAmountOfMoney(moneyAmount);
        //panel.SetUpgradeText(upgradeText);
        textClone.GetComponent<TextMesh>().text = GetTestPopup();
    }
    private String GetTestPopup()
    {
        StringBuilder stringBuilder = new StringBuilder();

        stringBuilder.AppendFormat("you get {0} coins", moneyAmount);
        stringBuilder.AppendLine();
        stringBuilder.AppendFormat("And a {0}" , upgradeText);

        return stringBuilder.ToString();

    }

    private void UpgradeRandomItem()
    {
        List<Item> currentPlayerItems = upgradeAbility.GetItems();
        Debug.Log(currentPlayerItems);
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
