using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteProjectile : MonoBehaviour
{
    private float damage;
    private float speed;
    
    private int penetration; 

    private Vector3 direction;

   public void Initialize(float damage, float speed, int startPenetration, Vector3 direction)
{
    this.damage = damage;
    this.speed = speed;
    this.penetration = startPenetration;  
    this.direction = direction;
}

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && penetration > 0)
        {
            other.GetComponent<Enemy>().TakeDamage(damage); // Assume Enemy has a TakeDamage method
            penetration--;
            if (penetration <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    
}