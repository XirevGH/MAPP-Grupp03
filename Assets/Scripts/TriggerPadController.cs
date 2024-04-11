using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TriggerPadController : MonoBehaviour
{

    [SerializeField] KeyCode keyCode;
    [SerializeField] GameObject currentWeapon;

    private Image image;
    private GameObject beat;
    private bool isPressable;
    private int totalHit;

    void Start()
    {
        image = GetComponent<Image>();
        totalHit = 0;

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(keyCode))
        {
            image.color = Color.blue;
            if (isPressable) 
            {
                Destroy(beat);
                Debug.Log("Boogie Boogie");
                ++totalHit;
                currentWeapon.GetComponent<Weapon>().ChangeCooldownDuration(-0.0f);

                Debug.Log(currentWeapon.GetComponent<Weapon>().GetCooldownDuration());

            }
        }

        if (Input.GetKeyUp(keyCode))
        {
            image.color = Color.white;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        beat = collider.GameObject();
        if (collider.CompareTag("Beat"))
        {
           
            isPressable = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Beat"))
        {
            isPressable = false;
        }
    }

    
}
