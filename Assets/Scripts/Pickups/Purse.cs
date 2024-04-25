using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purse : MonoBehaviour
{
    [SerializeField] private UpgradeAbility upgradeAbility;
    private Item item;
    private string upgradeText;


    private void Start()
    {
        upgradeAbility = GameObject.FindGameObjectWithTag("UpgradeSystem").GetComponent<UpgradeAbility>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStats>().money += GiveRandomAmountOfMoney();
            UpgradeRandomItem();

        }
    }

    private void UpgradeRandomItem()
    {
        upgradeAbility.InitializeUpgradeOptions();
        (item, upgradeText) = upgradeAbility.ChooseRandomUpgrade();
        upgradeAbility.PerformRandomizedUpgrade(item, upgradeText);
    }

    private int GiveRandomAmountOfMoney()
    {
        int money = UnityEngine.Random.Range(50, 501);
        return money;
    }

  
   

}
