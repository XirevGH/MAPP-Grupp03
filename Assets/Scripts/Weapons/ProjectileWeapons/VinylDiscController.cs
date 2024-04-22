using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VinylDiscController : Weapon
{
    [SerializeField] private GameObject vinylDisc;
    private Vector3 playerPosition;
    // Start is called before the first frame updat
    void Start()
    {
       
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
      
       
    }

    void FixedUpdate()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
      


    }
    public override void Attack()
    {
        GameObject clone = Instantiate(vinylDisc, playerPosition, Quaternion.identity);
        clone.GetComponent<Projectile>().SetDamage(damage);
        clone.GetComponent<VinylDisc>().isAtPlayer = true;
    }
}
