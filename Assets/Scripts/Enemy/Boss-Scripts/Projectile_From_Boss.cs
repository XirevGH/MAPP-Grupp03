using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Projectile_From_Boss : MonoBehaviour{
    private float projectileSpeed;
    private float projectileLifetime;
    private Vector3 projectileDirection;

    private float abilitySize;
    private float abilitySlow;
    private float abilityLifetime;

    private float lastMeasuredDistance;
    private string bossAttack;
    private GameObject target;

    public GameObject secondaryObjectPrefab;

    public void InitializeDrunkardProjectile(float projectileSpeed, float projectileLifetime, float abilitySize, float abilitySlow, float abilityLifetime,  Vector3 projectileDirection, GameObject target)
    {
        this.abilitySize = abilitySize;  
        this.abilitySlow = abilitySlow;  
        this.abilityLifetime = abilityLifetime;

        this.projectileSpeed = projectileSpeed;
        this.projectileLifetime = projectileLifetime;
        this.projectileDirection = projectileDirection;
        
        this.target = target;

        bossAttack = "Drunkard";
    }   
    public void InitializeDivaProjectile(float projectileSpeed, float projectileLifetime, Vector3 projectileDirection, GameObject target)
    {
        this.projectileSpeed = projectileSpeed;
        this.projectileLifetime = projectileLifetime;
        this.projectileDirection = projectileDirection;
        
        this.target = target;

        bossAttack = "Diva";
    } 
        

    void Start()
    {   
        lastMeasuredDistance = Vector3.Distance(target.transform.position, transform.position);
    }

    void Update()
    {   
        transform.position += projectileDirection * projectileSpeed * Time.deltaTime;
        projectileLifetime -= Time.deltaTime;

        /*if (projectileLifetime <= 0 || Vector3.Distance(target.transform.position, transform.position) >= lastMeasuredDistance*1.01)
        {
            ActivateSecondaryObject();
        }
        lastMeasuredDistance = Vector3.Distance(target.transform.position, transform.position);
        */
    }

    private void ActivateSecondaryObject()
    {
        GameObject secondaryObject = Instantiate(secondaryObjectPrefab, transform.position, Quaternion.identity);
        if (bossAttack == "Drunkard")
        {
            Boss_AOE_Slow splash = secondaryObject.GetComponent<Boss_AOE_Slow>();
            if (splash != null)
            {   
                splash.Initialize(abilitySize, abilitySlow, abilityLifetime, target);
            }
        }

        Destroy(gameObject);
    }
}