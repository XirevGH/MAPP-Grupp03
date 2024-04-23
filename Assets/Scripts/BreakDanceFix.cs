using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakDanceFix : PhysicalWeapon
{
    public bool dealDamage;
    public int amountOfHits;
    public bool usedAbility = false;
    private int hitsCounter;
    Collider2D other;

    private void OnTriggerStay2D(Collider2D other)
    {
        if(!usedAbility)
        { 
            hitsCounter = amountOfHits;
            while (hitsCounter > 0)
            {
                BreakDanceInterval();
            }
            if (hitsCounter <= 0)
            {
                usedAbility = true;
            }
        }
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
        GetComponent<SpriteRenderer>().enabled = true;
        dealDamage = false;
        Invoke("RemoveIndicator", 0.5f);
        Invoke("AbilityCooldown", 3f);
    }

    private void RemoveIndicator()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }

    private void AbilityCooldown()
    {
        usedAbility = false;
    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }
}

