using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    protected float damage;
    protected float speed;
    protected int penetration;

    public void DealDamage(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

    public void DestroyWhenMaxPenetration()
    {
        penetration--;
        if (penetration <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetDamage(float amount)
    {
        damage = amount;

    }

    public void SetPenetration(int amount)
    {
        penetration = amount;
    }

}