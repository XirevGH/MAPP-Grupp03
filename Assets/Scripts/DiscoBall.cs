using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoBall : ProjectileWeapon
{
    private Rigidbody2D rigidbody;
    // Start is called before the first frame update
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.AddForce(new Vector2(Random.Range(-200, 200), 500 ));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Enemy"))
        {
            DealDamage(other);
        }

    }


   
}
