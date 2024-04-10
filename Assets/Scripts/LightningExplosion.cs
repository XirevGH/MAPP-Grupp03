using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningExplosion : MonoBehaviour
{
    ParticleSystem ps;

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(1);
        }
    }
}
