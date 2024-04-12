using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class TriggerPadController : MonoBehaviour
{

    //[SerializeField] KeyCode keyCode;
    //[SerializeField] GameObject currentWeapon;
    [SerializeField] GameObject[] weapons;
    [SerializeField] float reduceCooldownDuration;

    //private SpriteRenderer spriteRenderer;
    private GameObject beat;
    //private bool isPressable;
    //private int totalHit;

    void Start()
    {
       // spriteRenderer = GetComponent<SpriteRenderer>();
        //totalHit = 0;

    }

    // Update is called once per frame
    void Update()
    {

       // currentWeapon = GameObject.FindGameObjectWithTag("Weapon");

        //    if (Input.GetKeyDown(keyCode))
        //    {
        //        spriteRenderer.color = Color.blue;
        //        if (isPressable) 
        //        {
        //            Destroy(beat);
        //            Debug.Log("Boogie Boogie");
        //            ++totalHit;
        //            currentWeapon.GetComponent<Weapon>().ChangeCooldownDuration(-0.0f);

        //            Debug.Log(currentWeapon.GetComponent<Weapon>().GetCooldownDuration());

        //        }
        //    }

        //    if (Input.GetKeyUp(keyCode))
        //    {
        //        spriteRenderer.color = Color.white;
        //    }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        beat = collider.GameObject();
        if (collider.CompareTag("Beat"))
        {
            foreach (GameObject weapon in weapons)
            {
                if (weapon.activeSelf)
                {
                    weapon.GetComponent<Weapon>().ChangeCooldownDuration(-reduceCooldownDuration);
                    Debug.Log(weapon.GetComponent<Weapon>().GetCooldownDuration());
                }
               
            }


           
            Destroy(beat);
            //isPressable = true;
        }
    }

    //void OnTriggerExit2D(Collider2D collider)
    //{
    //    if (collider.CompareTag("Beat"))
    //    {
    //        isPressable = false;
    //    }
    //}

    
}
