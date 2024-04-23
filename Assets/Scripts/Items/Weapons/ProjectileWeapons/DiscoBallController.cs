using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoBallController : ProjectileWeapon
{    
    [SerializeField] private GameObject discoBall;

    public override void Attack()
    {
        GameObject clone = Instantiate(discoBall, transform.position, Quaternion.identity);
        clone.GetComponent<DiscoBall>().SetDamage(damage);
    }
}