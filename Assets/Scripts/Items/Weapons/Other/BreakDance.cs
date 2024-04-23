using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakDance : PhysicalWeapon
{
    public bool dealDamage;
    public float abilityTime;
    public int amountOfHits;

    private int hitsCounter;
    private bool canAttack;
    private Collider2D other;   

    private void OnTriggerStay2D(Collider2D other)
    {
        if(canAttack)
        {
            hitsCounter = amountOfHits;
            while (hitsCounter > 0)
            {
                BreakDanceInterval();
            }
            canAttack = false;
        }
    }
     
    public override void Attack()
    {
        canAttack = true;
    }

    private void BreakDanceInterval()
    {
        if (!dealDamage) 
        {
            dealDamage = true;
            Invoke("BreakDanceDamage", 0.5f);
        }
    }

    private void BreakDanceDamage()
    {
        DealDamage(other);
        hitsCounter--;
        dealDamage = false;
    }
}
