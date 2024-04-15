using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BoogieBomb : MonoBehaviour
{
    public float damage;
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

        if (dealDamage && other.CompareTag("Enemy")) //checkar om det finns enemies inom collidern som kan g�ras skada p�
        {

            other.GetComponent<Enemy>().TakeDamage(damage);
           
        }
    }


    void Attack() //G�r s� att man anv�nder abilityn
    {

        //TODO l�gg till animation
        bombMoving = true;
        Invoke("TouchedGround", 0.5f); //g�r s� att abilityn g�r damage och visar att abilityn har anv�nts
        Invoke("CanUseAbility", abilityCooldown); //efter en viss stund g�r den s� att usedAbility blir false s� man kan anv�nda ability igen

    }
    private void TouchedGround()
    {
        hasExploded = true;
        Invoke("Explosion", 0.015f);
        this.GetComponent<SpriteRenderer>().enabled = true;
        dealDamage = true;
        Invoke("BombTime", 0.06f); //g�r s� att bomben g�r skada under en viss tid
        usedAbility = true;
    }

    private void BombTime()
    {
        dealDamage = false;
    }

    private void Explosion()
    {
        hasExploded = false;
    }

    private void CanUseAbility()
    {
        this.GetComponent<SpriteRenderer>().enabled = false;
        if (usedAbility)
        {
            bombRangeY = UnityEngine.Random.Range(-6, 6);
            bombRangeX = UnityEngine.Random.Range(-6, 6);
            usedAbility = false;
        }
        else
        {
            usedAbility = false;
        }
        Debug.Log("You can use ability");
        bombMoving = false;
    }

}
