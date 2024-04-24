using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;
public class EnemyBoss : Enemy
{
    public float baseAblietyColdown;
    private float ablietyColdown;
    
    public EnemyBoss()
    {
        this.ablietyColdown = baseAblietyColdown;
    }

   

    protected override void FixedUpdate()
    {
        ablietyColdown -= Time.deltaTime;
        damageNumberWindow -= Time.deltaTime;
        if (!isSlow)
        {
            thisMovementSpeed = movementSpeed;
        }

        if (IsAlive()) 
        {
            if (Vector3.Distance(player.transform.position, transform.position) < 0.5)
            {
                player.GetComponent<Player>().TakeDamage(1);
            }
             if (Vector3.Distance(player.transform.position, transform.position) < 15 && ablietyColdown <= 0)
            {   
                GetComponent<DrunkerdBossAtack>().ShootNoteAttPalyer();
                ablietyColdown = baseAblietyColdown;
            }
            if (transform.position.x < player.GetComponent<Transform>().position.x)
            {
                sprite.flipX = false;
            }
            else
            {
                sprite.flipX = true;
            }
            enemyAnim.SetTrigger("Walking");
            transform.position = Vector3.MoveTowards(transform.position, player.GetComponent<Transform>().position, thisMovementSpeed / 200);
        }
      
        
    }


    

    
}

