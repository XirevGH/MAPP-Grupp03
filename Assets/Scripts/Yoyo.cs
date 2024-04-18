using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Yoyo : Weapon
{

    private float angle;
  
    private void FixedUpdate()
    {
        if (angle == 360)
        { 
         angle = 0;
        }
        this.transform.eulerAngles = new Vector3(0, 0, angle);
        angle++;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Enemy"))
        {
            DealDamage(other);
        }
       
    }
}
