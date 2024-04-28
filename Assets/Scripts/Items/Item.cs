using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] protected Player player;
    protected List<string> upgradeOptions = new List<string>();
    protected bool active;
    public string itemName;

    private void Awake()
    {
        CreateUpgradeOptions();
        if (!player.GetCurrentItems().Contains(this)) { 
            gameObject.SetActive(false);
        }
    }

    public string GetName()
    {
        return itemName;
    }

    public void EnableGameObject()
    {
        gameObject.SetActive(true);
    }

    public List<string> GetUpgradeOptions()
    {
        return new List<string>(upgradeOptions);
    }

    protected abstract void CreateUpgradeOptions();
}

