using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    protected List<string> upgradeOptions = new List<string>();
    public string itemName;

    private void Awake()
    {
        CreateUpgradeOptions();
    }

    public string GetName()
    {
        return itemName;
    }

    public List<string> GetUpgradeOptions()
    {
        return new List<string>(upgradeOptions);
    }

    protected abstract void CreateUpgradeOptions();
}

