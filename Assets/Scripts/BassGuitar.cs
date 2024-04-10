using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BassGuitar : Weapon
{
    HashSet<Collider2D> colliders = new HashSet<Collider2D>();
    private void OnTriggerStay2D(Collider2D other)
    {
        
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("BassGuitarSwing") && !colliders.Contains(other)) 
        {
            colliders.Add(other);
            DealDamage(other);
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("BassGuitarIdle"))
        {
            colliders.Clear();
        }
    }
}
