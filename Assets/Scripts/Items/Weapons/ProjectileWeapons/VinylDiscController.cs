using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VinylDiscController : ProjectileWeapon
{
    [SerializeField] private GameObject vinylDisc;
    [SerializeField] private float attackDelayTime;

    private Vector3 playerPosition;

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
            playerPosition = player.transform.position;
            GameObject clone = Instantiate(vinylDisc, playerPosition, Quaternion.identity);
            clone.GetComponent<Projectile>().SetDamage(damage);
            clone.GetComponent<Projectile>().SetPenetration(penetration);
            clone.GetComponent<VinylDisc>().isAtPlayer = true;

            yield return new WaitForSeconds(attackDelayTime);

            playerPosition = player.transform.position;
            GameObject clone1 = Instantiate(vinylDisc, playerPosition, Quaternion.identity);
            clone1.GetComponent<Projectile>().SetDamage(damage);
            clone1.GetComponent<Projectile>().SetPenetration(penetration);
            clone1.GetComponent<VinylDisc>().isAtPlayer = true;
        }

    }
}
