using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSwaper : MonoBehaviour
{

    public GameObject soundManager;

    public int i = 0;
   
    private void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("SoundManager");
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (i >= 4)
            {
                i = 0;
            }
                soundManager.GetComponent<SoundManager>().ChangeTrack(i + 1);
                i = i + 1;
            

            
            
        }
    }
}
