using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static BeatSpawnerController;

public class BeatSpawner : MonoBehaviour
{
    [SerializeField] private GameObject beat;
    [SerializeField] private GameObject parentForBeat;



    public void SpawnBeat()
    {
       Instantiate(beat, this.transform.position, Quaternion.identity, parentForBeat.GetComponent<Transform>());



    }

    

   

    public void SetSpawnPosition()
    {
        this.transform.localPosition = new Vector2(Random.Range(-16.0f, 16.0f), Random.Range(-8.0f, 8.0f));


    }


}
