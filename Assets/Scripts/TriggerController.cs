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
    [SerializeField] private Tringger[] trigger;
    [SerializeField] public bool isTriggering;
    public GameObject soundManager;


    private AudioSource inGameMusic;
        


    [System.Serializable]
    public class Tringger
    {
        [SerializeField] private float noteValue;
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
        trigger[0].isTriggering = isTriggering;
    }


    void Update()
    {
        inGameMusic = soundManager.transform.GetChild(0).GetComponent<AudioSource>();

        currentTrackBPM = soundManager.GetComponent<SoundManager>().BPMforTracks[trackswaper.GetComponent<TrackSwaper>().i];
        
        if (isTriggering)
        {
            float sampledTime = (inGameMusic.timeSamples / (inGameMusic.clip.frequency * trigger[0].GetIntervalLength(currentTrackBPM)));
            trigger[0].CheckForNewQuaterNote(sampledTime);
                

            

        }


    }

    public void ToggleTrigger()
    {
        trigger[0].isTriggering = !trigger[0].isTriggering;



        isTriggering = !isTriggering;
    }


}
