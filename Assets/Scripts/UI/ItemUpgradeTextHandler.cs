using TMPro;
using UnityEngine;

public class ItemUpgradeTextHandler : MonoBehaviour
{
    [SerializeField] private Item item;
    private TMP_Text textfield;

    public string method;
    public bool isPercentage;

    private void Awake()
    {
        textfield = GetComponent<TMP_Text>();
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
