using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MetaUpgradeNodeUI : MonoBehaviour
{
    [Header("Upgrade Configuration")]
    [SerializeField] private UpgradeDefinitionSO upgradeDefinition;

    [Header("Dynamic UI Elements")]
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private TMP_Text currentValueText;
    [SerializeField] private Slider rankSlider;
    [SerializeField] private Button purchaseButton;

    private void Start()
    {
        InitializeSlider();

        if (MetaUpgradeManager.Instance != null)
        {
            MetaUpgradeManager.Instance.OnUpgradePurchased += HandleUpgradePurchased;
            MetaUpgradeManager.Instance.OnCurrencyChanged += HandleCurrencyChanged;
        }

        if (purchaseButton != null)
        {
            purchaseButton.onClick.AddListener(OnPurchaseButtonClicked);
        }

        RefreshDisplay();
    }

    private void OnDestroy()
    {
        if (MetaUpgradeManager.Instance != null)
        {
            MetaUpgradeManager.Instance.OnUpgradePurchased -= HandleUpgradePurchased;
            MetaUpgradeManager.Instance.OnCurrencyChanged -= HandleCurrencyChanged;
        }
    }

    private void InitializeSlider()
    {
        if (rankSlider != null && upgradeDefinition != null)
        {
            rankSlider.minValue = 0;
            rankSlider.maxValue = upgradeDefinition.MaxRank;
        }
    }

    public void RefreshDisplay()
    {
        if (upgradeDefinition == null || MetaUpgradeManager.Instance == null) return;

        int currentRank = MetaUpgradeManager.Instance.GetUpgradeRank(upgradeDefinition);

        if (rankSlider != null)
        {
            rankSlider.value = currentRank;
        }

        if (currentValueText != null)
        {
            currentValueText.text = currentRank.ToString();
        }

        bool isMaxRank = currentRank >= upgradeDefinition.MaxRank;
        if (isMaxRank)
        {
            if (priceText != null) priceText.text = "MAX";
            if (purchaseButton != null) purchaseButton.interactable = false;
        }
        else
        {
            int cost = upgradeDefinition.GetCostForRank(currentRank);
            if (priceText != null) priceText.text = cost.ToString();

            if (purchaseButton != null)
            {
                purchaseButton.interactable = MetaUpgradeManager.Instance.CanAfford(upgradeDefinition);
            }
        }
    }

    private void HandleUpgradePurchased(UpgradeDefinitionSO purchasedUpgrade, int newRank)
    {
        if (purchasedUpgrade == upgradeDefinition)
        {
            RefreshDisplay();
        }
    }

    private void HandleCurrencyChanged(int newTotalCurrency)
    {
        RefreshDisplay();
    }

    private void OnPurchaseButtonClicked()
    {
        if (upgradeDefinition != null && MetaUpgradeManager.Instance != null)
        {
            MetaUpgradeManager.Instance.TryPurchaseUpgrade(upgradeDefinition);
        }
    }
}