using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public float damage;
    public bool onCooldown;
    private float cooldownDuration;
    private float cooldownTimer;

    private void Update()
    {
        if (onCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0 )
            {
                ResetCooldown();
            }
        }
    }
    public float DealDamage()
    {
        return damage;
    }
    public bool IsOnCooldown() 
    { 
        return onCooldown; 
    }

    public void StartCooldown()
    {
        cooldownTimer = cooldownDuration;
        onCooldown = true;
    }

    private void ResetCooldown()
    {
        onCooldown = false;
    }

    public void SetCooldown( float duration )
    {
        cooldownDuration = duration;
    }

    public float GetCooldown()
    {
        return cooldownDuration;
    }
}
