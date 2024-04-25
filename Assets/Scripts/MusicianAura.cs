using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicianAura : Weapon
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        //    foreach (Collider2D collider in Physics2D.OverlapCircleAll(transform.position, GetComponent<CircleCollider2D>().radius * 3))
        

            if (other.gameObject.CompareTag("Enemy"))
            {

                GetComponent<SpriteRenderer>().enabled = true;
                other.GetComponent<Enemy>().TakeDamage(damage);


                //   collider.GetComponent<Enemy>().TakeDamage(damage);
            }
        
       
        
    }

 /*   private void OnTriggerExit2D(Collider2D other)
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

    }*/

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }
}
