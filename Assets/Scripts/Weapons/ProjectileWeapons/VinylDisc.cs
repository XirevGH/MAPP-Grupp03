using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VinylDisc: Weapon
{
    private GameObject circle, particle;
    private SoundManager soundManager;
    private TriggerController triggerController;
    [SerializeField] private float travelTime, BPM, noteValue, pitch, travelDistance;
    [SerializeField] private int triggerNumber;

    // Start is called before the first frame update
    void Start()
    {
        triggerController = GameObject.FindGameObjectWithTag("TriggerController").GetComponent<TriggerController>();
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
      


    }

    // Update is called once per frame
    void Update()
    {
        BPM = triggerController.GetCurrentTrackBPM();
        noteValue = triggerController.GetTrigger(triggerNumber).noteValue;
        pitch = soundManager.transform.GetChild(0).GetComponent<AudioSource>().pitch;
        travelTime = (((60f / (BPM / noteValue)) / pitch) / 2f);
    }


    public override void Attack()
    {

        //transform.position = Vector3.Lerp(transform.position, new Vector3(0));

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if ( other.gameObject.CompareTag("Enemy"))
        {
          
            DealDamage(other);
        }
       
    }
}
