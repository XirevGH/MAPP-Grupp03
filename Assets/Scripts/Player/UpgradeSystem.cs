using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using System.Reflection;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UpgradeSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] panels;
    [SerializeField] private Item[] startingItems;
    [SerializeField] private Player player;
    [SerializeField] private Sprite weaponPanel, utilityPanel;

    private MetaUpgradeSystem controller;

    private Dictionary<Item, List<string>> upgradeOptions = new Dictionary<Item, List<string>>();
    private List<Item> sessionItems;
    private List<Item> choiceItems;
    private List<Item> currentPlayerItems = new List<Item>();
    private List<string> typeOptions = new List<string>();
    
    private int allowedAmountOfWeapons;
    private int allowedAmountOfUtility;

    private void Awake()
    {
        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }

        if (controller == null)
        {
            controller = FindObjectOfType<MetaUpgradeSystem>();
        }
        
        allowedAmountOfWeapons = 4;
        allowedAmountOfUtility = 3;
        sessionItems = new List<Item>(controller.GetItems());
    }


    public void InitializeUpgradeOptions(List<Item> playerItems)
    {
        upgradeOptions = new Dictionary<Item, List<string>>();

        //Loops over each item that the player has.
        foreach (Item item in playerItems)
        {
            //Adds each item as a key to a dictionary along with its list of upgrade options as values if it doesn't already exist.
            if (!upgradeOptions.ContainsKey(item)) 
            { 
                upgradeOptions.Add(item, item.GetUpgradeOptions());
            }
        }
    }

    private string GetUpgradeDescription(Item item, string typeOfChoice, string upgrade)
    {
        var file = Resources.Load<TextAsset>("Text/UpgradeDescriptions");
        foreach (string line in file.text.Split("\n")) {
            string[] fields = line.Split(',');
            string type = fields[0];
            string name = fields[1];
            string description = fields[2];
            if (typeOfChoice == "Upgrade" && type.Equals("Upgrade") && name.Equals(item.GetName())) {
                string upgradeMethodName = fields[3];
                if (upgradeMethodName.Equals(upgrade))
                {
                    string getStatIncrease = fields[4];
                    string symbol = fields[5];
                    return description + " " + item.GetType().GetMethod(getStatIncrease).Invoke(item, null) + symbol;
                }
            }
            else
            {
                if (name.Equals(item.GetName()))
                {
                    return description;
                }
            }
        }
        return "Item Missing or Upgrade Missing or Text Missing";
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

        Debug.Log("Upgrade: Amount of upgrades for " + chosenItem + ": " + upgradeOptions[chosenItem].Count);
        Debug.Log("Upgrade: Random index chosen: " + randomUpgradeIndex);
        string chosenUpgrade = upgradeOptions[chosenItem][randomUpgradeIndex];

        //Return the chosen item along with the chosen upgrade.
        return Tuple.Create(chosenItem, chosenUpgrade);
    }

    private Tuple<Item, string> ChooseRandomItem(string typeOfItem)
    {
        List<Item> itemsOfType = new List<Item>();

        foreach (Item item in choiceItems)
        {
            if (item is Weapon && typeOfItem.Equals("Weapon"))
            {
                itemsOfType.Add((Weapon)item);
            }
            if (item is Utility && typeOfItem.Equals("Utility"))
            {
                itemsOfType.Add((Utility)item);
            }
        }

        
        int randomItemIndex = UnityEngine.Random.Range(0, itemsOfType.Count);

        Item chosenItem = itemsOfType[randomItemIndex];
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

    private string ChooseUpgradeType(List<string> typeOptions)
    {
        //Get a random number based on the length of the typeOptions list.
        int randomTypeIndex = UnityEngine.Random.Range(0, typeOptions.Count);
        //Return the type of upgrade using the random index.
        return typeOptions[randomTypeIndex];
    }

    public List<Item> GetItems()
    {
        //Gets the current items that the player has and adds them to the list.
        return player.GetCurrentItems();
    }

    private void InitializeItems()
    {
        //Prepare all available items in order to be able to remove from the random choices from the list 
        // during the current upgrade session without it affecting the available items.
        choiceItems = new List<Item>(sessionItems);
    }


    private void SetPanelText(GameObject panel, Item item, string textDescription)
    {
        panel.GetComponentInChildren<TMP_Text>().text = item.GetName() + "\n\n" + textDescription;
        if(item.GetItemType().Equals("Weapon"))
        {
            panel.GetComponent<UnityEngine.UI.Image>().sprite = weaponPanel;
        } 
        else
        {
            panel.GetComponent<UnityEngine.UI.Image>().sprite = utilityPanel;
        }
    }


    private void SetPanelMethod(GameObject panel, string typeOfChoice, Item item, string upgrade)
    {
        //Depending on if it is an item or an upgrade, the button gets told to call a different method along with the necessary information.
        if (typeOfChoice.Equals("Upgrade"))
        { 
            panel.GetComponent<UpgradeButton>().PrepareMethodToExecute(nameof(PerformRandomizedUpgrade), item, upgrade);
        }
        else
        {
            panel.GetComponent<UpgradeButton>().PrepareMethodToExecute(nameof(GiveRandomizedItem), item);
        }
    }

    public void StartUpgradeSystem()
    {
        //Takes the list of items that the upgrade system is currently holding
        //and places them inside of a new list so that the new list can
        //be modified inside of the loop without it affecting the original list.
        InitializeItems();

        //Gets all of the items that the player is currently holding.
        currentPlayerItems = GetItems();

        //If player has at least one item then we get all of the upgrades that are possible to present to the player.
        if (currentPlayerItems.Count > 0) 
        {
            //Sends in the items that the player has which get put into a dictionary that connects each item to its list of upgrade options.
            InitializeUpgradeOptions(currentPlayerItems);
        }

        // Chooses a random item or upgrade based on the amount of panels the upgrade system has.
        for (int i = 0; i < panels.Length; i++)
        {
            Item item;
            string upgradeText;

            //Figure out what is possible to present to the player based on what they currently have.
            typeOptions = DetermineTypeOptions();

            //Randomly choose one type.
            string typeOfChoice = ChooseUpgradeType(typeOptions);

            //Depending on which type was chosen, we execute different methods to either get an upgrade or an item.
            if (typeOfChoice.Equals("Upgrade"))
            {
                //Select a random item that the player has, and then choose a random upgrade from that item.
                (item, upgradeText) = ChooseRandomUpgrade();

                //Remove the upgrade option so that it cannot be chosen again for the remaining loops.
                RemoveUpgradeOption(item, upgradeText);
            }
            else
            {
                //Choose an item based on the random type, which at this point can only be Weapon or Utility.
                (item, upgradeText) = ChooseRandomItem(typeOfChoice);

                //Remove the item so it cannot be received again as a choice for the remaining loops.
                RemoveItemAsChoice(item, choiceItems);
            }


            //Sets the text on the panel for the type of item or upgrade chosen.
            SetPanelText(panels[i], item, GetUpgradeDescription(item, typeOfChoice, upgradeText));

            //Prepares the button with the method to call in case that button is pressed.
            SetPanelMethod(panels[i], typeOfChoice, item, upgradeText);
        }
    }

    public void PerformRandomizedUpgrade(Item item, string upgradeText)
    {
        //Uses reflection to dynamically get the correct type and method to call.
        MethodInfo methodInfo = item.GetType().GetMethod(upgradeText);

        //Calls the upgrade method on the given item.
        methodInfo.Invoke(item, null);
    }

    public void GiveRandomizedItem(Item item)
    {
        //Provides the player with the item.
        player.AddItem(item);
        MethodInfo methodInfo = item.GetType().GetMethod("EnableGameObject");
        methodInfo.Invoke(item, null);
        //Removes the item from the UpgradeAbility class so that it cannot be given again.
        sessionItems.Remove(item);
    }

    private List<string> DetermineTypeOptions()
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
        //Check if we are at the allowed amount of weapons or are out of weapons.
        if ((numberOfWeapons == allowedAmountOfWeapons || CountItemType(typeof(Weapon)) == 0) && typeOptions.Contains("Weapon"))
        {
            //Remove the string from the list so that it cannot be chosen as an option.
            typeOptions.Remove("Weapon");
        }
        //Check if we are at the allowed amount of utilities or we are out of utilities.
        if ((numberOfUtilities == allowedAmountOfUtility || CountItemType(typeof(Utility)) == 0) && typeOptions.Contains("Utility"))
        {
            //Remove the string from the list so that it cannot be chosen as an option.
            typeOptions.Remove("Utility");
        }

        return typeOptions;
    }

    private int CountItemType(Type type)
    {
        int itemCount = 0;
        foreach (Item item in choiceItems)
        {
            if (type.IsAssignableFrom(item.GetType()))
            {
                itemCount++;
            }
        }
        return itemCount;
    }
}