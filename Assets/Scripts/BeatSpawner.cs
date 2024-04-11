using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatSpawner : MonoBehaviour
{
    [SerializeField] private float BPM;
    [SerializeField] GameObject note;
    private float spawnRate;


    // Start is called before the first frame update
    void Start()
    {
        spawnRate = 60f/BPM;
        InvokeRepeating("Spawn", 0, spawnRate);
    }

    // Update is called once per frame
    void Update()
    {
       
       
    }

    private void Spawn()
    {
        Instantiate(note, this.transform.position, this.transform.rotation, this.transform);
       
       

    }
}
