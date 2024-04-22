using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VinylDisc : PenetratingProjectileWeapon
{
    private Vector3 playerPosition;
    private SoundManager soundManager;
    private TriggerController triggerController;
    [SerializeField] private float travelTime, BPM, noteValue, pitch, travelDistance;
    [SerializeField] private int triggerNumber;

    private float elapsedTime, percentageComplete, lerpElapsedTime;
    public bool attack;


    // Start is called before the first frame update
    void Start()
    {
        triggerController = GameObject.FindGameObjectWithTag("TriggerController").GetComponent<TriggerController>();
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
       

    }

    void Update()
    {
        BPM = triggerController.GetCurrentTrackBPM();
        noteValue = triggerController.GetTrigger(triggerNumber).noteValue;
        pitch = soundManager.transform.GetChild(0).GetComponent<AudioSource>().pitch;
        travelTime = (((60f / (BPM / noteValue)) / pitch) / 2f);

        if (attack)
        {
           
           
          
        }
       
    }


    public override void Attack()
    {

        elapsedTime += Time.timeScale;
        percentageComplete = elapsedTime / travelTime;

        transform.localPosition = Vector3.Lerp(new Vector3(0, 0, 0), new Vector3(7, 0, 0), percentageComplete);


        //if (transform.localPosition == new Vector3(7, 0, 0))
        //{
        //    elapsedTime += Time.deltaTime;
        //    transform.localPosition = Vector3.Lerp(transform.localPosition, playerPosition.position, percentageComplete);
        //}

    }

    private void Go()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(7, 0, 0), percentageComplete / 2);
    }

    private void ComeBack()
    {
    
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if ( other.gameObject.CompareTag("Enemy"))
        {
          
            DealDamage(other);
        }
       
    }
}
