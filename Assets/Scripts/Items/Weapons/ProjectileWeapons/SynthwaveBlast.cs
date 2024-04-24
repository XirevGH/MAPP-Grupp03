
using System.Collections.Generic;
using UnityEngine;


public class SynthwaveBlast : ProjectileWeapon
{
    [SerializeField] GameObject synthwavePivotPrefab;
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
        for (int i = 0; i < amountOfProjectiles; i++) {
            float randomValue = Random.Range(0f, 360f);
            GameObject clone = Instantiate(synthwavePivotPrefab, transform);
            clone.transform.eulerAngles = new Vector3(0, 0, randomValue);
            clone.transform.GetChild(0).GetComponent<SynthwaveBolt>().SetDamage(damage);
            clone.transform.GetChild(0).GetComponent<SynthwaveBolt>().SetPenetration(penetration);
        }
    }
}
