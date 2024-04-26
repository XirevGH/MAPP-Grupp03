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

    public abstract List<string> GetUpgradeOptions();
}
