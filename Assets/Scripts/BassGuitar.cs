using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BassGuitar : Weapon
{
    HashSet<Collider2D> colliders = new HashSet<Collider2D>();
    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public override void Attack()
    {
        Debug.Log("Attck in Bass");
        anim.SetTrigger("Attacking");
        base.Attack();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("BassGuitarSwing") && !colliders.Contains(other) && other.gameObject.CompareTag("Enemy")) 
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
