using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AsawinDebugging : MonoBehaviour
{
    public KeyCode keyCode;
    [SerializeField] public GameObject testingObj1, testingObj2;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        testingObj1 = GameObject.FindGameObjectWithTag("SoundManager");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("isRunning");
        

        if (Input.GetKeyDown(keyCode))
        {
            spriteRenderer.color = Color.blue;

             
                Debug.Log("Boogie Boogie");

            testingObj1.GetComponent<SoundManager>().Pause();
            testingObj2.GetComponent<BeatSpawnerController>().ToggleNoteSpawn();





        }

        if (Input.GetKeyUp(keyCode))
        {
            spriteRenderer.color = Color.white;
        }
    }

}
