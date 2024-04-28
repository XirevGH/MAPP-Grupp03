using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VinylDiscController : ProjectileWeapon
{
    [SerializeField] private GameObject vinylDisc;
    [SerializeField] private float attackDelayTime;

    private Transform playerTransform;
    private Vector3 playerPosition;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void Attack()
    {
        if (gameObject.activeSelf) { 
            StartCoroutine("AttackDelay");
        }
    }

    private IEnumerator AttackDelay()
    {
        for (int i = 0; i < amountOfProjectiles; i++)
        {
            playerPosition = playerTransform.position;
            GameObject clone = Instantiate(vinylDisc, playerPosition, Quaternion.identity);
            clone.GetComponent<Projectile>().SetDamage(damage);
            clone.GetComponent<Projectile>().SetPenetration(penetration);
            clone.GetComponent<VinylDisc>().isAtPlayer = true;

            yield return new WaitForSeconds(attackDelayTime);

            playerPosition = playerTransform.position;
            GameObject clone1 = Instantiate(vinylDisc, playerPosition, Quaternion.identity);
            clone1.GetComponent<Projectile>().SetDamage(damage);
            clone1.GetComponent<Projectile>().SetPenetration(penetration);
            clone1.GetComponent<VinylDisc>().isAtPlayer = true;
        }

    }
}
