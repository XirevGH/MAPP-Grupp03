using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicianAura : Weapon
{

    private void OnTriggerEnter2D(Collider2D other)
    {
            if (other.gameObject.CompareTag("Enemy"))
            {
                GetComponent<SpriteRenderer>().enabled = true;
                other.GetComponent<Enemy>().TakeDamage(damage);
            }  
    }

 /*  private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {

            other.GetComponent<Enemy>().TakeDamage(damage);

        }

    }*/

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }
}
