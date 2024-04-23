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

    private Vector3  startPosition, endPosition, playerPosition, centerPivot1;
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
      
        startPosition = transform.position;
        endPosition = new Vector3(
        Mathf.Cos(Mathf.Deg2Rad * aimingArrowRotation.eulerAngles.z) * travelDistance + startPosition.x,
        Mathf.Sin(Mathf.Deg2Rad * aimingArrowRotation.eulerAngles.z) * travelDistance + startPosition.y, startPosition.z);

        centerPivot1 = (startPosition + endPosition) * 0.5f;

        centerPivot1 -= new Vector3(0, -centerOffSet);

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
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        BPM = triggerController.GetCurrentTrackBPM();
        noteValue = triggerController.GetTrigger(triggerNumber).noteValue;
        pitch = source.pitch;
    
        travelTime = (((60f / (BPM / noteValue)) / pitch) );

        if (isAtPlayer)
        {
            elapsedTime += Time.deltaTime;
            percentageComplete = elapsedTime / travelTime;
            Debug.Log(centerOffSet);
            transform.position = QuadraticBezierCurve(startPosition, endPosition, centerPivot1,  curve.Evaluate(percentageComplete));
            //transform.position = Vector3.Slerp(startPosition - centerPivot1 , endPosition - centerPivot1, curve.Evaluate(percentageComplete)) + centerPivot1;
            angle -= rotateSpeed;
            vinylDisc.transform.eulerAngles = new Vector3(0, 0, angle);
            if (percentageComplete >= 1)
            {
                elapsedTime = 0;
                percentageComplete = 0;
                centerOffSet = -centerOffSet;
                centerPivot1 -= new Vector3(0, -centerOffSet);

                isAtPlayer = false;
                isHalfWay = true;
            }
        }
        else if (isHalfWay)
        {
            elapsedTime += Time.deltaTime;
            percentageComplete = elapsedTime / travelTime;


            transform.position = QuadraticBezierCurve(endPosition, playerPosition, centerPivot1, curve.Evaluate(percentageComplete));
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

    private Vector2 QuadraticBezierCurve(Vector2 startPosition, Vector2 endPosition, Vector2 controlPoint, float time)
    {
        Vector2 p0 = Vector2.Lerp(startPosition, controlPoint, time);
        Vector2 p1 = Vector2.Lerp(controlPoint, endPosition, time);

        return Vector2.Lerp(p0, p1, time);
    }
}
