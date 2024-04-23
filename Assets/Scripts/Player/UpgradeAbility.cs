using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class UpgradeAbility : MonoBehaviour
{
    [SerializeField] private Weapon[] weapons;
    [SerializeField] private UtilityItem[] utilities;
    [SerializeField] private Player player;
    [SerializeField] private PlayerMovement playerMovement;
    
    private List<Weapon> weaponOptions;
    
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

    public void UpgradeProjectileCount(ProjectileWeapon weapon, int targetIncrease)

    {
        weapon.IncreaseProjectileCount(targetIncrease);
    }

    public void UpgradePenetrationAmount(ProjectileWeapon weapon, int penetrationAmount)
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

    private void UpgradeRollerSkates()
    {
        RollerSkates rollerSkates = (RollerSkates)utilities[0];
        rollerSkates.UpgradeMovementSpeed();
    }

    private void UpgradeGrooveArmor()
    {
        player.IncreaseHealth(1.1f);
    }
}
