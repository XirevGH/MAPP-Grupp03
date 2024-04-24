using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using static UnityEditor.Progress;
using UnityEditor.Playables;
using System.Reflection;


public class UpgradeAbility : MonoBehaviour
{
    [SerializeField] private GameObject[] panels;
    [SerializeField] private List<GameObject> panelList;
    [SerializeField] private List<Weapon> weapons = new List<Weapon>();
    [SerializeField] private List<Utility> utilities = new List<Utility>();
    [SerializeField] private Player player;
    [SerializeField] private PlayerMovement playerMovement;
    private List<Item> currentPlayerItems = new List<Item>();
    private Dictionary<Item, List<string>> upgradeOptions = new Dictionary<Item, List<string>>();
    private List<string> typeOptions = new List<string>();
    private int allowedAmountOfWeapons;
    private int allowedAmountOfUtility;

    void Start()
    {
        allowedAmountOfWeapons = 4;
        allowedAmountOfUtility = 4;
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

    public void InitializeUpgradeOptions()
    {
        //Loops over each item.
        foreach (Item item in currentPlayerItems)
        {
            //Adds each item as a key to a dictionary along with its list of upgrade options as values if it doesn't already exist.
            if (!upgradeOptions.ContainsKey(item)) 
            { 
                upgradeOptions.Add(item, item.GetUpgradeOptions());
            }
        }
    }
    private Tuple<Item, string> ChooseRandomUpgrade()
    {
        //Get a random number based on the amount of items in the dictionary.
        int randomItemIndex = UnityEngine.Random.Range(0, upgradeOptions.Count);

        //Fetch the keys from the dictionary and put it in a list and use the random number as an index to get a random item.
        Item chosenItem = new List<Item>(upgradeOptions.Keys)[randomItemIndex];

        //Get a random number based on the amount of upgrade options that the chosen item has.
        int randomUpgradeIndex = UnityEngine.Random.Range(0, chosenItem.GetUpgradeOptions().Count);

        //Fetch the upgrade option at the index of the random number.
        string chosenUpgrade = chosenItem.GetUpgradeOptions()[randomUpgradeIndex];

        //Remove the upgrade option so that it cannot be chosen again for the remaining options.
        RemoveUpgradeOption(chosenItem, chosenUpgrade);

        return Tuple.Create(chosenItem, chosenUpgrade);
    }


    private void RemoveUpgradeOption(Item item, string upgradeOption)
    {
        upgradeOptions[item].Remove(upgradeOption);
    }

    private string ChooseUpgradeType()
    {
        //Get a random number based on the length of the typeOptions list.
        int randomTypeIndex = UnityEngine.Random.Range(0, typeOptions.Count);

        //Return the type of upgrade using the random index.
        return typeOptions[randomTypeIndex];
    }

    private void GetUpgradableItems()
    {
        //Gets the current items that the player has and adds them to the list.
        currentPlayerItems = player.GetCurrentItems();
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
        panelList = new List<GameObject>(panels);
    }

    private GameObject ChooseRandomPanel()
    {
        //Get a random number based on the amount of panels in the panel list.
        int randomPanel = UnityEngine.Random.Range(0, panelList.Count);

        //Save the Game Object of the chosen panel.
        GameObject chosenPanel = panelList[randomPanel];

        //Remove the chosen panel so it cannot be chosen for the remaining upgrades.
        RemovePanelAsOption(chosenPanel);

        //Return the game object of the randomly chosen panel.
        return chosenPanel;
    }

    private void SetPanelText(GameObject panel, string upgrade)
    {
        panel.GetComponentInChildren<TextMeshPro>().text = upgrade;
    }
    private void RemovePanelAsOption(GameObject chosenPanel)
    {
        panelList.Remove(chosenPanel);
    }
    private void SetPanelMethod(GameObject panel, Item item, string upgrade)
    {
        panel.GetComponent<UpgradeButton>().PrepareMethodToExecute("PerformRandomizedAction", item, upgrade);
    }

    public void StartUpgradeSystem()
    {
        GetUpgradableItems();
        DetermineTypeOptions();
        InitializeUpgradeOptions();
        InitializeAvailablePanels();
        for (int i = 0; i < panels.Length; i++)
        {
            string typeOfUpgrade = ChooseUpgradeType();
            GameObject chosenPanel = ChooseRandomPanel();
            if (typeOfUpgrade.Equals("") 
            {
                (Item item, string upgrade) = ChooseRandomUpgrade();
            }
            
            SetPanelText(chosenPanel, upgrade);
            SetPanelMethod(chosenPanel, item, upgrade);
        }
    }

    public void PerformRandomizedAction(Item item, string upgrade)
    {
        MethodInfo methodInfo = item.GetType().GetMethod(upgrade);
        methodInfo.Invoke(item, null);
    }

    private void DetermineTypeOptions()
    {
        //Start two counters that will keep track of how many weapons and utilities the player has.
        int numberOfWeapons = 0;
        int numberOfUtilities = 0;

        //Start a loop that will iterate through each item.
        foreach (Item item in currentPlayerItems)
        {
            //Check if the current item inherits from the Weapon class.
            if (item is Weapon)
            {
                //Increase the counter for the amount of weapons the player has.
                numberOfWeapons++;

                //Only add the string if it doesn't already exist.
                if (!typeOptions.Contains("Weapon Upgrade"))
                {
                    //Add the string to the typeOptions so that weapon upgrades can be chosen as options.
                    typeOptions.Add("Weapon Upgrade");
                }
                
            }
            //Check if the current item inherits from the Utility class.
            if (item is Utility)
            {
                //Increase the counter for the amount of utilities the player has.
                numberOfUtilities++;

                //Only add the string if it doesn't already exist.
                if (!typeOptions.Contains("Utility Upgrade"))
                {
                    //Add the string to the typeOptions so that utility upgrades can be chosen as options.
                    typeOptions.Add("Utility Upgrade");
                }
            }
        }

        //Check if we're at the maximum allowed amount of weapons yet.
        if (numberOfWeapons < allowedAmountOfWeapons)
        {
            //If we aren't then add "Weapon" to the list so that it can be chosen as an option.
            typeOptions.Add("Weapon");
        }
        //Check if we're at the maximum allowed amount of utilities yet.
        if (numberOfUtilities < allowedAmountOfUtility)
        {
            //If we aren't then add "Utility" to the list so that it can be chosen as an option.
            typeOptions.Add("Utility");
        }
        //Check if we are at the allowed amount of weapons.
        if (numberOfWeapons == allowedAmountOfWeapons && typeOptions.Contains("Weapon"))
        {
            //Remove the string from the list so that it cannot be chosen as an option.
            typeOptions.Remove("Weapon");
        }
        //Check if we are at the allowed amount of utilities.
        if (numberOfUtilities == allowedAmountOfUtility)
        {
            //Remove the string from the list so that it cannot be chosen as an option.
            typeOptions.Remove("Utility");
        }
    }


}