using System.Reflection;
using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private UpgradeAbility upgradeAbility;
    
    private Item item;
    private string upgrade;
    private string method;

    public void PrepareMethodToExecute(string method, Item item, string upgrade)
    {
        this.method = method;
        this.item = item;
        this.upgrade = upgrade;
    }

    public void PrepareMethodToExecute(string method, Item item)
    {
        this.method = method;
        this.item = item;
    }

    public void ExecuteMethod()
    {
        if (method == "PerformRandomizedUpgrade") 
        {
            upgradeAbility.PerformRandomizedUpgrade(item, upgrade);
        }
        if (method == "GiveRandomizedItem")
        {
            upgradeAbility.GiveRandomizedItem(item);
        }
    }
}
