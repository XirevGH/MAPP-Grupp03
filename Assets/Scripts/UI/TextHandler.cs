using TMPro;
using UnityEngine;

public class TextHandler : MonoBehaviour
{
    private TMP_Text textfield;
    private MetaUpgradeSystem metaUpgradeSystem;
    private Item item;

    public string itemName;
    public string method;
    public bool isPercentage;

    private void Awake()
    {
        textfield = GetComponent<TMP_Text>();
        metaUpgradeSystem = GameObject.FindGameObjectWithTag("MetaUpgradeSystem").GetComponent<MetaUpgradeSystem>();
        foreach (Item item in metaUpgradeSystem.GetItems())
        {
            if (item.GetName() == itemName)
            {
                this.item = item;
            }
        }
    }

    private void Update()
    {
        /*Debug.Log("Item string name: " + item);
        Debug.Log("Method string name: " + method);
        Debug.Log("Actual method: " + item.GetType().GetMethod(method).Name);
        Debug.Log("Item GameObject: " + item.name);
        Debug.Log("Item actual name:" + item.GetName())*/;
        string value = item.GetType().GetMethod(method).Invoke(item, null).ToString();
        if (method.Contains("Increase"))
        {
            textfield.text = isPercentage ? "+" + value + "%" : "+" + value;
        }
        if (method.Contains("Cost"))
        {
            textfield.text = "$" + value;
        }
        if (method.Contains("Current"))
        {
            textfield.text = isPercentage ? value + "%" : value;
        }
    }
}
