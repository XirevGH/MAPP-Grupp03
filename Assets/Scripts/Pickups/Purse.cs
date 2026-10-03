using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Purse : Pickup
{
    [Header("Audio & Visuals")]
    [SerializeField] private AudioClip sfxClip;
    [SerializeField] private GameObject confettiLeft;
    [SerializeField] private GameObject confettiRight;
    [SerializeField] private GameObject confettiCenter;

    private UpgradeSystem upgradeSystem;
    private GameController gameController;
    private PursePanel pursePanel;
    private GameObject pursePanelGameObject;
    private int moneyAmount;

    private void Start()
    {
        upgradeSystem = FindObjectOfType<UpgradeSystem>();
        gameController = FindObjectOfType<GameController>();
        pursePanel = FindObjectOfType<PursePanel>(true);

        if (gameController != null)
        {
            pursePanelGameObject = gameController.pursePanel;
        }

        moneyAmount = GenerateRandomMoney();
    }

    protected override void IndividualPickupAction()
    {
        if (pursePanel != null)
        {
            pursePanel.OpenPurseWindow();
        }

        SpawnConfetti();

        if (SoundManager.Instance != null && sfxClip != null)
        {
            SoundManager.Instance.PlaySFX(sfxClip, 1f);
        }

        if (player != null)
        {
            player.AddCurrency(moneyAmount);
        }

        UpgradeRandomOwnedItem();
    }

    private void UpgradeRandomOwnedItem()
    {
        if (player == null || upgradeSystem == null) return;

        List<Item> ownedItems = player.GetCurrentItems();
        if (ownedItems == null || ownedItems.Count == 0) return;

        Item randomItem = ownedItems[Random.Range(0, ownedItems.Count)];
        if (randomItem == null || randomItem.BaseItemData == null) return;

        ItemDefinitionSO itemData = randomItem.BaseItemData;

        UpgradeDefinitionSO chosenUpgradeSO = null;
        if (itemData.AvailableMetaUpgrades != null && itemData.AvailableMetaUpgrades.Count > 0)
        {
            chosenUpgradeSO = itemData.AvailableMetaUpgrades[Random.Range(0, itemData.AvailableMetaUpgrades.Count)];
        }

        if (chosenUpgradeSO != null)
        {
            upgradeSystem.ApplyStatUpgrade(randomItem, chosenUpgradeSO);
        }

        UpdatePursePopupUI(randomItem, chosenUpgradeSO);
    }

    private void UpdatePursePopupUI(Item item, UpgradeDefinitionSO upgradeSO)
    {
        if (pursePanelGameObject == null || item == null || item.BaseItemData == null) return;

        ItemDefinitionSO data = item.BaseItemData;

        TMP_Text[] textComponents = pursePanelGameObject.GetComponentsInChildren<TMP_Text>();
        if (textComponents.Length > 0)
        {
            textComponents[0].text = data.ItemName;
        }
        if (textComponents.Length > 1)
        {
            string bonusText = upgradeSO != null
                ? $"{upgradeSO.DisplayName}: +{upgradeSO.IncreasePerRank}{(upgradeSO.IsPercentage ? "%" : "")}"
                : "+10% Power";

            textComponents[1].text = bonusText;
        }
        if (textComponents.Length > 2)
        {
            textComponents[2].text = $"+${moneyAmount} COINS & UPGRADE!";
        }

        Image[] images = pursePanelGameObject.GetComponentsInChildren<Image>();
        if (images.Length > 1 && data.Icon != null)
        {
            images[1].sprite = data.Icon;
        }
    }

    private void SpawnConfetti()
    {
        if (gameController == null || gameController.canvasWorldSpace == null) return;

        Transform canvasTransform = gameController.canvasWorldSpace.transform;

        if (confettiLeft != null)
        {
            GameObject leftClone = Instantiate(confettiLeft, canvasTransform);
            var mainModule = leftClone.GetComponent<ParticleSystem>().main;
            mainModule.useUnscaledTime = true;
        }

        if (confettiRight != null)
        {
            GameObject rightClone = Instantiate(confettiRight, canvasTransform);
            var mainModule = rightClone.GetComponent<ParticleSystem>().main;
            mainModule.useUnscaledTime = true;
        }

        if (confettiCenter != null)
        {
            GameObject centerClone = Instantiate(confettiCenter, canvasTransform);
            var mainModule = centerClone.GetComponent<ParticleSystem>().main;
            mainModule.useUnscaledTime = true;
        }
    }

    private int GenerateRandomMoney()
    {
        return Random.Range(50, 501);
    }

    protected override void ResetThis()
    {
        moneyAmount = GenerateRandomMoney();
    }
}