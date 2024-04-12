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
    [SerializeField] private GameObject electricGuitar, lightningAoe, bassGuitar;

    public int money;
    public float moneyMultiplier;
    public int damage;
    public int areaOfEffectSize;
    public int pierce;
    public float xpMultiplier;
    public int health;
    public int defence;
    public float movementSpeed;

    private int xpHeld;
    private int xpToLevel = 300;
    public int level = 1;

    private short burstAmount = 3;

    public string SaveToString()
    {
        return JsonUtility.ToJson(this);
    }

    public static Player CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<Player>(jsonString);
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
        SceneManager.LoadScene("MainMenu");
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
        if(level > 2)
        {
            electricGuitar.GetComponent<ElectricGuitar>().UpgradeTargetAmount(1);
        }
        if(level > 3)
        {
            burstAmount += 3;
            lightningAoe.GetComponent<ParticleSystem>().emission.SetBursts(new ParticleSystem.Burst[] {new ParticleSystem.Burst(0.05f, burstAmount)});
            electricGuitar.GetComponent<Weapon>().ChangeCooldownDuration(0.2f);
            bassGuitar.GetComponent<Weapon>().ChangeCooldownDuration(0.2f);
            lightningAoe.GetComponent<Weapon>().ChangeCooldownDuration(0.2f);
        }
    }

    private void UpdateXPSlider()
    {
        Debug.Log(xpHeld / xpToLevel);
        xpSlider.value = (float)xpHeld / xpToLevel;
    }
    #endregion
}