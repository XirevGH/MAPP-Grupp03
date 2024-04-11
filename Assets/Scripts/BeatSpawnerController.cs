using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BeatSpawnerController : MonoBehaviour
{
    [SerializeField] private float BPM;
    [SerializeField] GameObject soundManager;
    [SerializeField] private Spawners[] spawners;

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

    // Start is called before the first frame update
    void Start()
    {
        audioSource = soundManager.transform.GetChild(0).GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        audioSource = soundManager.transform.GetChild(0).GetComponent<AudioSource>();
        foreach (Spawners spawners in spawners)
        {
            float sampledTime = (audioSource.timeSamples / (audioSource.clip.frequency * spawners.GetIntervalLength(BPM)));
            spawners.CheckForNewInterval(sampledTime);

        }
       
    }

   
}
