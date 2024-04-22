using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoBall : MonoBehaviour
{ 
    private Rigidbody2D rb;
    public float height, distance;

    private float damage;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce( new Vector2 (Random.Range(-distance, distance), height));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // TODO Replace this with DealDamage from future superclass projectile
            other.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

    public void SetDamage(float amount)
    {
        damage = amount;
    }
}