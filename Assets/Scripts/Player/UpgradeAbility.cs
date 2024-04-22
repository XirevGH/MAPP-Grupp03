using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class UpgradeAbility : MonoBehaviour
{
    [SerializeField] private Weapon[] weapons;
    [SerializeField] private Player player;
    private List<Weapon> weaponOptions;
    private List<string> utilityOptions;
    private Dictionary<Weapon, string> upgradeOptions;
    private List<string> typeOptions;
    private int allowedAmountOfWeapons;

    void Start()
    {
        weaponOptions = new List<Weapon>(weapons);
        allowedAmountOfWeapons = 4;
        typeOptions = new List<string> { "Weapon", "Utility"};

    }

    public void UpgradeDamage(Weapon weapon, float percentageIncrease)
    {
        weapon.IncreaseDamage(percentageIncrease);
    }
    public void UpgradeTargetCount(PenetratingProjectileWeapon weapon, int targetIncrease)
    {
        weapon.IncreaseTargetCount(targetIncrease);
    }

    public void UpgradePenetrationAmount(PenetratingProjectileWeapon weapon, int penetrationAmount)
    {
        weapon.IncreasePenetrationAmount(penetrationAmount);
    }

    public Weapon ChooseRandomWeapon()
    {
        int randomWeapon = Random.Range(0, weaponOptions.Count);
        return weaponOptions[randomWeapon];
    }

    public void ChooseRandomUpgrade()
    {
        Weapon[] currentPlayerWeapons = player.GetCurrentWeapons().ToArray();
        

    }
    public List<Weapon> GetAvailableWeapons()
    {
        return new List<Weapon>(weaponOptions);
    }

    public void ChooseWeapon(Weapon weapon)
    {
        weaponOptions.Remove(weapon);
    }

    public void RandomUpgrade()
    {
        
    }

    private void ExhaustOption(string option) 
    { 
        typeOptions.Remove(option);
    }
    private void ChooseOptions()
    {
        if (player.GetCurrentWeapons().Count == 0) 
        { 

        }
        if (player.GetCurrentWeapons().Count < allowedAmountOfWeapons) { 
            int randomChoiceOne = Random.Range(0, typeOptions.Count);
            int randomChoiceTwo = Random.Range(0, typeOptions.Count);
            int randomChoiceThree = Random.Range(0, typeOptions.Count);
        }
    }

    private string ChooseUpgradeType(int randomChoice)
    {
        return typeOptions[randomChoice];
    }

    private void CheckForUpgradableItems()
    {
    }
}
