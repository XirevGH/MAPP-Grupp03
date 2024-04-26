using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushAwayAbility : MonoBehaviour
{
  
    public float pushForce = 10f;
    public float pushInterval = 3f;

    private float timer = 0f;

    private void Update()
    {

        timer += Time.deltaTime;


        if (timer >= pushInterval)
        {
            PushEnemiesAway();
            timer = 0f; 
        }
    }

    private void PushEnemiesAway()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, GetComponent<CircleCollider2D>().radius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                Vector2 direction = collider.transform.position - transform.position;

                direction.Normalize();

                Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.AddForce(direction * pushForce, ForceMode2D.Impulse);
                    StartCoroutine(RemoveForce(rb, 1f));
                }
            }
        }
    }

    private System.Collections.IEnumerator RemoveForce(Rigidbody2D rb, float delay)
    {
        yield return new WaitForSeconds(delay);
        if(rb != null)
        {
            rb.velocity = Vector2.zero;
        }
       
    }

}

