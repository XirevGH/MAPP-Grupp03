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
    [SerializeField] private GameObject electricGuitar, lightningAoe, bassGuitar, saxophon;
    [SerializeField] private GameController gameController;
    [SerializeField] private Player player;
    [SerializeField] private PlayerStats playerStats;

    private int money;
    private float moneyMultiplier;
    private int damage;
    private int areaOfEffectSize;
    private int pierce;
    private float xpMultiplier;
    private int health;
    private int defence;
    private float movementSpeed;

    private int xpHeld;
    private int xpToLevel;
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
        health = playerStats.health; 
        defence = playerStats.defence; 
        movementSpeed = playerStats.movementSpeed; 
        xpHeld = playerStats.xpHeld;  
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
        xpToLevel *= (int)1.5;
        level++;
        levelText.text = "Level: " + level;
        UpdateXPSlider();
        
        if(level == 2)
        {
            electricGuitar.SetActive(true);
        }
        if(level == 3)
        {
            lightningAoe.SetActive(true);
        }
        if(level == 4){
            saxophon.SetActive(true);
        }

        if(level > 2)
        {
            electricGuitar.GetComponent<ElectricGuitar>().UpgradeTargetAmount(1);
        }
        if(level > 3)
        {
            burstAmount += 3;
            lightningAoe.GetComponent<ParticleSystem>().emission.SetBursts(new ParticleSystem.Burst[] {new ParticleSystem.Burst(0.05f, burstAmount)});
        }
         if(level > 4)
        {
            if(level % 6 == 0){
                saxophon.GetComponent<SaxophoneWeapon>().UpgradePirceAndSpeed(1, 0.1f ,1 ,1);
            }
            
        }
    }

    private void UpdateXPSlider()
    {
        xpSlider.value = (float)xpHeld / xpToLevel;
    }
    #endregion
}