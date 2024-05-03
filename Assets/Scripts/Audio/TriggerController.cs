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

    public static TriggerController Instance
    {
        get; private set;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
       
    }

    void Start()
    {
        isTriggering = true;

        inGameMusic = SoundManager.Instance.transform.GetChild(0).GetComponent<AudioSource>();

    }

    void Update()
    {
        inGameMusic = SoundManager.Instance.transform.GetChild(0).GetComponent<AudioSource>();
        inGameCurrentTrackBPM = SoundManager.Instance.GetCurrentBPM();
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