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
        string value = item.GetType().GetMethod(method).Invoke(item, null).ToString();
        if (method.Contains("Increase"))
        {
            string firstSplit = textfield.text.Split('+')[0].Trim();
            textfield.text = firstSplit += isPercentage ? firstSplit.Length < 10 ? "\t\t+" + value + "%" : "\t+" + value + "%" : firstSplit.Length < 10 ? "\t\t+" + value : "\t+" + value;
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
