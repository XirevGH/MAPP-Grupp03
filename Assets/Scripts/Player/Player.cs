using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class Player : MonoBehaviour
{
    [Header("Combat & Health")]
    [SerializeField] private float maxHealth = 100f;
    public float health;
    public int currency;
    public float xpHeld;
    public float xpToLevel = 100f;
    public int level = 1;

    [Header("Loadout")]
    [SerializeField] private Weapon startingWeapon;
    [SerializeField] private List<Item> currentItems = new List<Item>();
    [SerializeField] private List<Item> allItems = new List<Item>();

    [Header("Direct Scene UI References")]
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Slider xpSlider;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private UpgradePanel upgradeScreen;
    [SerializeField] private UpgradeSystem upgradeSystem;
    [SerializeField] private GameController gameController;
    [SerializeField] private LocalizedString localizedLevelFormat = new LocalizedString("StringTable", "ui_level_format");


    [Header("Audio & FX")]
    [SerializeField] private ParticleSystem hpLossParticles;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip levelUpSound;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private GameObject confettiLeft, confettiRight, confettiCenter;

    [Header("Damage & FX Settings")]
    private float takingDamagePeriod = 0f;
    private float continuousDamageTime = 0f;
    private bool isTakingDamage = false;

    private bool isAlive = true;
    private bool vibrating = false;
    private float vibrationTime;
    private ParticleSystem.MainModule particleMainModule;



    public static Player Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        allItems = new List<Item>(GetComponentsInChildren<Item>(true));
        if (startingWeapon != null)
        {
            currentItems = new List<Item> { startingWeapon };
        }

        if (hpLossParticles != null)
        {
            particleMainModule = hpLossParticles.main;
        }
    }

    private void OnDestroy()
    {
        LocalizationSettings.SelectedLocaleChanged -= OnLocaleChanged;

        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        health = maxHealth;
        isAlive = true;
        UpdateHealthSlider();
        UpdateLevelDisplay();

        LocalizationSettings.SelectedLocaleChanged += OnLocaleChanged;

    }

    private void OnLocaleChanged(Locale newLocale)
    {
        UpdateLevelDisplay();
    }

    public void UpdateLevelDisplay()
    {
        if (levelText != null)
        {
            if (!localizedLevelFormat.IsEmpty)
            {
                levelText.text = localizedLevelFormat.GetLocalizedString(level);
            }
            else
            {
                levelText.text = $"Level: {level}";
            }
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LevelUp();
        }

        takingDamagePeriod -= Time.deltaTime;

        if (takingDamagePeriod > 0)
        {
            ColorPlayerSprite(true);
            continuousDamageTime += Time.deltaTime;
            ScaleParticleLifetime(continuousDamageTime);
        }

        if (takingDamagePeriod <= 0)
        {
            ColorPlayerSprite(false);
            StopParticles();
        }

        if (vibrating)
        {
            vibrationTime -= Time.deltaTime;
        }
    }


    public void TakeDamage(int damageAmount)
    {
        if (isAlive)
        {
            if (!vibrating)
            {
                vibrationTime = 1f;
                Handheld.Vibrate();
                if (SoundManager.Instance != null && hitSound != null)
                {
                    SoundManager.Instance.PlaySFX(hitSound, 1f);
                }
                vibrating = true;
            }

            if (vibrating && vibrationTime <= 0)
            {
                vibrating = false;
            }

            takingDamagePeriod = 0.1f;
            health -= damageAmount;
            UpdateHealthSlider();

            if (health <= 0)
            {
                Die();
            }
        }
    }

    private void ColorPlayerSprite(bool damagePeriod)
    {
        if (damagePeriod && !isTakingDamage)
        {
            isTakingDamage = true;
            LeanTween.color(gameObject, Color.red, 0.25f);
        }

        if (!damagePeriod && isTakingDamage)
        {
            isTakingDamage = false;
            LeanTween.color(gameObject, Color.white, 0.25f);
        }
    }

    private void ScaleParticleLifetime(float duration)
    {
        if (hpLossParticles == null) return;

        if (duration >= 2f) particleMainModule.startLifetime = 0.5f;
        else if (duration >= 1.5f) particleMainModule.startLifetime = 0.4f;
        else if (duration >= 1.0f) particleMainModule.startLifetime = 0.3f;
        else if (duration >= 0.5f) particleMainModule.startLifetime = 0.2f;
        else particleMainModule.startLifetime = 0.15f;
    }

    #region Health & Combat
    public void RestoreHealth(float percent)
    {
        health = Mathf.Min(maxHealth, health + (maxHealth * percent / 100f));
        UpdateHealthSlider();
    }

    public void IncreaseMaxHealth(float percentageMultiplier)
    {
        float oldMaxHealth = maxHealth;
        maxHealth *= percentageMultiplier;
        health += (maxHealth - oldMaxHealth);
        UpdateHealthSlider();
    }

    private void StopParticles()
    {
        continuousDamageTime = 0;
        particleMainModule.startLifetime = 0f;
    }

    private void UpdateHealthSlider()
    {
        if (hpSlider != null) hpSlider.value = health / maxHealth;
    }

    // Inside Player.cs:
    private void Die()
    {
        isAlive = false;
        StopParticles();

        if (SoundManager.Instance != null && deathSound != null)
        {
            SoundManager.Instance.PlaySFX(deathSound, 1.5f);
        }

        if (MetaUpgradeManager.Instance != null)
        {
            MetaUpgradeManager.Instance.AddCurrency(currency);
        }

        if (ResultManager.Instance != null)
        {
            ResultManager.Instance.moneyEarned += currency;

            List<ItemDefinitionSO> itemsToSave = new List<ItemDefinitionSO>();
            foreach (Item item in currentItems)
            {
                if (item != null && item.BaseItemData != null)
                {
                    itemsToSave.Add(item.BaseItemData);
                }
            }
            ResultManager.Instance.savedItemData = itemsToSave;
        }

        if (gameController != null)
        {
            gameController.GameOver();
        }
    }
    #endregion

    #region XP & Leveling
    public void AddXP(int amountToAdd)
    {
        xpHeld += amountToAdd;
        StartCoroutine(UpdateXPSliderRoutine());

        if (isAlive && xpHeld >= xpToLevel)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        if (SoundManager.Instance != null && levelUpSound != null)
        {
            SoundManager.Instance.PlaySFX(levelUpSound, 1f);
        }

        xpHeld -= xpToLevel;
        xpToLevel *= 1.4f;
        level++;

        UpdateLevelDisplay();

        StartCoroutine(UpdateXPSliderRoutine());

        if (ResultManager.Instance != null)
        {
            ResultManager.Instance.mainLevel = level;
        }

        SpawnConfetti();

        if (upgradeScreen != null) upgradeScreen.OpenUpgradeWindow();
        if (upgradeSystem != null) upgradeSystem.StartUpgradeSystem();
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

    private IEnumerator UpdateXPSliderRoutine()
    {
        if (xpSlider == null) yield break;

        float elapsedTime = 0;
        float timeToChange = 0.5f;
        float currentValue = xpSlider.value;
        float nextValue = xpHeld / xpToLevel;

        while (elapsedTime <= timeToChange)
        {
            elapsedTime += Time.unscaledDeltaTime;
            xpSlider.value = Mathf.Lerp(currentValue, nextValue, elapsedTime / timeToChange);
            yield return null;
        }
    }
    #endregion

    public void AddItem(Item item) => currentItems.Add(item);
    public void AddCurrency(int added) => currency += added;
    public List<Item> GetCurrentItems() => new List<Item>(currentItems);
    public List<Item> GetAllItems() => new List<Item>(allItems);
    public bool PlayerIsAlive() => isAlive;
}