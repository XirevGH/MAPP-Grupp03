using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPadController : MonoBehaviour
{

    [SerializeField] KeyCode keyCode;

    private SpriteRenderer spriteRenderer;
    GameObject ballz;
    private bool isPressable;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        ballz = GameObject.FindGameObjectWithTag("Ballz");

        if (Input.GetKeyDown(keyCode))
        {
            spriteRenderer.color = Color.blue;
            if (isPressable) 
            {
                Destroy(ballz);
            }
        }

        if (Input.GetKeyUp(keyCode))
        {
            spriteRenderer.color = Color.white;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Ballz"))
        {
            Debug.Log("Boogie Boogie");
            isPressable = true;

        }


    }

    void OnTriggerEnxit2D(Collider2D collider)
    {
        if (collider.CompareTag("Ballz"))
        {
            Debug.Log("Boogie Boogie");
            isPressable = false;

        }


    }
}
