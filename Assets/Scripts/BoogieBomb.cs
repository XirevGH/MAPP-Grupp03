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
    public float bombRangeY; //TODO fixa s� att man kan sikta den
    public float bombRangeX;

    [SerializeField] private Transform player;
    GameObject enemy;


    void Update()
    {

        transform.position = player.position;

        if (Input.GetKeyDown(KeyCode.Q) && !usedAbility) //kr�ver att man klickar p� knappen och att man inte har ability p� cooldown
        {
            UnityEngine.Vector2 currentPosition = transform.position;

            this.GetComponent<CircleCollider2D>().offset = new UnityEngine.Vector2(bombRangeX, bombRangeY);

            Attack();
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
        Invoke("TouchedGround", 0.5f); //g�r s� att abilityn g�r damage och visar att abilityn har anv�nts
        Invoke("CanUseAbility", abilityCooldown); //efter en viss stund g�r den s� att usedAbility blir false s� man kan anv�nda ability igen

    }
    private void TouchedGround()
    {

        dealDamage = true;
        Invoke("BombTime", 0.5f); //g�r s� att bomben g�r skada under en viss tid
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
