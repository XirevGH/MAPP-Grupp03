using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PhysicalWeapon : Weapon 
{
    public virtual void DealDamage(Collider2D other)
    {
        if (other != null && other.TryGetComponent<Enemy>(out var enemy))
        {
            enemy.TakeDamage(GetCurrentDamage());
        }
    }
}
