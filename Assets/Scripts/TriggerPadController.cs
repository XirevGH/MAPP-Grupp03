using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TriggerPadController : MonoBehaviour
{

    [SerializeField] KeyCode keyCode;

    private Image image;
    private GameObject ballz;
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
                Destroy(ballz);
            }
        }

        if (Input.GetKeyUp(keyCode))
        {
            image.color = Color.white;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        ballz = collider.GameObject();
        if (collider.CompareTag("Ballz"))
        {
            Debug.Log("Boogie Boogie");
            isPressable = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Ballz"))
        {
            Debug.Log("Boogie Boogie");
            isPressable = false;
        }
    }

    
}
