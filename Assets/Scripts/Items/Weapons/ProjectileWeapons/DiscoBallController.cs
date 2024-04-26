using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoBallController : ProjectileWeapon
{    
    [SerializeField] private GameObject discoBall;

    public override void Attack()
    {
        
        for (int i = 0; i < amountOfProjectiles; i++) 
        {
            GameObject clone = Instantiate(discoBall, transform.position, Quaternion.identity);
            clone.GetComponent<DiscoBall>().SetDamage(damage);
            clone.GetComponent<DiscoBall>().SetPenetration(penetration);
        }
    }
}