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

    private List<Weapon> currentWeapons;
    private int money;
    private float moneyMultiplier;
    private int damage;
    private int areaOfEffectSize;
    private int pierce;
    private float xpMultiplier;
    public float health;
    private int defence;
    private float movementSpeed;
    private float xpHeld;
    private float xpToLevel;
    private int level;

    private short burstAmount;

    private void Start()
    {
        currentWeapons = new List<Weapon>();

        money = playerStats.money; 
        moneyMultiplier = playerStats.moneyMultiplier; 
        damage = playerStats.damage;
        areaOfEffectSize = playerStats.areaOfEffectSize;
        pierce = playerStats.pierce;
        xpMultiplier = playerStats.xpMultiplier;
        health = 100; 
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
        hpSlider.value = health / 100f;
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

        if (level == 2)
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
                saxophone.GetComponent<Saxophone>().IncreaseTargetCount(1);
                saxophone.GetComponent<Saxophone>().IncreaseDamage(1.1f);
            }
        }
    }

    private void UpdateXPSlider()
    {
        xpSlider.value = (float)xpHeld / xpToLevel;
    }
    #endregion

    #region Weapon Stuff
    private void AddWeapon(Weapon weapon)
    {
        currentWeapons.Add(weapon);
    }

    public List<Weapon> GetCurrentWeapons()
    {
        return new List<Weapon>(currentWeapons);
    }
    #endregion

    public void IncreaseMovementSpeed(float percentageIncrease)
    {
        movementSpeed *= percentageIncrease;
        Debug.Log("Movement Speed is now " + movementSpeed);
    }

    public void IncreaseHealth(float percentageIncrease)
    {
        health *= percentageIncrease;
        Debug.Log("Health is now " + movementSpeed);
    }
}