using UnityEngine;
using TMPro;

public class CurrencyTextHandler : MonoBehaviour
{
    [SerializeField] private MetaUpgradeSystem metaUpgradeSystem;
    [SerializeField] private TMP_Text textField;
    private string currency;


    void Start()
    {
        SetText();
    }

    public void UpdateCurrency()
    {
        SetText();
    }

    private void SetText()
    {
        currency = metaUpgradeSystem.GetCurrency().ToString();
        textField.text = "Currency: " + currency;
    }
}
