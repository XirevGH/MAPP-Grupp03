using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class VinylDisc : Projectile
{
    [SerializeField] private float travelTime, BPM, noteValue, pitch, travelDistance, rotateSpeed, angle, centerOffSet;
    [SerializeField] private int triggerNumber;
    [SerializeField] private GameObject vinylDisc;
    [SerializeField] private AnimationCurve curve;

    public float elapsedTime, percentageComplete, lerpElapsedTime;
    public bool attack, isAtPlayer, isHalfWay;

    private Transform playerPosition;
    private Vector3 startingPosition, startPosition, endPosition, centerPivot;
    private Quaternion aimingArrowRotation;
    private SoundManager soundManager;
    private AudioSource source;
    private TriggerController triggerController;

    void Awake()
    {
        triggerController = GameObject.FindGameObjectWithTag("TriggerController").GetComponent<TriggerController>();
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        source = soundManager.transform.GetChild(0).GetComponent<AudioSource>();
        aimingArrowRotation = GameObject.FindGameObjectWithTag("AimingArrow").transform.rotation;
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        startPosition = transform.position;
        endPosition = new Vector3(
        Mathf.Cos(Mathf.Deg2Rad * aimingArrowRotation.eulerAngles.z) * travelDistance + startPosition.x,
        Mathf.Sin(Mathf.Deg2Rad * aimingArrowRotation.eulerAngles.z) * travelDistance + startPosition.y, startPosition.z);

        centerPivot = (startPosition + endPosition) * 0.5f;

        centerPivot -= new Vector3(0, -centerOffSet);

        //if (aimingArrowRotation.eulerAngles.z <= 90f && aimingArrowRotation.eulerAngles.z >= -90f)
        //{

        //    rotateSpeed = +rotateSpeed;
        //}
        //else
        //{
        //    rotateSpeed = -rotateSpeed;
        //}

    }

    void FixedUpdate()
    {
        startingPosition = playerPosition.position;

        BPM = triggerController.GetCurrentTrackBPM();
        noteValue = triggerController.GetTrigger(triggerNumber).noteValue;
        pitch = source.pitch;
    
        travelTime = (((60f / (BPM / noteValue)) / pitch) );

        if (isAtPlayer)
        {
            elapsedTime += Time.deltaTime;
            percentageComplete = elapsedTime / travelTime;
            Debug.Log(centerOffSet);
            transform.position = Vector3.Slerp(startPosition - centerPivot , endPosition - centerPivot, curve.Evaluate(percentageComplete)) + centerPivot;
            angle -= rotateSpeed;
            vinylDisc.transform.eulerAngles = new Vector3(0, 0, angle);
            if (percentageComplete >= 1)
            {
                elapsedTime = 0;
                percentageComplete = 0;
                centerOffSet = -centerOffSet;
                centerPivot -= new Vector3(0, -centerOffSet);

                isAtPlayer = false;
                isHalfWay = true;
            }
        }
        else if (isHalfWay)
        {
            elapsedTime += Time.deltaTime;
            percentageComplete = elapsedTime / travelTime;
           
           
            transform.position = Vector3.Slerp(endPosition - centerPivot, startingPosition - centerPivot, curve.Evaluate(percentageComplete)) + centerPivot;
            Debug.Log(centerOffSet);
            angle += rotateSpeed;
            vinylDisc.transform.eulerAngles = new Vector3(0, 0, angle);
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
