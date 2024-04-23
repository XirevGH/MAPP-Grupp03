using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VinylDiscController : Weapon
{
    [SerializeField] private GameObject vinylDisc;

    private Transform playerTransform;
    private Vector3 playerPosition;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void Attack()
    {
        playerPosition = playerTransform.position;
        GameObject clone = Instantiate(vinylDisc, playerPosition, Quaternion.identity);
        clone.GetComponent<Projectile>().SetDamage(damage);
        clone.GetComponent<VinylDisc>().isAtPlayer = true;
    }
}