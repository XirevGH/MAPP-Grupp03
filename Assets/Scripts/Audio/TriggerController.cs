using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class TriggerController : MonoBehaviour
{
    [SerializeField] public float inGameCurrentTrackBPM;
    [SerializeField] GameObject trackswapper;
    [SerializeField] public Trigger[] triggers;
    [SerializeField] public bool isTriggering;
    private SoundManager soundManager;

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
        GameObject manager = GameObject.FindGameObjectWithTag("SoundManager");
        inGameMusic = manager.transform.GetChild(0).GetComponent<AudioSource>();
        soundManager = manager.GetComponent<SoundManager>();
        triggers[0].isTriggering = isTriggering;
    }

    void Update()
    {
        //inGameMusic = soundManager.transform.GetChild(0).GetComponent<AudioSource>();
        inGameCurrentTrackBPM = soundManager.BPMforTracks[trackswapper.GetComponent<TrackSwapper>().i];
        if (isTriggering)
        {
            foreach (Trigger trigger in triggers)
            {
                float sampledTime = (inGameMusic.timeSamples / (inGameMusic.clip.frequency * trigger.GetIntervalLength(inGameCurrentTrackBPM)));
                trigger.CheckForNewQuaterNote(sampledTime);
                //Debug.Log(Mathf.FloorToInt(sampledTime));
                //Debug.Log(Mathf.FloorToInt(inGameMusic.));
            }
        }
    }

    public void ToggleTrigger()
    {
        isTriggering = !isTriggering;
    }

    public Trigger GetTrigger(int triggerNumber) 
    {
        return triggers[triggerNumber];
    }

    public float GetCurrentTrackBPM()
    {
        return inGameCurrentTrackBPM;
    }
}