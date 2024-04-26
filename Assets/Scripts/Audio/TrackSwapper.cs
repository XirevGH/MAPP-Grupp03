using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSwapper : MonoBehaviour
{

    public int i = 0;


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Music Collider"))
        {
            i++;
            if (i >= 4)
            {
                i = 0;
            }
            SoundManager.instance.ChangeTrack(i);
        }
    }
}
