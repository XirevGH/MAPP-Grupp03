using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakDanceAbility : MonoBehaviour
{
    public float damage;
    public bool usedAbility = false;
    public bool dealDamage = false;
    public float abilityCooldown;
    public float abilityTime;

    GameObject enemy;   


    void Update()
    {
        if(dealDamage)
        {
            Debug.Log("You are dealing damage ->"); //checkar om dealDamage blir true 
        }

        if (Input.GetKeyDown(KeyCode.E) && !usedAbility) //kr�ver att man klickar p� knappen och att man inte har ability p� cooldown
        {
            Attack();
            Debug.Log("Yoinkers");
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (dealDamage && other.CompareTag("Enemy")) //checkar om det finns enemies inom collidern som kan g�ras skada p�
        {
            // TODO l�gg till s� att man kan g�ra skada p� enemies i collidern

            Debug.Log("Hello");
        }
  
    }


    void Attack() //G�r s� att man anv�nder abilityn, beh�ver l�gga till animationsdelen h�r senare
    {
        dealDamage = true;
        usedAbility = true;
        //TODO l�gg till animation
        Invoke("BreakDanceTime", abilityTime);
        Invoke("CanUseAbility", abilityCooldown); //efter en viss stund g�r den s� att usedAbility blir false s� man kan anv�nda ability igen
        Debug.Log("There");
       
    }

    private void CanUseAbility()
    {
        usedAbility = false;
        Debug.Log("Yahallo");
    }

    private void BreakDanceTime()
    {
        dealDamage = false;
        Debug.Log("Yoyoyoyoyoy");
    }

}
