using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;

public class DiscoBallController : ProjectileWeapon
{    
    [SerializeField] private GameObject discoBall;
    [SerializeField] private float blinkTime;
    public HashSet<GameObject> activeDiscoBalls = new HashSet<GameObject>();

    public static DiscoBallController instance
    {
        get; private set;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void Blink()
    {

        if (activeDiscoBalls.Count == 0)
        {
            return;
        }
        else
        {
            
            foreach (GameObject discoBall in activeDiscoBalls)
            {
              
                if (discoBall == null)
                {
                    discoBall.GetComponent<DiscoBall>().RemoveFromSetIfNotAlive();
                }
                else
                {
                    discoBall.GetComponent<DiscoBall>().Blink();
                }
            }
        }
        
    }

    public override void Attack()
    {
        if (gameObject.activeSelf) { 
            for (int i = 0; i < amountOfProjectiles; i++) 
            {
                GameObject clone = Instantiate(discoBall, transform.position, Quaternion.identity);
                activeDiscoBalls.Add(clone);
                clone.GetComponent<DiscoBall>().SetDamage(damage);
                clone.GetComponent<DiscoBall>().SetPenetration(penetration);
            }
        }
    }
}