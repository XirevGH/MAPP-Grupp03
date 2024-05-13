using System.Reflection;
using UnityEngine;
using UnityEngine.Events;


public class TriggerController : MonoBehaviour
{
    [SerializeField] public float inGameCurrentTrackBPM;
    [SerializeField] public Trigger[] triggers;
    [SerializeField] public bool isTriggering;

    private AudioSource inGameMusic;
        
    [System.Serializable]

    public class Trigger
    {
        [SerializeField] public float noteValue;
        [SerializeField] public UnityEvent quaterNoteTrigger;
        private int lastQuaterNote;
        public bool isTriggering;
        private AudioClip currentClip;

        public float GetIntervalLength(float BPM)
        {
            return 60f / (BPM/ noteValue);
        }

        public void CheckForNewQuaterNote(float interval)
        {
            if (currentClip != SoundManager.Instance.transform.GetChild(0).GetComponent<AudioSource>().clip)
            {
                currentClip = SoundManager.Instance.transform.GetChild(0).GetComponent<AudioSource>().clip;
                lastQuaterNote = 0;
            }

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
        Instance = this;
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

    public void SetTrigger(int triggerNumber, UnityAction action)
    {
        triggers[triggerNumber].quaterNoteTrigger.AddListener(action);
    }
}