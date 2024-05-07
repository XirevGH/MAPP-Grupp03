using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BassGuitar : PhysicalWeapon
{
    public  HashSet<Collider2D> colliders = new HashSet<Collider2D>();
    public Animator anim;
   

    private void Start()
    {
        anim = GetComponent<Animator>();
       
        UnityAction action = new UnityAction(Attack);         
        TriggerController.Instance.SetTrigger(3, action);

        
    }

    public override void Attack()
    {
        anim.SetTrigger("Attacking");
        SoundManager.Instance.PlaySFX(attackSound, transform, 1, 128);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("BassGuitarSwing") && !colliders.Contains(other) && other.gameObject.CompareTag("Enemy")) 
        {
            colliders.Add(other);
            //SoundManager.Instance.PlaySFX(hitSound, transform, 1);
            DealDamage(other);

          
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("BassGuitarIdle"))
        {
            colliders.Clear();
        }
    }
}
