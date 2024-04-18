using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class TriggerController : MonoBehaviour
{
    [SerializeField] public float currentTrackBPM;
    [SerializeField] GameObject trackswaper;
    [SerializeField] public Trigger[] triggers;
    [SerializeField] public bool isTriggering;
    public GameObject soundManager;


    private AudioSource inGameMusic;
        


    [System.Serializable]
    public class Trigger
    {
        [SerializeField] public float noteValue;
        [SerializeField] private UnityEvent quaterNoteTrigger;
        private int lastQuaterNote;
        public bool isTriggering;
        public float GetIntervalLength(float BPM)
        {

            return 60f / (BPM/ noteValue);

        }
        

        public void CheckForNewQuaterNote(float interval)
        {
            if (Mathf.FloorToInt(interval) != lastQuaterNote)
            {
                lastQuaterNote = Mathf.FloorToInt(interval);

                if(isTriggering)
                {
                    quaterNoteTrigger.Invoke();
                } 
               
            }

        }


    }


    void Start()
    {
        isTriggering = true;
        soundManager = GameObject.FindGameObjectWithTag("SoundManager");
        inGameMusic = soundManager.transform.GetChild(0).GetComponent<AudioSource>();
        triggers[0].isTriggering = isTriggering;
    }


    void Update()
    {
        inGameMusic = soundManager.transform.GetChild(0).GetComponent<AudioSource>();

        currentTrackBPM = soundManager.GetComponent<SoundManager>().BPMforTracks[trackswaper.GetComponent<TrackSwaper>().i];
        
        if (isTriggering)
        {
            float sampledTime = (inGameMusic.timeSamples / (inGameMusic.clip.frequency * triggers[0].GetIntervalLength(currentTrackBPM)));
            triggers[0].CheckForNewQuaterNote(sampledTime);
                

            

        }


    }

    public void ToggleTrigger()
    {
        triggers[0].isTriggering = !triggers[0].isTriggering;



        isTriggering = !isTriggering;
    }

    public Trigger GetTrigger(int triggerNumber) 
    {
        return triggers[triggerNumber];
    }


}
