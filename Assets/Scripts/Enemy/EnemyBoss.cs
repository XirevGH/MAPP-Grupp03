using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class EnemyBoss : Enemy
{
    public EnemyBoss()
    {
    }

    protected override void FixedUpdate()
    {
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
    /*

    

    public float attckColdown;


    void FixedUpdate(){
        if(!attckColdown <= 0){
            
        }
    }

    private void AtackPlayer(){

        Instantiate()

    }*/
}
