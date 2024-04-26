using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PickUp(Collider2D other)
    {
        if (other.CompareTag("Player"))
        { 
                Destroy(gameObject);
        }
        
    }
}
