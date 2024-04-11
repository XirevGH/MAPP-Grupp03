using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TriggerPadController : MonoBehaviour
{

    [SerializeField] KeyCode keyCode;

    private Image image;
    private GameObject beat;
    private bool isPressable;

    void Start()
    {
        image = GetComponent<Image>();

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
