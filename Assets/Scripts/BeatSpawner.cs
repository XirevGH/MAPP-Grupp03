using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static BeatSpawnerController;

public class BeatSpawner : MonoBehaviour
{
    [SerializeField] GameObject note;
    RectTransform rectTransform;
    private void Start()
    {
       rectTransform = GetComponent<RectTransform>();

    }
    public void SpawnBeat()
    {
        Instantiate(note, this.transform.position, Quaternion.identity);



    }

    public void SetSpawnPosition()
    {
        this.transform.localPosition = new Vector2(Random.Range(-16.0f, 16.0f), Random.Range(-8.0f, 8.0f));


    }


}
