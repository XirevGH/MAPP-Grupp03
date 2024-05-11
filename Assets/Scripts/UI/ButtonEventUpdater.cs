using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonEventUpdater : MonoBehaviour
{
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private Button button;
    [SerializeField] private MetaUpgradeSystem metaUpgradeSystem;

    void Start()
    {
        SetPriceOnButton();
    }

    private void Update()
    {
        if (metaUpgradeSystem.GetCurrencyAmount() < int.Parse(priceText.text[1..]))
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }
    }

    private void SetPriceOnButton()
    {
        UnityAction<int> action = metaUpgradeSystem.DeductCurrency;
        button.onClick.AddListener(() => action(int.Parse(priceText.text[1..])));
    }

    public void UpdatePrice()
    {
        SetPriceOnButton();
    }
}