using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

public class EnemyBoss : Enemy
{
    public float baseAttackColdown;
    private float attackColdown;

    private bool bossChargActiv;
    private float bossChargColdown;

    public float bossChargSpeed;


    public EnemyBoss()
    {
        this.bossChargColdown = baseAttackColdown/2;
        this.attackColdown = baseAttackColdown;
        this.bossChargActiv = false;
        EnemySpawner.bossAlive = true;
    }

   

    void FixedUpdate()
    {
        attackColdown -= Time.deltaTime;
        damageNumberWindow -= Time.deltaTime;
        if(bossChargActiv){
            bossChargColdown -= Time.deltaTime;
        }
        if (!isSlow)
        {
            thisMovementSpeed = movementSpeed;
        }

        if (IsAlive()) 
        {
            if (GameObject.FindGameObjectWithTag("Decoy") != null && GameObject.FindGameObjectWithTag("Decoy").activeInHierarchy)
            {
                target = GameObject.FindGameObjectWithTag("Decoy");

            }
            else
            {
                target = GameObject.FindGameObjectWithTag("Player");
            }

            if (Vector3.Distance(player.transform.position, transform.position) < 1)
            {
                player.GetComponent<Player>().TakeDamage(1);
            }
             if (Vector3.Distance(player.transform.position, transform.position) < 15 && attackColdown <= 0)
            {   enemyAnim.SetTrigger("Attack");
                GetComponent<DrunkerdBossAtack>().ShootNoteAttPalyer();
                attackColdown = baseAttackColdown;

                bossChargActiv = true;
            }

            if(bossChargColdown <= 0){
                bossChargActiv = false;
                bossChargColdown = baseAttackColdown/2;
            }



            if (transform.position.x < target.transform.position.x)
            {
                sprite.flipX = false;
            }
            else
            {
                sprite.flipX = true;
            }

            enemyAnim.SetTrigger("Walking");
            

            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, thisMovementSpeed / 200);

        }
      
        
    }
    protected override void DestroyGameObject()
    {
        EnemySpawner.bossAlive = false;
        Drops();
        MainManager.Instance.enemiesDefeated += 1;
        Destroy(gameObject);
    }
    

    
}

