using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;


public class TriggerController : MonoBehaviour
{
    public static float inGameCurrentTrackBPM;
    public static Trigger[] triggers;
    public static bool isTriggering;

    private AudioSource inGameMusic;
        
    [System.Serializable]

    public class Trigger
    {
        [SerializeField] public float noteValue;
        [SerializeField] public UnityEvent quaterNoteTrigger;
        private int lastQuaterNote;
        public bool isTriggering;

        public float GetIntervalLength(float BPM)
        {
            return 60f / (BPM/ noteValue);
        }

        public void CheckForNewQuaterNote(float interval)
        {
            float intervalTwoDecimal = (float)Math.Floor(interval * 100) / 100;

            if (Mathf.FloorToInt(intervalTwoDecimal) != lastQuaterNote)
            {
                lastQuaterNote = Mathf.FloorToInt(intervalTwoDecimal);

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

    public static void ToggleTrigger()
    {
        isTriggering = !isTriggering;
    }

    public static Trigger GetTrigger(int triggerNumber) 
    {
        return triggers[triggerNumber];
    }

    public float GetCurrentTrackBPM()
    {
        return inGameCurrentTrackBPM;
    }

    public static void SetTrigger(int triggerNumber, UnityAction action)
    {
        triggers[triggerNumber].quaterNoteTrigger.AddListener(action);
    }
}