using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsawinDebugging : MonoBehaviour
{
    public KeyCode keyCode;
    [SerializeField] public GameObject testingObj, gameObject;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        

        if (Input.GetKeyDown(keyCode))
        {
            spriteRenderer.color = Color.blue;

             
                Debug.Log("Boogie Boogie");

            testingObj.GetComponent<SoundManager>().Pause();

          
        }

        if (Input.GetKeyUp(keyCode))
        {
            spriteRenderer.color = Color.white;
        }
    }

}
