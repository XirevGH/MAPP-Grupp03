using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class EnemyBoss : Enemy
{
    [SerializeField] private Slider hpSlider;
    public float baseAttackColdown;
    private float attackColdown;

    private bool bossChargActiv;
    public float baseChargDuration;
    private float chargDuration;

    public float chargAddetivSpeed;


    public EnemyBoss()
    {
        this.chargDuration = baseChargDuration;
        this.attackColdown = baseAttackColdown;
        this.bossChargActiv = false;
        EnemySpawner.bossAlive = true;
    }

   

    protected override void CustomSlowUpdate() //Slow uppdate 0.4s
    {
         if (!isSlow)
        {
            UppdateSpeed();
            
        }
        SelectTarget();
        UpdateBossHp();


    }
    void FixedUpdate()
    {
        attackColdown -= Time.deltaTime;
        damageNumberWindow -= Time.deltaTime;
        if(bossChargActiv){
            chargDuration -= Time.deltaTime;
        }
        
        if (IsAlive()) 
        {

            if (Vector3.Distance(player.transform.position, transform.position) < 1)
            {
                player.GetComponent<Player>().TakeDamage(1);
            }
             if (Vector3.Distance(player.transform.position, transform.position) < 15 && attackColdown <= 0)
            {   enemyAnim.SetTrigger("Attack");
                GetComponent<DrunkerdBossAtack>().ShootNoteAttPalyer();
                attackColdown = baseAttackColdown;

                bossChargActiv = true;
            }

            if(chargDuration <= 0){
                bossChargActiv = false;
                chargDuration = baseChargDuration;
            }



            if (transform.position.x < target.transform.position.x)
            {
                sprite.flipX = false;
            }
            else
            {
                sprite.flipX = true;
            }
            if(bossChargActiv){
                enemyAnim.SetTrigger("Charg");
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, (thisMovementSpeed + chargAddetivSpeed) / 200);
            }else{
                enemyAnim.SetTrigger("Walking");
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, thisMovementSpeed / 200);
            }
            

        }
      
        
    }

    private void UpdateBossHp(){
        hpSlider.value = health / startingHealth;
    }
    protected override void DestroyGameObject()
    {
        EnemySpawner.bossAlive = false;
        Drops();
        MainManager.Instance.enemiesDefeated += 1;
        Destroy(gameObject);
    }
    

    
}

