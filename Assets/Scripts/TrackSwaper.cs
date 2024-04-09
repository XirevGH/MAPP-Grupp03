using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSwaper : MonoBehaviour
{

    public GameObject soundManager;
   
    private void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("SoundManager");
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {


            soundManager.GetComponent<SoundManager>().ChangeTrack((int)Random.Range(0, 2));
            
        }
    }
}
