using UnityEngine;
using TMPro;

public class CurrencyTextHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text textField;
    private string currency;

    public static CurrencyTextHandler Instance
    {
        get; private set;
    }

    void Start()
    {
        Instance = this;
        SetText();
    }

    public void UpdateCurrency()
    {
        SetText();
    }

    private void SetText()
    {
        currency = MetaUpgradeSystem.Instance.GetCurrencyAmount().ToString();
        textField.text = ":" + currency;
    }
}
