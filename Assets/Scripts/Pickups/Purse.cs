using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class Purse : MonoBehaviour
{
    private UpgradeAbility upgradeAbility;
    private Item item;
    private string upgradeText = "";
    private int moneyAmount;
    public GameObject text;

    private void Start()
    {
        upgradeAbility = GameObject.FindGameObjectWithTag("UpgradeSystem").GetComponent<UpgradeAbility>();
        moneyAmount = GiveRandomAmountOfMoney();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStats>().money += moneyAmount;
            //UpgradeRandomItem();
            Debug.Log(item + upgradeText);
            GameObject textClone = Instantiate(text, new Vector3(transform.position.x, transform.position.y +2, -0.5f), Quaternion.identity, transform);
            textClone.GetComponent<TextMesh>().text = GetTestPopup();
        }
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
        upgradeAbility.InitializeUpgradeOptions(currentPlayerItems);
        (item, upgradeText) = upgradeAbility.ChooseRandomUpgrade();
        upgradeAbility.PerformRandomizedUpgrade(item, upgradeText);
    }

    private int GiveRandomAmountOfMoney()
    {
        int money = UnityEngine.Random.Range(50, 501);
        return money;
    }

  
   

}
