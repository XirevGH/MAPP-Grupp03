using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PhysicalWeapon : Weapon 
{
    public void DealDamage(Collider2D other)
    {
        if (other.gameObject != null)
        {
            other.GetComponent<Enemy>().TakeDamage(damage);

        }
    }
}
