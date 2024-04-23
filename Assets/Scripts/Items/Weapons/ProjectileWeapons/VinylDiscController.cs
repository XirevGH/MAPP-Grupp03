using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VinylDiscController : Weapon
{
    [SerializeField] private GameObject vinylDisc;

    // Start is called before the first frame updat

    public override void Attack()
    {
        Instantiate(vinylDisc, transform.position, Quaternion.identity);

    }
}
