using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Weapon startingWeapon;
    [SerializeField] private List<Item> currentItems;

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

    private bool isAlive = true;

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
        
        /*        InitializePlayer();*/
    }
    /*    private void InitializePlayer(){
            currency = 0; 
            maxHealth = 100;
            health = maxHealth;
            xpToLevel = 100;
            level = 1;
            xpHeld = 0;
            currentItems = new List<Item>{startingWeapon};
        }*/

    private void FindItems(Transform transform)
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.GetComponent<Item>() != null)
            {
                Debug.Log(child.gameObject.name);

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

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        UpdateHealthSlider();
        if(health <= 0 && isAlive)
        {
            Die();
        }
    }

    private void UpdateHealthSlider()
    {
        hpSlider.value = health / maxHealth;
    }

    private void Die()
    {
        isAlive = false;
        MetaUpgradeSystem.Instance.AddCurrency(currency);
        ResultManager.Instance.moneyEarned += currency;
        gameController.GameOver();
    }
    #endregion

    #region XP Stuff
    public void AddXP(int amountToAdd)
    {
        xpHeld += amountToAdd;
        UpdateXPSlider();
        Invoke("CheckForLevelUp", 0.8f);
    }

    private void CheckForLevelUp()
    {
        if(xpHeld >= xpToLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        xpHeld -= xpToLevel;
        xpToLevel *= 1.4f;
        level++;
        levelText.text = "Level: " + level;
        UpdateXPSlider();
        ResultManager.Instance.mainLevel = level;
        upgradeScreen.OpenUpgradeWindow();
        upgradeSystem.StartUpgradeSystem();
        Invoke("CheckForLevelUp", 0.8f);
        
    }

    private void UpdateXPSlider()
    {
        xpSlider.value = (float)xpHeld / xpToLevel;
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