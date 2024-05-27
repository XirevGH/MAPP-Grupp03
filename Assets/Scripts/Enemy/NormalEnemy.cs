using Unity.VisualScripting;
using UnityEngine;

public class NormalEnemy : Enemy
{
    public Collider2D screenCollider;

    protected override void Start()
    {
        screenCollider = GameObject.FindGameObjectWithTag("Despawner").GetComponent<Collider2D>();
        base.Start();
    }
    void FixedUpdate()
    {

        if (Vector3.Distance(transform.position, player.transform.position) > destroyDistance)
        {
            Destroy(gameObject);
        }

        SelectTarget();
        if (IsAlive())
        {
            enemyAnim.SetTrigger("Walking");
            if (isPushedBack)
            {
                Debug.Log("I am pushed back");
                /*rb.AddForce(new Vector2(1000, 1000));*/
            }

            if (!isPushedBack)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, thisMovementSpeed / 200);
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
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other == screenCollider)
        {
         
           Destroy(gameObject);
            
        }
    }


}
