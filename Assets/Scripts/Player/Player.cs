using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private Slider hpSlider, xpSlider;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private GameController gameController;
    [SerializeField] private Player player;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private UpgradeSystem upgradeSystem;
    [SerializeField] private UpgradePanel upgradeScreen;

    [SerializeField] private Weapon startingWeapon;
    public List<Item> currentItems;

    private int money;
    private float moneyMultiplier;
    private int damage;
    private int areaOfEffectSize;
    private int pierce;
    private float xpMultiplier;
    public float maxHealth;
    public float health;
    private int defence;
    private float movementSpeed;
    private float xpHeld;
    private float xpToLevel;
    private int level;

    private short burstAmount;

    
    private void Awake()
    {
        money = playerStats.money; 
        moneyMultiplier = playerStats.moneyMultiplier; 
        damage = playerStats.damage;
        areaOfEffectSize = playerStats.areaOfEffectSize;
        pierce = playerStats.pierce;
        xpMultiplier = playerStats.xpMultiplier;
        maxHealth = playerStats.maxHealth; 
        health = maxHealth;
        defence = playerStats.defence; 
        movementSpeed = playerStats.movementSpeed; 
        xpToLevel = 100;
        level = 1;
        burstAmount = playerStats.burstAmount;
        xpHeld = 0;
        currentItems = new List<Item>();
        currentItems.Add(startingWeapon);
    }

    //For leveling up faster for testing, remove later
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L)) 
        {
            LevelUp();
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
        if(health <= 0)
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

        gameController.GameOver();
    }
    #endregion

    #region XP Stuff
    public void AddXP(int amountToAdd)
    {
        xpHeld += amountToAdd;
        UpdateXPSlider();
        CheckForLevelUp();
    }

    private void CheckForLevelUp()
    {
        if (xpHeld >= xpToLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        xpHeld -= xpToLevel;
        xpToLevel *= 1.2f;
        level++;
        levelText.text = "Level: " + level;
        UpdateXPSlider();
        MainManager.Instance.mainLevel = level;
        upgradeScreen.OpenUpgradeWindow();
        upgradeSystem.StartUpgradeSystem();
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
}