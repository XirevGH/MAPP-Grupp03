using UnityEngine;


public class NormalEnemy : Enemy
{
    public NormalEnemy()
    {
    }

    void FixedUpdate()
    {
        damageNumberWindow -= Time.deltaTime;
        

       
        if (IsAlive())
        {
            if (Vector3.Distance(target.transform.position, transform.position) < 0.5)
            {
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
    protected override void DestroyGameObject()
    {
        Drops();
        MainManager.Instance.enemiesDefeated += 1;
        Destroy(gameObject);
    }
}
