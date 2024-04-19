using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BoogieBomb : Weapon
{
    HashSet<Collider2D> colliders = new HashSet<Collider2D>();

    public bool usedAbility = false;
    public bool dealDamage = false;
    public bool bombMoving = false;
    public float abilityCooldown;
    public float bombRangeY;
    public float bombRangeX;


    [SerializeField] private GameObject bombParticles;
    public bool hasExploded = false;

    [SerializeField] private Transform player;
    GameObject enemy;


    private void Start()
    {
     bombRangeY = UnityEngine.Random.Range(-6, 6);
     bombRangeX = UnityEngine.Random.Range(-6, 6);
    }

    void FixedUpdate()
    {
        if (!bombMoving)
        {
            transform.position = player.position;
        }


        if (!usedAbility)
        {
            bombMoving = true;
            transform.position = transform.position + new UnityEngine.Vector3(bombRangeX, bombRangeY, 0);
            Attack();

            
        }

        if (hasExploded)
        {
            Instantiate(bombParticles, transform.position, bombParticles.transform.localRotation);
            hasExploded = false; 
        }
    }


    private void OnTriggerStay2D(Collider2D other)
    {

        if (WeaponIsReady() && !colliders.Contains(other) && other.gameObject.CompareTag("Enemy")) //checkar om det finns enemies inom collidern som kan göras skada på
        {
            colliders.Add(other);
            DealDamage(other);
        }
        if (!dealDamage)
        {
            colliders.Clear();
        }
    }


    override public void Attack() //Gör så att man använder abilityn
    {
        StartCooldown();
        //TODO lägg till animation
        usedAbility = true;
        Invoke("TouchedGround", 0.5f); //gör så att abilityn gör damage och visar att abilityn har använts
        Invoke("AbilityCooldown", abilityCooldown);

    }
    private void TouchedGround()
    {
        hasExploded = true;
        Invoke("Explosion", 0.015f);
        this.GetComponent<SpriteRenderer>().enabled = true;
        dealDamage = true;
        Invoke("ReturnBomb", 1f);
    }

    private void ReturnBomb()
    {
        bombRangeY = UnityEngine.Random.Range(-6, 6);
        bombRangeX = UnityEngine.Random.Range(-6, 6);
        bombMoving = false;
    }


    private void Explosion()
    {
        hasExploded = false;
    }

    private void AbilityCooldown()
    {

        usedAbility = false;
        this.GetComponent<SpriteRenderer>().enabled = false;
    }

}
