using System.Collections;
using System.Collections.Generic;
using System.Numerics;
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
    //public float bombSpeed;

    [SerializeField] private Transform player;
    GameObject enemy;


    private void Start()
    {
     bombRangeY = UnityEngine.Random.Range(-6, 6);
     bombRangeX = UnityEngine.Random.Range(-6, 6);
    } 

    void FixedUpdate()
    {
        if(!bombMoving)
        {
            transform.position = player.position;
        }
      

        if (!usedAbility) //kräver att man klickar på knappen och att man inte har ability på cooldown
        {

          
        //    transform.position = UnityEngine.Vector3.MoveTowards(transform.position, new UnityEngine.Vector3(bombRangeX, bombRangeY, 0), 20 * Time.deltaTime);

               transform.position = transform.position + new UnityEngine.Vector3(bombRangeX, bombRangeY, 0);


            Attack();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {

        if (dealDamage && other.CompareTag("Enemy")) //checkar om det finns enemies inom collidern som kan göras skada på
        {

            other.GetComponent<Enemy>().TakeDamage(damage);
           
        }
    }


    void Attack() //Gör så att man använder abilityn
    {

        //TODO lägg till animation
       
        bombMoving = true;
        Invoke("TouchedGround", 0.5f); //gör så att abilityn gör damage och visar att abilityn har använts
        Invoke("CanUseAbility", abilityCooldown); //efter en viss stund gör den så att usedAbility blir false så man kan använda ability igen

    }
    private void TouchedGround()
    {
        this.GetComponent<SpriteRenderer>().enabled = true;
        dealDamage = true;
        Invoke("BombTime", 0.06f); //gör så att bomben gör skada under en viss tid
        usedAbility = true;
    }

    private void BombTime()
    {
        dealDamage = false;
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
