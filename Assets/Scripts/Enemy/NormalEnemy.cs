using UnityEngine;


public class NormalEnemy : Enemy
{
    void FixedUpdate()
    {
        SelectTarget();
        if (IsAlive())
        {
            if (Vector2.Distance(target.transform.position, transform.position) < 0.5)
            {   enemyAnim.SetTrigger("Attack");
                player.GetComponent<Player>().TakeDamage(1);
                
            }

            if (Vector2.Distance(player.transform.position, transform.position) < 1)
            {   enemyAnim.SetTrigger("Attack");
                player.GetComponent<Player>().TakeDamage(1);
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
    
}
