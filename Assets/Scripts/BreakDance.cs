using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakDance : Weapon
{
    public bool dealDamage;
    public float abilityTime;
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
            //StartCooldown();
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
        dealDamage = false;
    }
}
