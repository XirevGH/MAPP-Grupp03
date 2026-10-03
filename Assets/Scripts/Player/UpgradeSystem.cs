using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

public class UpgradeSystem : MonoBehaviour
{
    [Header("UI Panels (Card Choices)")]
    [SerializeField] private GameObject[] panels;
    [SerializeField] private Player player;

    [Header("Card Background Sprites")]
    [SerializeField] private Sprite weaponPanelSprite;
    [SerializeField] private Sprite utilityPanelSprite;

    [Header("Localized Category Badges")]
    [SerializeField] private LocalizedString newWeaponBadge = new LocalizedString("StringTable", "badge_new_weapon");
    [SerializeField] private LocalizedString newUtilityBadge = new LocalizedString("StringTable", "badge_new_utility");
    [SerializeField] private LocalizedString upgradeBadge = new LocalizedString("StringTable", "badge_upgrade");

    [Header("Inventory Rules")]
    [SerializeField] private int maxWeapons = 4;
    [SerializeField] private int maxUtilities = 3;

    private List<Item> unownedSessionItems = new List<Item>();
    private List<Item> temporaryChoicePool = new List<Item>();

    private void Start()
    {
        if (player == null) player = Player.Instance != null ? Player.Instance : FindObjectOfType<Player>();
        InitializeSessionItems();
    }

    public void InitializeSessionItems()
    {
        if (player == null) return;

        unownedSessionItems = new List<Item>(player.GetAllItems());
        foreach (Item ownedItem in player.GetCurrentItems())
        {
            unownedSessionItems.Remove(ownedItem);
        }
    }

    public void StartUpgradeSystem()
    {
        if (player == null) player = Player.Instance;
        if (player == null) return;

        temporaryChoicePool = new List<Item>(unownedSessionItems);
        List<Item> ownedItems = player.GetCurrentItems();

        // 1. Build a temporary pool of all available stat upgrades for owned items
        List<(Item item, UpgradeDefinitionSO upgrade)> availableUpgradePool = new List<(Item, UpgradeDefinitionSO)>();
        foreach (Item ownedItem in ownedItems)
        {
            if (ownedItem != null && ownedItem.BaseItemData != null && ownedItem.BaseItemData.AvailableMetaUpgrades != null)
            {
                foreach (var upg in ownedItem.BaseItemData.AvailableMetaUpgrades)
                {
                    availableUpgradePool.Add((ownedItem, upg));
                }
            }
        }

        for (int i = 0; i < panels.Length; i++)
        {
            if (panels[i] == null) continue;

            bool canOfferNewWeapon = GetOwnedCount<Weapon>() < maxWeapons && HasAvailableItemsOfType<Weapon>();
            bool canOfferNewUtility = GetOwnedCount<Utility>() < maxUtilities && HasAvailableItemsOfType<Utility>();
            bool canOfferStatUpgrade = availableUpgradePool.Count > 0;

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
            string chosenType = validTypes[Random.Range(0, validTypes.Count)];

            if (chosenType == "StatUpgrade")
            {
                int randomIndex = Random.Range(0, availableUpgradePool.Count);
                var (chosenItem, chosenUpgrade) = availableUpgradePool[randomIndex];

                availableUpgradePool.RemoveAt(randomIndex);

                SetupStatUpgradeCard(panels[i], chosenItem, chosenUpgrade);
            }
            else if (chosenType == "NewWeapon")
            {
                Item newWeapon = GetRandomUnownedItem<Weapon>();
                SetupNewItemCard(panels[i], newWeapon, true);
            }
            else if (chosenType == "NewUtility")
            {
                Item newUtility = GetRandomUnownedItem<Utility>();
                SetupNewItemCard(panels[i], newUtility, false);
            }
        }
    }

    private void SetupNewItemCard(GameObject panel, Item item, bool isWeapon)
    {
        if (item == null || item.BaseItemData == null) return;

        ItemDefinitionSO data = item.BaseItemData;

        string badgeText = isWeapon ? newWeaponBadge.GetLocalizedString() : newUtilityBadge.GetLocalizedString();

        SetPanelText(panel, data.ItemName, data.BaseDescription, badgeText);
        SetPanelSprites(panel, data);

        Button btn = panel.GetComponent<Button>();
        if (btn == null) btn = panel.GetComponentInChildren<Button>();
        if (btn == null) btn = panel.AddComponent<Button>();

        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() =>
        {
            GiveNewItem(item);
            CloseUpgradePanel();
        });
    }

    private void SetupStatUpgradeCard(GameObject panel, Item item, UpgradeDefinitionSO upgradeSO)
    {
        if (item == null || item.BaseItemData == null) return;

        ItemDefinitionSO data = item.BaseItemData;

        string upgradeDesc = upgradeSO != null ? upgradeSO.GetFormattedDescription(1) : "+10%";
        string badgeText = upgradeBadge.GetLocalizedString();

        SetPanelText(panel, data.ItemName, upgradeDesc, badgeText);
        SetPanelSprites(panel, data);

        Button btn = panel.GetComponent<Button>();
        if (btn == null) btn = panel.GetComponentInChildren<Button>();
        if (btn == null) btn = panel.AddComponent<Button>();

        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() =>
        {
            ApplyStatUpgrade(item, upgradeSO);
            CloseUpgradePanel();
        });
    }

    private void SetPanelText(GameObject panel, string title, string description, string badge)
    {
        TMP_Text[] textComponents = panel.GetComponentsInChildren<TMP_Text>();
        if (textComponents.Length > 0) textComponents[0].text = title;
        if (textComponents.Length > 1) textComponents[1].text = description;
        if (textComponents.Length > 2) textComponents[2].text = badge;
    }

    private void SetPanelSprites(GameObject panel, ItemDefinitionSO data)
    {
        var panelImage = panel.GetComponent<Image>();
        if (panelImage != null)
        {
            panelImage.sprite = data.GetItemType() == "Weapon" ? weaponPanelSprite : utilityPanelSprite;
        }

        Image[] images = panel.GetComponentsInChildren<Image>();
        if (images.Length > 1 && data.Icon != null)
        {
            images[1].sprite = data.Icon;
        }
    }

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

        float statBoostAmount = upgradeSO.IncreasePerRank;

        switch (upgradeSO.TargetStat)
        {
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
        if (upgradePanel != null) upgradePanel.CloseUpgradeWindow();
    }

    private int GetOwnedCount<T>() where T : Item => player != null ? player.GetCurrentItems().OfType<T>().Count() : 0;
    private bool HasAvailableItemsOfType<T>() where T : Item => temporaryChoicePool.OfType<T>().Any();

    private Item GetRandomUnownedItem<T>() where T : Item
    {
        List<T> matching = temporaryChoicePool.OfType<T>().ToList();
        if (matching.Count == 0) return null;
        Item chosen = matching[Random.Range(0, matching.Count)];
        temporaryChoicePool.Remove(chosen);
        return chosen;
    }
}