using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BassGuitar : MonoBehaviour
{
    public float damage;

    public float DealDamage() 
    {
        return damage; 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(DealDamage());
        }
    }
}
