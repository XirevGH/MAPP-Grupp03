using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public void DealDamage(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {

            other.GetComponent<Enemy>().TakeDamage(1);
        }

    }
}
