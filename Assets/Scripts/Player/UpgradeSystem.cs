using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSystem : MonoBehaviour
{
    [Header("UI Panels (Card Choices)")]
    [SerializeField] private GameObject[] panels; // The 3 choice panels
    [SerializeField] private Player player;

    [Header("Card Background Sprites")]
    [SerializeField] private Sprite weaponPanelSprite;
    [SerializeField] private Sprite utilityPanelSprite;
    [SerializeField] private Sprite placeholderSprite;

    [Header("Inventory Rules")]
    [SerializeField] private int maxWeapons = 4;
    [SerializeField] private int maxUtilities = 3;

    // Available unowned items that can be offered as new weapons/utilities
    private List<Item> unownedSessionItems = new List<Item>();
    private List<Item> temporaryChoicePool = new List<Item>();

    private void Start()
    {
        if (player == null)
        {
            player = Player.Instance != null ? Player.Instance : FindObjectOfType<Player>();
        }

        InitializeSessionItems();
    }

    public void InitializeSessionItems()
    {
        if (player == null) return;

        // Fetch all items from the player character
        unownedSessionItems = new List<Item>(player.GetAllItems());

        // Remove starting active items from the "unowned" pool
        foreach (Item ownedItem in player.GetCurrentItems())
        {
            unownedSessionItems.Remove(ownedItem);
        }
    }

    // Called when the player levels up
    public void StartUpgradeSystem()
    {
        if (player == null) player = Player.Instance;
        if (player == null) return;

        temporaryChoicePool = new List<Item>(unownedSessionItems);
        List<Item> ownedItems = player.GetCurrentItems();

        for (int i = 0; i < panels.Length; i++)
        {
            if (panels[i] == null) continue;

            // Decide whether to offer a New Item or a Stat Upgrade
            bool canOfferNewWeapon = GetOwnedCount<Weapon>() < maxWeapons && HasAvailableItemsOfType<Weapon>();
            bool canOfferNewUtility = GetOwnedCount<Utility>() < maxUtilities && HasAvailableItemsOfType<Utility>();
            bool canOfferStatUpgrade = ownedItems.Count > 0;

            // Pick a choice type
            List<string> validTypes = new List<string>();
            if (canOfferStatUpgrade) validTypes.Add("StatUpgrade");
            if (canOfferNewWeapon) validTypes.Add("NewWeapon");
            if (canOfferNewUtility) validTypes.Add("NewUtility");

            if (validTypes.Count == 0)
            {
                panels[i].SetActive(false);
                continue;
            }

            panels[i].SetActive(true);
            string chosenType = validTypes[UnityEngine.Random.Range(0, validTypes.Count)];

            if (chosenType == "StatUpgrade")
            {
                // Pick a random owned item and roll an in-run upgrade for it
                Item randomOwnedItem = ownedItems[UnityEngine.Random.Range(0, ownedItems.Count)];
                SetupStatUpgradeCard(panels[i], randomOwnedItem);
            }
            else if (chosenType == "NewWeapon")
            {
                Item newWeapon = GetRandomUnownedItem<Weapon>();
                SetupNewItemCard(panels[i], newWeapon);
            }
            else if (chosenType == "NewUtility")
            {
                Item newUtility = GetRandomUnownedItem<Utility>();
                SetupNewItemCard(panels[i], newUtility);
            }
        }
    }

    #region Card Setup
    private void SetupNewItemCard(GameObject panel, Item item)
    {
        if (item == null || item.BaseItemData == null) return;

        ItemDefinitionSO data = item.BaseItemData;

        // UI Text
        SetPanelText(panel, data.ItemName, data.BaseDescription, $"NEW {data.GetItemType().ToUpper()}");

        // Sprites
        SetPanelSprites(panel, data);

        // Hook up the button click event (No reflection!)
        var btn = panel.GetComponent<Button>();
        if (btn == null) btn = panel.GetComponentInChildren<Button>();

        if (btn != null)
        {
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() =>
            {
                GiveNewItem(item);
                CloseUpgradePanel();
            });
        }
    }

    private void SetupStatUpgradeCard(GameObject panel, Item item)
    {
        if (item == null || item.BaseItemData == null) return;

        ItemDefinitionSO data = item.BaseItemData;

        // Pick one of the item's available upgrade definitions (Damage, Projectiles, Penetration, etc.)
        UpgradeDefinitionSO upgradeSO = null;
        if (data.AvailableMetaUpgrades != null && data.AvailableMetaUpgrades.Count > 0)
        {
            upgradeSO = data.AvailableMetaUpgrades[UnityEngine.Random.Range(0, data.AvailableMetaUpgrades.Count)];
        }

        // Dynamic Title & Description (e.g. "Saxophone: +1 Projectile" or "Electric Guitar: +1 Tether")
        string cardTitle = $"{data.ItemName}";
        string cardSubtitle = upgradeSO != null
            ? $"{upgradeSO.DisplayName}: +{upgradeSO.IncreasePerRank}{(upgradeSO.IsPercentage ? "%" : "")}"
            : "+10% Power";

        SetPanelText(panel, cardTitle, cardSubtitle, "UPGRADE");
        SetPanelSprites(panel, data);

        // Wire up the button
        var btn = panel.GetComponent<Button>();
        if (btn == null) btn = panel.GetComponentInChildren<Button>();

        if (btn != null)
        {
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() =>
            {
                ApplyStatUpgrade(item, upgradeSO);
                CloseUpgradePanel();
            });
        }
    }

    private void SetPanelText(GameObject panel, string title, string description, string category)
    {
        TMP_Text[] textComponents = panel.GetComponentsInChildren<TMP_Text>();
        if (textComponents.Length > 0) textComponents[0].text = title;
        if (textComponents.Length > 1) textComponents[1].text = description;
        if (textComponents.Length > 2) textComponents[2].text = category;
    }

    private void SetPanelSprites(GameObject panel, ItemDefinitionSO data)
    {
        var panelImage = panel.GetComponent<Image>();
        if (panelImage != null)
        {
            panelImage.sprite = data.GetItemType() == "Weapon" ? weaponPanelSprite : utilityPanelSprite;
        }

        // Set item icon
        Image[] images = panel.GetComponentsInChildren<Image>();
        if (images.Length > 1 && data.Icon != null)
        {
            images[1].sprite = data.Icon;
        }
    }
    #endregion

    #region Apply Choices
    public void GiveNewItem(Item item)
    {
        if (item == null || player == null) return;

        player.AddItem(item);
        item.EnableGameObject();
        unownedSessionItems.Remove(item);
    }

    public void ApplyStatUpgrade(Item item, UpgradeDefinitionSO upgradeSO)
    {
        if (item == null || upgradeSO == null) return;

        // Use the value defined in the ScriptableObject as the in-run card boost
        float statBoostAmount = upgradeSO.IncreasePerRank;

        switch (upgradeSO.TargetStat)
        {
            // ─────────────────────────────────────────────
            // 1. WEAPON STATS
            // ─────────────────────────────────────────────
            case StatType.Damage:
                if (item is Weapon weapon)
                {
                    weapon.IncreaseInRunDamage(statBoostAmount);
                }
                break;

            case StatType.ProjectileCount:
                if (item is ProjectileWeapon projWeapon)
                {
                    projWeapon.IncreaseInRunProjectiles(Mathf.RoundToInt(statBoostAmount));
                }
                else if (item is PermanentProjectileWeapon permWeapon)
                {
                    permWeapon.IncreaseInRunProjectiles(Mathf.RoundToInt(statBoostAmount));
                }
                break;

            case StatType.Penetration:
                if (item is ProjectileWeapon piercingWeapon)
                {
                    piercingWeapon.IncreaseInRunPenetration(Mathf.RoundToInt(statBoostAmount));
                }
                break;

            case StatType.TetherAmount:
                if (item is TetheringWeapon tetherWeapon)
                {
                    tetherWeapon.IncreaseInRunTethers(Mathf.RoundToInt(statBoostAmount));
                }
                break;

            // ─────────────────────────────────────────────
            // 2. SPATIAL & UTILITY STATS
            // ─────────────────────────────────────────────
            case StatType.Radius:
                if (item is BreakDance breakDance)
                {
                    breakDance.IncreaseInRunRadius(statBoostAmount);
                }
                else if (item is ChillVibe chillVibe)
                {
                    chillVibe.IncreaseInRunRadius(statBoostAmount);
                }
                else if (item is PersonalSpace personalSpace)
                {
                    personalSpace.IncreaseInRunRadius(statBoostAmount);
                }
                break;

            case StatType.SlowPercentage:
                if (item is ChillVibe vibe)
                {
                    vibe.IncreaseInRunSlow(statBoostAmount);
                }
                break;

            case StatType.Force:
                if (item is PersonalSpace space)
                {
                    space.IncreaseInRunForce(statBoostAmount);
                }
                break;

            case StatType.MovementSpeed:
                if (item is RollerSkates skates)
                {
                    skates.IncreaseInRunMovementSpeed(statBoostAmount);
                }
                break;

            case StatType.Health:
                if (item is GrooveArmor armor)
                {
                    armor.IncreaseInRunHealth(statBoostAmount);
                }
                break;

            case StatType.DecoyAmount:
                if (item is DecoyController decoyAmount)
                {
                    decoyAmount.IncreaseInRunDecoyAmount(Mathf.RoundToInt(statBoostAmount));
                }
                break;

            case StatType.DecoyHealth:
                if (item is DecoyController decoyHealth)
                {
                    decoyHealth.IncreaseInRunDecoyHealth(statBoostAmount);
                }
                break;

            default:
                Debug.LogWarning($"[UpgradeSystem] Unhandled StatType: {upgradeSO.TargetStat} on {item.name}");
                break;
        }
    }

    private void CloseUpgradePanel()
    {
        var upgradePanel = FindObjectOfType<UpgradePanel>(true);
        if (upgradePanel != null)
        {
            upgradePanel.CloseUpgradeWindow();
        }
    }
    #endregion

    #region Helpers
    private int GetOwnedCount<T>() where T : Item
    {
        return player != null ? player.GetCurrentItems().OfType<T>().Count() : 0;
    }

    private bool HasAvailableItemsOfType<T>() where T : Item
    {
        return temporaryChoicePool.OfType<T>().Any();
    }

    private Item GetRandomUnownedItem<T>() where T : Item
    {
        List<T> matching = temporaryChoicePool.OfType<T>().ToList();
        if (matching.Count == 0) return null;

        Item chosen = matching[UnityEngine.Random.Range(0, matching.Count)];
        temporaryChoicePool.Remove(chosen);
        return chosen;
    }
    #endregion
}