using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSwapper : MonoBehaviour
{
    private SoundManager soundManager;

    public int i = 0;
   
    private void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Music Collider"))
        {
            i++;
            if (i >= 4)
            {
                i = 0;
            }
            soundManager.ChangeTrack(i);
        }
    }
}
