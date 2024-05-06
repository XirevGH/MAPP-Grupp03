using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Item : MonoBehaviour
{
    [SerializeField] protected Player player;
    [SerializeField] protected string itemName;
    protected List<string> upgradeOptions = new List<string>();
    protected bool active;

    private void LateUpdate()
    {
        if (!player.GetCurrentItems().Contains(this))
        {
            gameObject.SetActive(false);
        }
    }

    protected virtual void Awake()
    {
        CreateUpgradeOptions();
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

    public abstract string GetItemType();
}

