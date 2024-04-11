using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BeatSpawner : MonoBehaviour
{
    [SerializeField] GameObject note;
    // Start is called before the first frame update
    public void Spawn()
    {
        Instantiate(note, this.transform.position, this.transform.rotation, this.transform);



    }
}
