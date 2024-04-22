using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VinylDisc : Projectile
{
    private Vector3 playerPosition, startPosition, halfWayPosition;
    private Quaternion aimingArrowRotation;
    private SoundManager soundManager;
    private TriggerController triggerController;
    [SerializeField] private float travelTime, BPM, noteValue, pitch, travelDistance;
    [SerializeField] private int triggerNumber;

    public float elapsedTime, percentageComplete, lerpElapsedTime;
    public bool attack, isAtPlayer, isHalfWay;

    // Start is called before the first frame update
    void Awake()
    {
        triggerController = GameObject.FindGameObjectWithTag("TriggerController").GetComponent<TriggerController>();
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        aimingArrowRotation = GameObject.FindGameObjectWithTag("AimingArrow").transform.rotation;
        startPosition = transform.position;
        halfWayPosition = new Vector3(
        Mathf.Cos(Mathf.Deg2Rad * aimingArrowRotation.eulerAngles.z) * travelDistance + startPosition.x,
        Mathf.Sin(Mathf.Deg2Rad * aimingArrowRotation.eulerAngles.z) * travelDistance + startPosition.y, startPosition.z );
        attack = true;
    }

    void FixedUpdate()
    {
        aimingArrowRotation = GameObject.FindGameObjectWithTag("AimingArrow").transform.rotation;
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
    
        BPM = triggerController.GetCurrentTrackBPM();
        noteValue = triggerController.GetTrigger(triggerNumber).noteValue;
        pitch = soundManager.transform.GetChild(0).GetComponent<AudioSource>().pitch;
    
        travelTime = (((60f / (BPM / noteValue)) / pitch) / 4f);

        if (isAtPlayer)
        {
            elapsedTime += Time.deltaTime;
            percentageComplete = elapsedTime / travelTime;
            transform.position = Vector3.Lerp(startPosition, halfWayPosition, percentageComplete);
            if (percentageComplete >= 1)
            {
                isAtPlayer = false;
                isHalfWay = true;
                elapsedTime = 0;
                percentageComplete = 0;
            }
        }
        else if (isHalfWay)
        {
                
                elapsedTime += Time.deltaTime;
                percentageComplete = elapsedTime / travelTime;
                transform.position = Vector3.Lerp(halfWayPosition, playerPosition, percentageComplete);
                if (percentageComplete >= 1)
                {
                  Destroy(gameObject);
                }
            

        }

    }
    public void Attack() 
    {
        isAtPlayer = true;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
            DealDamage(other, damage);
        
       
    }
}
