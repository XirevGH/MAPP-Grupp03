using TMPro;
using UnityEngine;

public class CurrencyTextHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text textField;

    private void Awake()
    {
        if (textField == null)
        {
            textField = GetComponent<TMP_Text>();
        }
    }

    private void Start()
    {
        if (MetaUpgradeManager.Instance != null)
        {
            MetaUpgradeManager.Instance.OnCurrencyChanged += UpdateDisplay;
            UpdateDisplay(MetaUpgradeManager.Instance.GetCurrency());
        }
        else
        {
            SetText(0);
        }
    }

    private void OnDestroy()
    {
        if (MetaUpgradeManager.Instance != null)
        {
            MetaUpgradeManager.Instance.OnCurrencyChanged -= UpdateDisplay;
        }
    }

    private void UpdateDisplay(int totalCurrency)
    {
        SetText(totalCurrency);
    }

    private void SetText(int amount)
    {
        if (textField != null)
        {
            textField.text = ":" + amount;
        }
    }
}