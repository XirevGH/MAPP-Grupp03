using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private float percentToHeal;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Playuer"))
        {
            other.GetComponent<Player>().RestoreHealth(percentToHeal);
        }
    }
}