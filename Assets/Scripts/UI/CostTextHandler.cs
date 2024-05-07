using UnityEngine;
using TMPro;

public class CostTextHandler : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] private string methodName;
    private MetaUpgradeSystem upgrade;

    private void Awake()
    {
        upgrade = GameObject.FindGameObjectWithTag("MetaUpgradeSystem").GetComponent<MetaUpgradeSystem>();
        GetComponent<TMP_Text>().text = upgrade.GetUpgradeCost(itemName + "," + methodName);
    }
}
