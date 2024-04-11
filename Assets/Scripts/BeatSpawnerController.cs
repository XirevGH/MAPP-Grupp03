using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BeatSpawnerController : MonoBehaviour
{
    [SerializeField] private float BPM;
    [SerializeField] GameObject soundManager, trackswaper;
    [SerializeField] private Spawners[] spawners;
    [SerializeField] public bool isSpawning;
 

    private AudioSource audioSource;


    [System.Serializable]
    public class Spawners 
    {
        [SerializeField] private float noteValue;
        [SerializeField] private UnityEvent trigger;
        private int lastInterval;

        public float GetIntervalLength(float BPM)
        { 
            
            return 60f / (noteValue * BPM); 
        
        }

        public void CheckForNewInterval(float interval)
        { 
            if(Mathf.FloorToInt(interval) != lastInterval)
            { 
                lastInterval = Mathf.FloorToInt(interval);
                trigger.Invoke();
            }
        
        }
    
    }

   
    void Start()
    {
        isSpawning = true;
        audioSource = soundManager.transform.GetChild(0).GetComponent<AudioSource>();

   
    }


    void Update()
    {
        audioSource = soundManager.transform.GetChild(0).GetComponent<AudioSource>();

        BPM = soundManager.GetComponent<SoundManager>().BPMforTracks[trackswaper.GetComponent<TrackSwaper>().i]; 

        if (isSpawning == true)
        {
            foreach (Spawners spawners in spawners)
            {
                float sampledTime = (audioSource.timeSamples / (audioSource.clip.frequency * spawners.GetIntervalLength(BPM)));
                spawners.CheckForNewInterval(sampledTime);

            }

        }
       
    }

    public void ToggleNoteSpawn()
    {
        isSpawning = !isSpawning;
    }

   
}
