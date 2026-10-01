using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BassGuitar : PhysicalWeapon
{
    [SerializeField] private Animator anim;

    private static readonly int SwingStateHash = Animator.StringToHash("BassGuitarSwing");
    private static readonly int IdleStateHash = Animator.StringToHash("BassGuitarIdle");

    private HashSet<Collider2D> hitEnemiesInCurrentSwing = new HashSet<Collider2D>();

    protected override void Start()
    {
        base.Start();
        if (anim == null) anim = GetComponent<Animator>();

        if (weaponData != null && weaponData.HasBeatTrigger)
        {
            UnityAction action = new UnityAction(Attack);
            TriggerController.Instance.SetTrigger(weaponData.BeatNumber, action);
        }
    }

    public override void Attack()
    {
        if (gameObject.activeSelf)
        {
            anim.SetTrigger("Attacking");
            if (weaponData != null && weaponData.AttackSound != null)
            {
                SoundManager.Instance.PlaySFX(weaponData.AttackSound, 1f);
            }
        }
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        var stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.shortNameHash == SwingStateHash)
        {
            if (!hitEnemiesInCurrentSwing.Contains(other) && other.CompareTag("Enemy"))
            {
                hitEnemiesInCurrentSwing.Add(other);
                DealDamage(other);
            }
        }

        else if (stateInfo.shortNameHash == IdleStateHash)
        {
            hitEnemiesInCurrentSwing.Clear();
        }
    }
}
