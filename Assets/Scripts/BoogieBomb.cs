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
    public float abilityCooldown;
    public float bombRangeY; //TODO fixa så att man kan sikta den
    public float bombRangeX;

    [SerializeField] private Transform player;
    GameObject enemy;


    void Update()
    {

        transform.position = player.position;

        if (Input.GetKeyDown(KeyCode.Q) && !usedAbility) //kräver att man klickar på knappen och att man inte har ability på cooldown
        {
            UnityEngine.Vector2 currentPosition = transform.position;

            this.GetComponent<CircleCollider2D>().offset = new UnityEngine.Vector2(bombRangeX, bombRangeY);

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
        Invoke("TouchedGround", 0.5f); //gör så att abilityn gör damage och visar att abilityn har använts
        Invoke("CanUseAbility", abilityCooldown); //efter en viss stund gör den så att usedAbility blir false så man kan använda ability igen

    }
    private void TouchedGround()
    {

        dealDamage = true;
        Invoke("BombTime", 0.5f); //gör så att bomben gör skada under en viss tid
        usedAbility = true;
    }

    private void BombTime()
    {
        dealDamage = false;
    }

    private void CanUseAbility()
    {
        Debug.Log("You can use ability");
        this.GetComponent<CircleCollider2D>().offset = new UnityEngine.Vector2(0, 0);

        usedAbility = false;
    }

}
