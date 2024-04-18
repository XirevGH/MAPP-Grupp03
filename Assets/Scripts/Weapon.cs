using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public Animator anim;
    public float damage;
    public bool weaponReady;
    public float cooldownDuration;
    private float cooldownTimer;

    private void Start()
    {
        weaponReady = true;
        anim = GetComponent<Animator>();
        
    }

    protected void Update()
    {
        if (!weaponReady)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0 )
            {
               SetWeaponReady();
            }
        }
        else
        {
            Attack();
        }
    }

    public virtual void Attack()
    {
        anim.SetTrigger("Attacking");
        StartCooldown();
    }

    public void DealDamage(Collider2D other)
    {
        if(other.gameObject != null)
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
    public bool WeaponIsReady() 
    {
        return weaponReady; 
    }

    public void StartCooldown()
    {
        cooldownTimer = cooldownDuration;
        weaponReady = false;
    }

    private void SetWeaponReady()
    {
        weaponReady = true;
    }

    public void ChangeCooldownDuration(float value)
    {
        cooldownDuration += value;
    }

    public float GetCooldownDuration()
    {
        return cooldownDuration;
    }

    public void IncreaseDamage(float percentageAmount)
    {
        damage *= percentageAmount;
    }
}
