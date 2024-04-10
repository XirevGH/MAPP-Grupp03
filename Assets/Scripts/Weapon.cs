using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public float damage;
    public bool weaponReady;
    public float cooldownDuration;
    private float cooldownTimer;

    private void Start()
    {
        weaponReady = true;
    }
    private void Update()
    {
        if (!weaponReady)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0 )
            {
                Debug.Log("weapon is ready");
               SetWeaponReady();
            }
        }
    }
    public void DealDamage(Collider2D other)
    {
        other.GetComponent<Enemy>().TakeDamage(damage);
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

    public void SetCooldownDuration( float duration )
    {
        cooldownDuration = duration;
    }

    public float GetCooldownDuration()
    {
        return cooldownDuration;
    }
}
