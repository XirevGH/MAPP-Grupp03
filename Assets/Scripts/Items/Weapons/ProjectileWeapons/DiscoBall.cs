using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoBall : Projectile
{ 
    private Rigidbody2D rb;
    public float height, distance;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce( new Vector2 (Random.Range(-distance, distance), height));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DealDamage(other, damage);
    }

    public void SetDamage(float amount)
    {
        damage = amount;
    }
}