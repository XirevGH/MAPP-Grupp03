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
        // 1. Subscribe to live currency updates
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
        // Unsubscribe to prevent memory leaks when changing scenes
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
            textField.text = ":" + amount; // Matches your UI format (e.g. ":500")
        }
    }
}