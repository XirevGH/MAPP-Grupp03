using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonEventUpdater : MonoBehaviour
{
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private Button button;
    [SerializeField] private Slider slider;

    void Start()
    {
        SetPriceOnButton();
    }

    private void Update()
    {
        if (MetaUpgradeSystem.Instance.GetCurrencyAmount() < int.Parse(priceText.text[1..]) || slider.value == slider.maxValue)
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
        UnityAction<int> action = MetaUpgradeSystem.Instance.DeductCurrency;
        button.onClick.AddListener(() => action(int.Parse(priceText.text[1..])));
    }

    public void UpdatePrice()
    {
        SetPriceOnButton();
    }
}