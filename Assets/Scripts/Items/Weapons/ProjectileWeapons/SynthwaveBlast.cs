using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class SynthwaveBlast : ProjectileWeapon
{
    private Collider2D enemy;
    Dictionary<GameObject, int> particleCollisions = new Dictionary<GameObject, int>();

    public void CollisionDetected(GameObject particle, Collider2D enemy)
    {
        this.enemy = enemy;
        if (!particleCollisions.ContainsKey(particle))
        {
            GiveParticlesLifetime(particle);
        }
        SubtractFromLifetime(particle);
        Attack();
    }

    public void SpawnLightning()
    {

    }

    private void GiveParticlesLifetime(GameObject particle)
    {
        particleCollisions.Add(particle, penetration);
    }

    private void SubtractFromLifetime(GameObject particle) 
    {
        particleCollisions[particle]--;
    }


    

    public override void Attack()
    {
        DealDamage(enemy);
    }


   
}
