using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynthwaveBlast : ProjectileWeapon
{
    public override void Attack()
    {

    }
    ParticleSystem ps;
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void OnParticleTrigger()
    {
        Debug.Log("Yelloh");
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter, out var insideData);

        for (int i = 0; i < enter.Count; i++) {
            for (int j = 0; i < numEnter; j++) { 
                Collider2D enemy = (Collider2D) insideData.GetCollider(i, j);
            }
        }
        Debug.Log(amountOfTargets);
            amountOfTargets -= 1;
        if (amountOfTargets == 0)
        {
            Destroy(gameObject);
        }
    }
}
