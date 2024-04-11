using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

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
        [SerializeField] private UnityEvent quaterNoteTrigger;
        private int lastQuaterNote;
      
        public float GetIntervalLength(float BPM)
        {

            return 60f / (BPM/ noteValue);

        }

        public void CheckForNewQuaterNote(float interval)
        {
            if (Mathf.FloorToInt(interval) != lastQuaterNote)
            {
                lastQuaterNote = Mathf.FloorToInt(interval);
                quaterNoteTrigger.Invoke();
               
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
        
        if (isSpawning)
        {
            foreach (Spawners spawners in spawners)
            {
                float sampledTime = (audioSource.timeSamples / (audioSource.clip.frequency * spawners.GetIntervalLength(BPM)));
                spawners.CheckForNewQuaterNote(sampledTime);
                

            }

        }

    }

    public void ToggleNoteSpawn()
    {
        isSpawning = !isSpawning;
    }


}
