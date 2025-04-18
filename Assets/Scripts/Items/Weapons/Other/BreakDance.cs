using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class BreakDance : Weapon
{

    [SerializeField] private float radiusIncreasePercentage;
    private Animator anim;
    public int radiusRank;
    public int radiusUpgradeCost;

    private HashSet<GameObject> enemies = new HashSet<GameObject>();

    private void Start()
    {
        UnityAction action = new UnityAction(Attack);
        TriggerController.Instance.SetTrigger(beatNumber, action);
        anim = GetComponent<Animator>();
    }

    public void IncreaseRadius()
    {
        radiusRank++;
        gameObject.transform.localScale *= (1 + (radiusIncreasePercentage / 100f));
    }

    public float GetRadiusIncreasePercentage()
    {
        return radiusIncreasePercentage;
    }

    public float GetCurrentRadiusIncrease()
    {
        return (float)Math.Round((Mathf.Pow(1 + (radiusIncreasePercentage / 100f), radiusRank) - 1) * 100, 1);
    }

    public int GetIncreaseRadiusCost()

    {
        return radiusUpgradeCost;
    }

    public int GetRadiusUpgradeRank()
    {
        return radiusRank;
    }

    protected override void CreateUpgradeOptions()
    {
        base.CreateUpgradeOptions();
        upgradeOptions.Add("IncreaseRadius");
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemies.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        enemies.Remove(other.gameObject);
    }

    public override void Attack()
    {
        if (gameObject.activeSelf)
        {
            anim.SetTrigger("Attack");
            SoundManager.Instance.PlaySFX(attackSound, 1);
            foreach (GameObject enemy in enemies)
            {
                if (enemy.gameObject != null)
                {
                    enemy.GetComponent<Enemy>().TakeDamage(damage);
                }
            }
        }
    }
}
