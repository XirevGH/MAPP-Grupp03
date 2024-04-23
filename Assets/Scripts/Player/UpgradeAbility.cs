using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UIElements;

public class UpgradeAbility : MonoBehaviour
{
    [SerializeField] private GameObject[] panels;
    [SerializeField] private List<Weapon> weapons = new List<Weapon>();
    [SerializeField] private List<Utility> utilities = new List<Utility>();
    [SerializeField] private Player player;
    [SerializeField] private PlayerMovement playerMovement;
    private List<Item> currentPlayerItems = new List<Item>();
    private Dictionary<Item, List<string>> upgradeOptions;
    private List<string> typeOptions;
    private int allowedAmountOfWeapons;
    private int allowedAmountOfUtility;

    void Start()
    {
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
        int randomWeapon = Random.Range(0, weapons.Count);
        Weapon chosenWeapon = weapons[randomWeapon];
        weapons.Remove(chosenWeapon);
        return chosenWeapon;
    }

    public void ChooseRandomUpgrade()
    {
        currentPlayerItems = player.GetCurrentItems();
        

    }
    public List<Weapon> GetAvailableWeapons()
    {
        return new List<Weapon>(weapons);
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
        if (player.GetCurrentItems().Count == 0) 
        { 

        }
        if (player.GetCurrentItems().Count < allowedAmountOfWeapons) { 
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
        player.GetCurrentItems();
    }

    public void UpgradeRollerSkates()
    {
        RollerSkates rollerSkates = (RollerSkates)utilities[0];
        rollerSkates.UpgradeMovementSpeed();
    }

    public void UpgradeGrooveArmor()
    {
        player.IncreaseMaxHealth(1.1f);
    }

    private void InitializeAvailablePanels()
    {
        List<GameObject> panelList = new List<GameObject>(panels);
    }
    public void StartUpgradeSystem()
    {
        InitializeAvailablePanels();
        CheckForUpgradableItems();
    }
}

