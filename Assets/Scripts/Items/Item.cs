using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Item : MonoBehaviour
{
    public abstract ItemDefinitionSO BaseItemData { get; }

    [SerializeField] protected Player player;

    protected List<string> upgradeOptions = new List<string>();
    protected bool active;

    private void LateUpdate()
    {
        if (!player.GetCurrentItems().Contains(this))
        {
            gameObject.SetActive(false);
        }
    }

    public string GetName()
    {
        if (BaseItemData != null)
        {
            return BaseItemData.ItemName;
        }
        return gameObject.name;
    }

    public Sprite GetIcon() => BaseItemData.Icon;

    public int GetBeatNumber() => BaseItemData.BeatNumber;

    public void EnableGameObject()
    {
        gameObject.SetActive(true);
    }

    public string GetItemType() => BaseItemData.GetItemType();
}

