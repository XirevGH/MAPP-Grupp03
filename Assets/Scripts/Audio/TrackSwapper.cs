using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSwapper : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Music Collider"))
        {

            SoundManager.instance.StartGame();
        }
    }
}
