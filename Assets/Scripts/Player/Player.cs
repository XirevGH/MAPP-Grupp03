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

    public List<Item> currentItems = new List<Item>();

    private int money;
    private float moneyMultiplier;
    private float damage;
    private float areaOfEffectSize;
    private int pierce;
    private float xpMultiplier;
    public float maxHealth;
    public float health;
    private int defence;
    private float movementSpeed;
    private float xpHeld;
    private float xpToLevel;
    private int level;

    private int burstAmount;

    
    private void Start()
    {
        money = playerStats.stats.money; 
        moneyMultiplier = playerStats.stats.moneyMultiplier; 
        damage = playerStats.stats.damage;
        areaOfEffectSize = playerStats.stats.areaOfEffectSize;
        pierce = playerStats.stats.pierce;
        xpMultiplier = playerStats.stats.xpMultiplier;
        maxHealth = xpMultiplier * playerStats.stats.baseHealth; 
        health =  maxHealth;
        defence = playerStats.stats.defence; 
        movementSpeed = playerStats.stats.movementSpeed; 
        xpToLevel = 100;
        level = 1;
        burstAmount = playerStats.stats.burstAmount;
        xpHeld = 0;
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