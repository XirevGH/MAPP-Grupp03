using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Superclass_BossAttack_Projectile : MonoBehaviour
{
    // Is a superclass which is used to easily call on "ShootAtPlayer"
    public float projectileLifetime;
    public float projectileSpeed;

    public Transform bossPosition;
    [SerializeField] protected GameObject bossProjectileObject;

    public virtual void ShootAtPlayer(GameObject target)
    {

    }
}