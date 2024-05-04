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
    [SerializeField] private PlayerStats characterStats;
    [SerializeField] private UpgradeSystem upgradeSystem;
    [SerializeField] private UpgradePanel upgradeScreen;

    public List<Item> currentItems = new List<Item>();

    private float timer = 0f;
    private const float duration = 10f;  
    private bool timerActive = true;

    private float xpMultiplier;
    public float maxHealth;
    public float health;
    private int defence;

    private int regen; 
    private float xpHeld;
    private float xpToLevel;
    private int level;


    
    public void InitializePlayerStats()
    {
        xpMultiplier = characterStats.stats.xpMultiplier;
        maxHealth =  characterStats.stats.healthMultiplier * characterStats.stats.baseHealth; 
        health =  maxHealth;
        defence = characterStats.stats.defence;  
        regen = characterStats.stats.regeneration;


        xpToLevel = 100;
        level = 1;
        xpHeld = 0;
    }
    
    private void FixedUpdate() {
         if (timerActive)
        {
            timer += Time.fixedDeltaTime;  

            if (timer >= duration)
            {
                RestoreHealth(regen); 
                timer = 0f; 
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
    {   if(damageAmount > defence){
            health -= damageAmount - defence;
        }else{
            health -= 1;
        }
        
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
        MainManager.Instance.mony = characterStats.stats.money;
        GlobalUpgrades.Instance.upgradeStats.currencies[0] += characterStats.stats.money;
        GlobalUpgrades.Instance.SaveToPlayerPrefs();
        gameController.GameOver();
    }
    #endregion

    #region XP Stuff
    public void AddXP(int amountToAdd)
    {
        xpHeld += (int)(amountToAdd * xpMultiplier);
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