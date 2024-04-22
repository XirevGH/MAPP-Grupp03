using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakDance : Weapon
{
    public bool dealDamage;
    public int amountOfHits;
    private int hitsCounter;
    Collider2D other;   

    private void OnTriggerStay2D(Collider2D other)
    {
        if (WeaponIsReady()) {
            hitsCounter = amountOfHits;
            while (hitsCounter > 0)
                {
                    BreakDanceInterval();
                }
            StartCooldown();
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
        this.GetComponent<SpriteRenderer>().enabled = true;
        DealDamage(other);
        hitsCounter--;
        dealDamage = false;
        Invoke("RemoveIndicator", 0.5f);
    }

    private void RemoveIndicator()
    {
        this.GetComponent<SpriteRenderer>().enabled = false;
    }
}
