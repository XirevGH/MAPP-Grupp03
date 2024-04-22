using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynthwaveBlast : ProjectileWeapon
{
    ParticleSystem ps;
    /*List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();*/
    List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void OnParticleTrigger()
    {
        Debug.Log("Yelloh");
        /*int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);*/

        int numCollisionEvents = ps.GetCollisionEvents(gameObject, collisionEvents);

        for (int i = 0; i < numCollisionEvents; i++)
        {
            collisionEvents[i].colliderComponent.gameObject.GetComponent<Enemy>().TakeDamage(2);
        }
        Debug.Log(amountOfTargets);
            amountOfTargets -= 1;
        if (amountOfTargets == 0)
        {
            Destroy(gameObject);
        }
    }
}
