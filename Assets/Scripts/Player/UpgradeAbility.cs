using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using System.Reflection;


public class UpgradeAbility : MonoBehaviour
{
    [SerializeField] private GameObject[] panels;
    [SerializeField] private List<Item> items = new List<Item>();
    [SerializeField] private Player player;
    [SerializeField] private PlayerMovement playerMovement;
    private List<GameObject> choicePanels;
    private List<Item> choiceItems;
    private List<Item> currentPlayerItems = new List<Item>();
    private Dictionary<Item, List<string>> upgradeOptions = new Dictionary<Item, List<string>>();
    private List<string> typeOptions = new List<string>();
    private int allowedAmountOfWeapons;
    private int allowedAmountOfUtility;

    private void Awake()
    {
        allowedAmountOfWeapons = 4;
        allowedAmountOfUtility = 3;
    }

    public void InitializeUpgradeOptions()
    {
        upgradeOptions = new Dictionary<Item, List<string>>();
        //Get the current items that the player has.
        currentPlayerItems = GetItems();

        //Loops over each item that the player has.
        foreach (Item item in currentPlayerItems)
        {
            //Adds each item as a key to a dictionary along with its list of upgrade options as values if it doesn't already exist.
            if (!upgradeOptions.ContainsKey(item)) 
            { 
                upgradeOptions.Add(item, item.GetUpgradeOptions());
            }
        }
    }

    public Tuple<Item, string> ChooseRandomUpgrade()
    {
        //Get a random number based on the amount of items in the dictionary of available upgrade options.
        int randomItemIndex = UnityEngine.Random.Range(0, upgradeOptions.Count);

        //Fetch the keys from the dictionary and put it in a list and use the random number as an index to get a random item.
        Item chosenItem = new List<Item>(upgradeOptions.Keys)[randomItemIndex];

        //Get a random number based on the amount of upgrade options that the chosen item currently has.
        int randomUpgradeIndex = UnityEngine.Random.Range(0, upgradeOptions[chosenItem].Count);

        //Fetch the upgrade option at the index of the random number.
        string chosenUpgrade = upgradeOptions[chosenItem][randomUpgradeIndex];

        //Remove the upgrade option so that it cannot be chosen again for the remaining options.
        RemoveUpgradeOption(chosenItem, chosenUpgrade);

        //Return the chosen item along with the chosen upgrade.
        return Tuple.Create(chosenItem, chosenUpgrade);
    }

    private Tuple<Item, string> ChooseRandomItem()
    {
        //Get a random number based on the amount of items available.
        int randomItemIndex = UnityEngine.Random.Range(0, choiceItems.Count);

        //Get the item that is at the random index.
        Item chosenItem = choiceItems[randomItemIndex];

        //Remove the item so it cannot be received again as a choice during the current upgrade session.
        RemoveItemAsChoice(chosenItem, choiceItems);

        //Return the item along with its name.
        return Tuple.Create(chosenItem, chosenItem.GetName());
    }

    private void RemoveUpgradeOption(Item item, string upgradeOption)
    {
        upgradeOptions[item].Remove(upgradeOption);
        if (upgradeOptions[item].Count == 0)
        {
            upgradeOptions.Remove(item);
        }
    }

    private void RemoveItemAsChoice(Item item, List<Item> choiceItems)
    {
        choiceItems.Remove(item);
    }

    private string ChooseUpgradeType()
    {
        
        //Get a random number based on the length of the typeOptions list.
        int randomTypeIndex = UnityEngine.Random.Range(0, typeOptions.Count);
        //Return the type of upgrade using the random index.
        return typeOptions[randomTypeIndex];
    }

    private List<Item> GetItems()
    {
        //Gets the current items that the player has and adds them to the list.
        return player.GetCurrentItems();
    }

    private void InitializePanels()
    {
        choicePanels = new List<GameObject>(panels);
    }

    private void InitializeItems()
    {
        //Prepare all available items in order to be able to remove from the random choices from the list 
        // during the current upgrade session without it affecting the available items.
        choiceItems = new List<Item>(items);
    }

    private GameObject ChooseRandomPanel()
    {
        //Get a random number based on the amount of panels in the panel list.
        int randomPanel = UnityEngine.Random.Range(0, choicePanels.Count);

        //Save the Game Object of the chosen panel.
        GameObject chosenPanel = choicePanels[randomPanel];

        //Remove the chosen panel so it cannot be chosen for the remaining upgrades.
        RemovePanelAsOption(chosenPanel);

        //Return the game object of the randomly chosen panel.
        return chosenPanel;
    }

    private void SetPanelText(GameObject panel, string upgrade)
    {
        panel.GetComponentInChildren<TMP_Text>().text = upgrade;
    }
    private void RemovePanelAsOption(GameObject chosenPanel)
    {
        choicePanels.Remove(chosenPanel);
    }
    private void SetPanelMethod(GameObject panel, string typeOfUpgrade, Item item, string upgrade)
    {
        if (typeOfUpgrade.Equals("Upgrade"))
        { 
            panel.GetComponent<UpgradeButton>().PrepareMethodToExecute("PerformRandomizedUpgrade", item, upgrade);
        }
        else
        {
            panel.GetComponent<UpgradeButton>().PrepareMethodToExecute("GiveRandomizedItem", item);
        }
    }

    public void StartUpgradeSystem()
    {
        InitializeItems();
        InitializePanels();
        currentPlayerItems = GetItems();
        if (currentPlayerItems.Count > 0) 
        {
            InitializeUpgradeOptions();
        }
        for (int i = 0; i < panels.Length; i++)
        {
            Item item;
            string upgradeText;

            DetermineTypeOptions();
            string typeOfUpgrade = ChooseUpgradeType();
            
            if (typeOfUpgrade.Equals("Upgrade"))
            {
                (item, upgradeText) = ChooseRandomUpgrade();
            }
            else
            {
                (item, upgradeText) = ChooseRandomItem();
            }
            GameObject chosenPanel = ChooseRandomPanel();
            SetPanelText(chosenPanel, upgradeText);
            SetPanelMethod(chosenPanel, typeOfUpgrade, item, upgradeText);
        }
    }

    public void PerformRandomizedUpgrade(Item item, string upgradeText)
    {
        MethodInfo methodInfo = item.GetType().GetMethod(upgradeText);
        methodInfo.Invoke(item, null);
    }

    public void GiveRandomizedItem(Item item)
    {
        player.AddItem(item);
        items.Remove(item);

    }

    private void DetermineTypeOptions()
    {
        //Only add the ability to upgrade items if the player has more than one item or if the single available item has at least one available upgrade.
        if (!typeOptions.Contains("Upgrade"))
        {
            typeOptions.Add("Upgrade");
        }
        int options = 0;
        foreach (Item key in upgradeOptions.Keys)
        {
            options += upgradeOptions[key].Count;
        }
        if (options == 0)
        {
            //If we do not have any items that can be upgraded and it currently exists in the list then remove it.
            if (typeOptions.Contains("Upgrade"))
            {
                typeOptions.Remove("Upgrade");
            }
        }

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
            }
            //Check if the current item inherits from the Utility class.
            if (item is Utility)
            {
                //Increase the counter for the amount of utilities the player has.
                numberOfUtilities++;
            }
        }
        //Check if we're at the maximum allowed amount of weapons yet.
        if (numberOfWeapons < allowedAmountOfWeapons)
        {
            //If we aren't at the max then add "Weapon" to the list so that it can be chosen as an option unless it is already in there.
            if (!typeOptions.Contains("Weapon"))
            { 
                typeOptions.Add("Weapon");
            }
        }
        //Check if we're at the maximum allowed amount of utilities yet.
        if (numberOfUtilities < allowedAmountOfUtility)
        {
            //If we aren't then add "Utility" to the list so that it can be chosen as an option unless it is already in there.
            if (!typeOptions.Contains("Utility"))
            {
                typeOptions.Add("Utility");
            }
        }
        //Check if we are at the allowed amount of weapons.
        if (numberOfWeapons == allowedAmountOfWeapons && typeOptions.Contains("Weapon"))
        {
            //Remove the string from the list so that it cannot be chosen as an option.
            typeOptions.Remove("Weapon");
        }
        //Check if we are at the allowed amount of utilities.
        if (numberOfUtilities == allowedAmountOfUtility && typeOptions.Contains("Utility"))
        {
            //Remove the string from the list so that it cannot be chosen as an option.
            typeOptions.Remove("Utility");
        }
    }
}