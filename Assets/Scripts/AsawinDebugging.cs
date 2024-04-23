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
    void Start()
    {
 
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
      

        if (Input.GetKeyDown(keyCode))
        {
            spriteRenderer.color = Color.blue;

            Debug.Log("Boogie Boogie");


            testingObj1.GetComponent<SlowAroundPlayer>().UpgradeRradius(10);


        }

        if (Input.GetKeyUp(keyCode))
        {
            spriteRenderer.color = Color.white;
        }
    }
}
