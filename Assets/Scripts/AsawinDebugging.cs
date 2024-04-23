using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AsawinDebugging : MonoBehaviour
{
    public KeyCode keyCode;
    [SerializeField] public GameObject testingObj1;

    private SpriteRenderer spriteRenderer;
    private bool active;

    void Start()
    {
        active = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Blink()
    {
        if (spriteRenderer.color != Color.blue)
        {
            spriteRenderer.color = Color.blue;
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
    }
    void FixedUpdate()
    {
      

        if (Input.GetKeyDown(keyCode))
        {
            spriteRenderer.color = Color.blue;

            Debug.Log("Boogie Boogie");

            //testingObj1.GetComponent<YoyoController>().Upgrade();
            //testingObj1.GetComponent<YoyoController>().Attack();
            testingObj1.GetComponent<VinylDiscController>().Attack();


        }

        if (Input.GetKeyUp(keyCode))
        {
            spriteRenderer.color = Color.white;
        }
    }
}
