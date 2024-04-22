using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoBallController : Weapon
{    
    [SerializeField] private GameObject discoBall;

    // Start is called before the first frame updat

    public override void Attack()
    {
        Instantiate(discoBall, transform.position, Quaternion.identity);
        
    }
}
