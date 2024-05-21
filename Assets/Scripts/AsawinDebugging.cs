using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AsawinDebugging : MonoBehaviour
{
    public KeyCode keyCode;
    public Transform a;
    [SerializeField] public GameObject testingObj1;

    private SpriteRenderer spriteRenderer;
    void Start()
    {
 
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    public void LevelUp()
    {
        Player.Instance.LevelUp();
    }

    void Update()
    {
      

        if (Input.GetKeyDown(keyCode))
        {
            //spriteRenderer.color = Color.blue;
            //float random = UnityEngine.Random.Range(0f, 101f);
            //Debug.Log(random);

            SoundManager.Instance.GetComponent<SoundManager>().StartGame();
           


        }

        if (Input.GetKeyUp(keyCode))
        {
            //spriteRenderer.color = Color.white;
        }
    }
}
