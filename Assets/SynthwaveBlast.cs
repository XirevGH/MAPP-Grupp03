using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class SynthwaveBlast : ProjectileWeapon
{

    
    ParticleSystem ps;
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
    ParticleSystem.Burst[] bursts;
    ParticleSystem.EmissionModule emissionModule;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        ParticleSystem.EmissionModule emissionModule = ps.emission;
        ParticleSystem.Burst[] bursts = new ParticleSystem.Burst[1];
        
    }

    void FixedUpdate()
    {
        bursts[0] = new ParticleSystem.Burst(0, (short)amountOfProjectiles);
        emissionModule.SetBursts(bursts);
    }

    public override void Attack()
    {

    }

    private void OnParticleTrigger()
    {
        Debug.Log("Yelloh");
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter, out var insideData);

        for (int i = 0; i < enter.Count; i++) {
            for (int j = 0; i < numEnter; j++) { 
                Collider2D enemy = (Collider2D) insideData.GetCollider(i, j);
                enemy.GetComponent<Enemy>().TakeDamage(1);
                penetration -= 1;
                if (penetration == 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
