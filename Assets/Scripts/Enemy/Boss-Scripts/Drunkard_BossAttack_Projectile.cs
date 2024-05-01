using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drunkard_BossAttack_Projectile : Superclass_BossAttack_Projectile
{
    public float abilitySize;
    public float abilitySlow;
    public float abilityLifetime;

    public override void ShootAtPlayer(GameObject target)
    {
        if (bossProjectileObject)
        {
            GameObject clone = Instantiate(bossProjectileObject, bossPosition.position, Quaternion.identity, EnemySpawner.normalEnemyParent.GetComponent<Transform>());
            Projectile_From_Boss bossAttackProjectile = clone.GetComponent<Projectile_From_Boss>();
            if (bossAttackProjectile != null)
            {   
                Vector3 direction = (target.transform.position - bossPosition.position).normalized;
                bossAttackProjectile.InitializeDrunkardProjectile(projectileSpeed, projectileLifetime, abilitySize, abilitySlow, abilityLifetime, direction, target); 
            }
        }
    }
}