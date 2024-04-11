using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPDrop : MonoBehaviour
{
    [SerializeField] private int XP;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<Player>().AddXP(XP);
            Destroy(gameObject);
        }
    }
}