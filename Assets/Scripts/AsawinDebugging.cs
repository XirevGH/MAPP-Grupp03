using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AsawinDebugging : MonoBehaviour
{
    public KeyCode keyCode;
    [SerializeField] public GameObject testingObj1;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
       
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

    // Update is called once per frame
    void Update()
    {
       
        

        if (Input.GetKeyDown(keyCode))
        {
            spriteRenderer.color = Color.blue;

             
                Debug.Log("Boogie Boogie");

            testingObj1.GetComponent<YoyoController>().Upgrade();
            testingObj1.GetComponent<YoyoController>().YoYoSperMode();





        }

        if (Input.GetKeyUp(keyCode))
        {
            spriteRenderer.color = Color.white;
        }
    }

}
