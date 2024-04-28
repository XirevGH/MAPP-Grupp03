using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private UpgradeSystem upgradeSystem;
    
    private Item item;
    private string upgrade;
    private string method;

    //This method initializes the button with the information it needs in order to provide the upgrade when clicked.
    public void PrepareMethodToExecute(string method, Item item, string upgrade)
    {
        this.method = method;
        this.item = item;
        this.upgrade = upgrade;
    }

    //Overload for when the button is only giving an item.
    public void PrepareMethodToExecute(string method, Item item)
    {
        this.method = method;
        this.item = item;
    }

    //This method is called from the Pointer Click event in the EventTrigger component on each ChoiceButton GameObject.
    public void ExecuteMethod()
    {
        if (method == "PerformRandomizedUpgrade") 
        {
            //Calls the appropriate method in the UpgradeAbility class for providing the upgrade.
            upgradeSystem.PerformRandomizedUpgrade(item, upgrade);
        }
        if (method == "GiveRandomizedItem")
        {
            //Calls the appropriate method in the UpgradeAbility class for providing the item.
            upgradeSystem.GiveRandomizedItem(item);
        }
    }
}
