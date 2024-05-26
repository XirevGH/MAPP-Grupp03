using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.XPath;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private List<Item> allItems;
    [SerializeField] private Player player;
    [SerializeField] private Slider hpSlider;
    [SerializeField] private ParticleSystem hpLossParticles;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Weapon startingWeapon;
    [SerializeField] private List<Item> currentItems;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip levelUpSound;
    [SerializeField] private AudioClip hitSound;
    private UpgradeSystem upgradeSystem;
    private UpgradePanel upgradeScreen;
    private Slider xpSlider;
    private TMP_Text levelText;
    private GameController gameController;

    public int currency;
    public float maxHealth;
    public float health;
    public float xpHeld;
    public float xpToLevel;
    public int level;
    public GameObject confettiLeft, confettiRight, confettiCenter;

    private bool isAlive = true;
    private bool isTakingDamage = false;
    private bool vibrating = false;
    private float takingDamagePeriod;
    private float continuousDamageTime;
    private float vibrationTime;
    private ParticleSystem.MainModule particleMainModule;

    public static Player Instance
    {
        get; private set;
    }

    private void Awake()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(Instance.gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        currentItems = new List<Item> { startingWeapon };

        FindItems(transform);
        particleMainModule = hpLossParticles.main;
    }


    private void FindItems(Transform transform)
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.GetComponent<Item>() != null)
            {
                child.GetComponent<Item>().GetName();
                allItems.Add(child.GetComponent<Item>());
            }
            else
            {
                FindItems(child);
            }
        }
    }

    private void Update()
    {
        //For leveling up faster for testing, remove later
        if (Input.GetKeyDown(KeyCode.L)) 
        {
            LevelUp();
        }
        if(SceneManager.GetActiveScene().name == "Main")
        {
            if (xpSlider == null)
            {
                xpSlider = GameObject.FindGameObjectWithTag("XPSlider").GetComponent<Slider>();
            }
            if (levelText == null)
            {
                levelText = GameObject.FindGameObjectWithTag("levelText").GetComponent<TMP_Text>();
            }
            if (upgradeScreen == null)
            {
                upgradeScreen = FindObjectOfType<UpgradePanel>(true);
            }
            if (upgradeSystem == null)
            {
                upgradeSystem = FindObjectOfType<UpgradeSystem>();
            }
            if (gameController == null)
            {
                gameController = FindObjectOfType<GameController>();
            }
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
    #region HP Stuff
    public void RestoreHealth(float percent)
    {
        health += maxHealth * percent / 100;
        if(health > maxHealth)
        {
            health = maxHealth;
        }
        UpdateHealthSlider();
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

    public void TakeDamage(int damageAmount)
    {
        if (isAlive) {
            if (!vibrating)
            {
                vibrationTime = 1f;
             //   Handheld.Vibrate();
                Debug.Log("I'm vibrating");
                SoundManager.Instance.PlaySFX(hitSound, 1);
                vibrating = true;
            }

            if (vibrating && vibrationTime <= 0)
            {
                Debug.Log("I'm NOT vibrating");
                vibrating = false;
            }
            takingDamagePeriod = 0.1f;
            health -= damageAmount;
            UpdateHealthSlider();
            if(health <= 0)
            {
                Die();
            }
        }
    }

    private void ScaleParticleLifetime(float continuousDamageTime)
    {
        if (continuousDamageTime >= 2)
        {
            particleMainModule.startLifetime = 0.5f;
        }
        else if (continuousDamageTime >= 1.5)
        {
            particleMainModule.startLifetime = 0.4f;
        }
        else if (continuousDamageTime >= 1)
        {
            particleMainModule.startLifetime = 0.3f;
        }
        else if (continuousDamageTime >= 0.5 )
        {
            particleMainModule.startLifetime = 0.2f;
        }
        else if (continuousDamageTime > 0)
        {
            particleMainModule.startLifetime = 0.1f;
        }
    }

    private void StopParticles()
    {
        continuousDamageTime = 0;
        particleMainModule.startLifetime = 0f;
    }

    private void UpdateHealthSlider()
    {
        hpSlider.value = health / maxHealth;
    }

    private void Die()
    {
    //    Handheld.Vibrate();
        StopParticles();
        SoundManager.Instance.PlaySFX(deathSound, 1.5f);
        isAlive = false;
        MetaUpgradeSystem.Instance.AddCurrency(currency);
        ResultManager.Instance.moneyEarned += currency;
        ResultManager.Instance.currentItems = GetCurrentItems();
        gameController.GameOver();
    }
    #endregion

    #region XP Stuff
    public void AddXP(int amountToAdd)
    {
        xpHeld += amountToAdd;
        StartCoroutine(UpdateXPSlider());
        CheckForLevelUp();
    }

    private void CheckForLevelUp()
    {
        if (isAlive) { 
            if(xpHeld >= xpToLevel)
            {
                LevelUp();
            }
        }
    }

    public void LevelUp() // change to public to make a button reach it for testing
    {
        SoundManager.Instance.PlaySFX(levelUpSound, 1);
        xpHeld -= xpToLevel;
        xpToLevel *= 1.4f;
        level++;
        System.String[] text = levelText.text.Split(':');
        levelText.text = text[0] + ":" + level;
        StartCoroutine(UpdateXPSlider());
        ResultManager.Instance.mainLevel = level;
        GameObject confettiLeftClone = Instantiate(confettiLeft, gameController.canvasWorldSpace.transform);
        GameObject confettiRightClone = Instantiate(confettiRight, gameController.canvasWorldSpace.transform);
        GameObject confettiCenterClone = Instantiate(confettiCenter, gameController.canvasWorldSpace.transform);

        var mainModuleLeft = confettiLeftClone.GetComponent<ParticleSystem>().main;
        mainModuleLeft.useUnscaledTime = true;

        var mainModuleRight = confettiRightClone.GetComponent<ParticleSystem>().main;
        mainModuleRight.useUnscaledTime = true;

        var mainModuleCenter = confettiCenterClone.GetComponent<ParticleSystem>().main;
        mainModuleCenter.useUnscaledTime = true;
        upgradeScreen.OpenUpgradeWindow();
        upgradeSystem.StartUpgradeSystem();
    }

    //private void UpdateXPSlider()
    //{
    //    xpSlider.value = (float)xpHeld / xpToLevel;
    //}


    private IEnumerator UpdateXPSlider()
    {
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

    public void AddItem(Item item)
    {
        currentItems.Add(item);
    }

    public void AddCurrency(int addedCurrency)
    {
        currency += addedCurrency;
    }
    public List<Item> GetCurrentItems()
    {
        return new List<Item>(currentItems);
    }

    public void IncreaseMaxHealth(float percentageIncrease)
    {
        float oldMaxHealth = maxHealth;
        maxHealth *= percentageIncrease;
        health += maxHealth - oldMaxHealth;
        UpdateHealthSlider();
        Debug.Log("Health is now " + maxHealth);
    }

    public List<Item> GetAllItems()
    {
        return new List<Item>(allItems);
    }

    public bool PlayerIsAlive()
    {
        return isAlive;
    }
}