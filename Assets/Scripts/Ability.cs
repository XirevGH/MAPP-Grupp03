using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public float damage;
    public bool usedAbility = false;
    public bool dealDamage = false;
    public float abilityCooldown;
    
     GameObject enemy;   


    void Update()
    {
        if(dealDamage)
        {
            Debug.Log("You are dealing damage ->"); //checkar om dealDamage blir true 
        }

        if (Input.GetKeyDown(KeyCode.E) && !usedAbility) //kräver att man klickar på knappen och att man inte har ability på cooldown
        {
            Attack();
            Debug.Log("Yoinkers");
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (dealDamage && other.CompareTag("Enemy")) //checkar om det finns enemies inom collidern som kan göras skada på
        {  
            // TODO lägg till så att man kan göra skada på enemies i collidern

            Debug.Log("Hello");
            dealDamage = false;
        }
  
    }


    void Attack() //Gör så att man använder abilityn, behöver lägga till animationsdelen här senare
    {
        dealDamage = true;
        usedAbility = true;
        //TODO lägg till animation
        Invoke("CanUseAbility", abilityCooldown); //efter en viss stund gör den så att usedAbility blir false så man kan använda ability igen
        Debug.Log("There");
       
    }

    private void CanUseAbility()
    {
        usedAbility = false;
        Debug.Log("Yahallo");
    }
}
