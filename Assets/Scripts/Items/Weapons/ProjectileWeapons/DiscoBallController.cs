using System.Collections.Generic;
using UnityEngine;

public class DiscoBallController : ProjectileWeapon
{    
    [SerializeField] private GameObject discoBall;
    [SerializeField] private float blinkTime;
    public List<GameObject> activeDiscoBalls = new();

    public static DiscoBallController Instance
    {
        get; private set;
    }

    protected override void Awake()
    {
        base.Awake();
        if (Instance == null)
        {
            Instance = this;
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

            for (int i =0; i < activeDiscoBalls.Count; i++)
            {
                GameObject discoBall = activeDiscoBalls[i];

                if (discoBall == null)
                {
                    activeDiscoBalls.RemoveAt(i);
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