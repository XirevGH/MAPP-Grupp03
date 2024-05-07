using TMPro;
using UnityEngine;

public class TextHandler : MonoBehaviour
{
    private TMP_Text textfield;
    public string item;
    public string method;
    public bool isPercentage;

    private void Awake()
    {
        textfield = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        string value = GameObject.FindGameObjectWithTag(item).GetComponent<Item>().GetType().GetMethod(method).Invoke(GameObject.FindGameObjectWithTag(item).GetComponent<Item>(), null).ToString();
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
