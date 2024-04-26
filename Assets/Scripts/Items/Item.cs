using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    protected List<string> upgradeOptions = new List<string>();
    public string itemName;

    public string GetName()
    {
        return itemName;
    }

    public List<string> GetUpgradeOptions()
    {
        if (upgradeOptions.Count == 0)
        {
            CreateUpgradeOptions();
        }
        return new List<string>(upgradeOptions);
    }

    protected abstract void CreateUpgradeOptions();

}
