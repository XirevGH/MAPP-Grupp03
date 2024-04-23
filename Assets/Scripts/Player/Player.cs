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
    [SerializeField] private GameObject electricGuitar, lightningAoe, bassGuitar, saxophone;
    [SerializeField] private GameController gameController;
    [SerializeField] private Player player;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private UpgradeAbility upgrade;
    [SerializeField] private UpgradePanel upgradeScreen;

    public List<Item> currentItems = new List<Item>();

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

    private void Start()
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
    }
    #region HP Stuff
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
        /*if (level == 2)
        {
            electricGuitar.SetActive(true);
        }
        if(level == 3)
        {
            lightningAoe.SetActive(true);
        }
        if(level == 4){
            saxophone.SetActive(true);
        }

        if(level > 2)
        {
            electricGuitar.GetComponent<ElectricGuitar>().UpgradeTetherAmount(1);
        }
        if(level > 3)
        {
            burstAmount += 3;
            lightningAoe.GetComponent<ParticleSystem>().emission.SetBursts(new ParticleSystem.Burst[] {new ParticleSystem.Burst(0.05f, burstAmount)});
        }
        if (level > 4)
        {
            if (level % 6 == 0)
            {
                saxophone.GetComponent<Saxophone>().IncreasePenetrationAmount(1);
                saxophone.GetComponent<Saxophone>().IncreaseProjectileCount(1);
                saxophone.GetComponent<Saxophone>().IncreaseDamage(1.1f);
            }
        }*/
    }

    private void UpdateXPSlider()
    {
        xpSlider.value = (float)xpHeld / xpToLevel;
    }
    #endregion


    private void AddItem(Item item)
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