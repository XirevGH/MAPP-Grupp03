using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;
using static UnityEngine.InputManagerEntry;
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

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other == screenCollider)
        {
         
           Destroy(gameObject);
            
        }
    }


}
