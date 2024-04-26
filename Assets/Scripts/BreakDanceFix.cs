using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakDanceFix : PhysicalWeapon
{
    public bool dealDamage;
    public bool abilityCooldown = false;
    public int amountOfHits;
    public bool usedAbility = false;
    public int hitsCounter;
    public int enemyCount;

    public void Start()
    {
        hitsCounter = amountOfHits;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {

    //    foreach (Collider2D collider in Physics2D.OverlapCircleAll(transform.position, GetComponent<CircleCollider2D>().radius * 3))
        {

            if (other.gameObject.CompareTag("Enemy"))
            {
                hitsCounter += 1;
                //   hitsCounter = enemyCount;
                //   for (int i = 0; i < hitsCounter; hitsCounter--)

                GetComponent<SpriteRenderer>().enabled = true;
                other.GetComponent<Enemy>().TakeDamage(damage);
                
             
                //   collider.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
   //     GetComponent<SpriteRenderer>().enabled = false;
        hitsCounter = amountOfHits;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
     /*   foreach (Collider2D collider in Physics2D.OverlapCircleAll(transform.position, GetComponent<CircleCollider2D>().radius * 3))
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                hitsCounter -= 1;
                for (int i = 0; i < hitsCounter; hitsCounter--)
                {
                    if (GetComponent<SpriteRenderer>().enabled == false)
                    {
                        GetComponent<SpriteRenderer>().enabled = true;
                    }

                    collider.GetComponent<Enemy>().TakeDamage(damage);
                }
                //   collider.GetComponent<Enemy>().TakeDamage(damage);
              
            }
        }
        GetComponent<SpriteRenderer>().enabled = false;
        hitsCounter = amountOfHits;*/
    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    /*
    private void OnTriggerStay2D(Collider2D other)
    {
        if(!usedAbility && !abilityCooldown)
        {
            BreakDanceInterval();
        }



        if (usedAbility && !abilityCooldown)
        {


            do
            {

                if (other.gameObject.CompareTag("Enemy") && GetComponent<SpriteRenderer>().enabled == true && hitsCounter > 0 && !abilityCooldown)
                {
                    other.GetComponent<Enemy>().TakeDamage(damage);
                    hitsCounter -= 1;

                }
                else
                {
                    hitsCounter -= 1;
                }
            }
            while (hitsCounter > 0);
        
            
               // BreakDanceInterval();
              //  hitsCounter--;
            
            if (hitsCounter <= 0)
            {
                abilityCooldown = true;
                Invoke("AbilityCooldown", 3f);
            }


          
        }
        if (GetComponent<SpriteRenderer>().enabled == true)
        {
            Invoke("RemoveIndicator", 1f);
    
        }

        
      
    }

    private void BreakDanceInterval()
    {
        usedAbility = true;
        if (!dealDamage)
        {
            dealDamage = true;
            GetComponent<SpriteRenderer>().enabled = true;
            Invoke("BreakDanceDamage", 0.5f);

        }
        if (dealDamage)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            Invoke("BreakDanceDamage", 0.5f);
        }
        if (dealDamage && GetComponent<SpriteRenderer>().enabled == false)
        {
            dealDamage = false;
            Invoke("BreakDanceDamage", 0.5f);
        }
    }

    private void BreakDanceDamage()
    {

        //   DealDamage(other
     //   hitsCounter--;
        dealDamage = false;
        Invoke("RemoveIndicator", 0.2f);
    }

    private void RemoveIndicator()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        abilityCooldown = true;
    }

    private void AbilityCooldown()
    {
        usedAbility = false;
        abilityCooldown = false;
        hitsCounter = amountOfHits;
      //  enemyCount = 0;
    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }
    */
}
