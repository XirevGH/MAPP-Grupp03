using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Item : MonoBehaviour
{
    [SerializeField] protected Player player;
    protected List<string> upgradeOptions = new List<string>();
    private bool initialUpgradesComplete = false;
    protected bool active;
    public string itemName;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Main") { 
            if (!player.GetCurrentItems().Contains(this))
            {
                gameObject.SetActive(false);
            }
        }
    }

    protected virtual void Awake()
    {
        CreateUpgradeOptions();
    }

    protected void EndInitialUpgrades()
    {
        initialUpgradesComplete = true;
    }

    protected bool InitialUpgradesComplete()
    {
        return initialUpgradesComplete;
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

