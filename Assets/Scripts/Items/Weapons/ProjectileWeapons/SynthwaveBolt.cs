using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynthwaveBolt : Projectile
{
    private void Start()
    {
        Shoot();
    }

    void Shoot()
    {
        transform.localScale = new Vector2(0, transform.localScale.y);
    }

    void MoveBolt()
    {
        if (transform.localScale.x < 1)
        {
            transform.localScale = new Vector2(transform.localScale.x + 0.2f, transform.localScale.y);
        }
        else
        {
            transform.localPosition = new Vector2(transform.localPosition.x + 3f, transform.localPosition.y);
        }
    }

    private void FixedUpdate()
    {
        MoveBolt();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (penetration > 0)
        {
            penetration--;
            DealDamage(other);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
