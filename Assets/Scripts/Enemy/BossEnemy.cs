using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

[System.Serializable]
public class BossCharge
{   //Logicen för Charge
    public bool chargeEnabled;  //Om de är falsk Chargar bossen aldrig
    public float chargeDuration;
    public float chargeAdditiveSpeed;
    [HideInInspector] public bool bossChargeActive;
    private float chargeTimeCountdown;

    public void SetupCharge() {
        if (chargeEnabled) {
            chargeTimeCountdown = chargeDuration;
            bossChargeActive = false;
        }
    }

    public void UpdateCharge() {
        if (chargeEnabled) {
            if (bossChargeActive) {
                chargeTimeCountdown -= Time.deltaTime;
            }

            if (chargeTimeCountdown <= 0) {
                bossChargeActive = false;
                chargeTimeCountdown = chargeDuration;
            }
        }
    }

    public void ActivateCharge() {
        if (chargeEnabled) {
            bossChargeActive = true;
        }
    }
}

public class BossEnemy : Enemy
{
    [SerializeField] private Slider hpSlider;
    [SerializeField] private BossCharge bossCharge;
    public float baseAttackCooldown;
    private float attackCooldown;

    protected override void Start() {
        base.Start(); // Calls the Start method of the Enemy class.

        // Now add BossEnemy-specific initialization.
        if (bossCharge.chargeEnabled) {
            bossCharge.SetupCharge();
        }
    }

    public BossEnemy() {   
        attackCooldown = 0f;
        EnemySpawner.bossAlive = true;
    }

    protected override void CustomSlowUpdate() // Slow update 0.4s
    {   base.CustomSlowUpdate();
        UpdateBossHp();
    }

    void FixedUpdate() {
        SelectTarget();
        attackCooldown -= Time.deltaTime;

        if (IsAlive()) {
            bossCharge.UpdateCharge();

            if (Vector3.Distance(player.transform.position, transform.position) < 1) {
                player.GetComponent<Player>().TakeDamage(1);
            }
            //Shoot att player
            if (Vector3.Distance(player.transform.position, transform.position) < 15 && attackCooldown <= 0) {
                enemyAnim.SetTrigger("Attack");
                GetComponent<Superclass_BossAttack_Projectile>().ShootAtPlayer(target);
                attackCooldown = baseAttackCooldown;
                //activates Charge
                bossCharge.ActivateCharge();
            }

            if (transform.position.x < target.transform.position.x) {
                sprite.flipX = false;
            } else {
                sprite.flipX = true;
            }
            //Movement logic
            if (bossCharge.bossChargeActive) {
                enemyAnim.SetTrigger("Charge");
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, (thisMovementSpeed + bossCharge.chargeAdditiveSpeed) / 200);
                
            } else {
                enemyAnim.SetTrigger("Walking");
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, thisMovementSpeed / 200);
            }
        }
    }

    private void UpdateBossHp() {
        hpSlider.value = health / startingHealth;
    }

    protected override void DestroyGameObject() {
        EnemySpawner.bossAlive = false; 
        base.DestroyGameObject();
        
    }
}