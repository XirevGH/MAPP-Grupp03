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
        playerStats.health -= damageAmount;
        UpdateHealthSlider();
        if(playerStats.health <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthSlider()
    {
        hpSlider.value = playerStats.health / 100f;
    }

    private void Die()
    {

        gameController.GameOver();
    }
    #endregion

    #region XP Stuff
    public void AddXP(int amountToAdd)
    {
        playerStats.xpHeld += amountToAdd;
        UpdateXPSlider();
        CheckForLevelUp();
    }

    private void CheckForLevelUp()
    {
        if (playerStats.xpHeld >= playerStats.xpToLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        playerStats.xpHeld -= playerStats.xpToLevel;
        playerStats.xpToLevel *= (int)1.5;
        playerStats.level++;
        levelText.text = "Level: " + playerStats.level;
        UpdateXPSlider();
        
        if(playerStats.level == 2)
        {
            electricGuitar.SetActive(true);
        }
        if(playerStats.level == 3)
        {
            lightningAoe.SetActive(true);
        }
        if(playerStats.level == 4){
            saxophon.SetActive(true);
        }

        if(playerStats.level > 2)
        {
            electricGuitar.GetComponent<ElectricGuitar>().UpgradeTargetAmount(1);
        }
        if(playerStats.level > 3)
        {
            playerStats.burstAmount += 3;
            lightningAoe.GetComponent<ParticleSystem>().emission.SetBursts(new ParticleSystem.Burst[] {new ParticleSystem.Burst(0.05f, playerStats.burstAmount)});
        }
         if(playerStats.level > 4)
        {
            saxophon.GetComponent<SaxophoneWeapon>().UpgradePirceAndSpeed(1,5);
        }
    }

    private void UpdateXPSlider()
    {
        xpSlider.value = (float)playerStats.xpHeld / playerStats.xpToLevel;
    }
    #endregion
}