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
        if (Input.GetKeyDown(KeyCode.E) && !usedAbility) //kräver att man klickar på knappen och att man inte har ability på cooldown
        {
            Attack();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (dealDamage && other.CompareTag("Enemy")) //checkar om det finns enemies inom collidern som kan göras skada på
        {
            // TODO lägg till så att man kan göra skada på enemies i collidern
            other.GetComponent<Enemy>().TakeDamage(damage);
        }
  
    }


    void Attack() //Gör så att man använder abilityn, behöver lägga till animationsdelen här senare
    {
        dealDamage = true;
        usedAbility = true;
        //TODO lägg till animation
        Invoke("BreakDanceTime", abilityTime);
        Invoke("CanUseAbility", abilityCooldown); //efter en viss stund gör den så att usedAbility blir false så man kan använda ability igen
       
    }

    private void CanUseAbility()
    {
        Debug.Log("You can use ability");
        usedAbility = false;
    }

    private void BreakDanceTime()
    {
        dealDamage = false;
    }

}
