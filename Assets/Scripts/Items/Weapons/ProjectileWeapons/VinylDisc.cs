using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class VinylDisc : Projectile
{
    [SerializeField] private float travelTime, BPM, noteValue, pitch, travelDistance, rotateSpeed, angle, controlPointOffSet;
    [SerializeField] private int triggerNumber;
    [SerializeField] private GameObject vinylDisc;
    [SerializeField] private AnimationCurve curve;

    public float elapsedTime, percentageComplete, lerpElapsedTime;
    public bool attack, isAtPlayer, isHalfWay;

    private Vector3  startPosition, endPosition, playerPosition, controlPoint;
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

        controlPoint = CalculateControlPoint(startPosition, endPosition, true);

        if (aimingArrowRotation.eulerAngles.z <= 90f && aimingArrowRotation.eulerAngles.z >= -90f)
        {

            rotateSpeed = -rotateSpeed;
        }
        else
        {
            rotateSpeed = +rotateSpeed;
        }

    }

    void FixedUpdate()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        BPM = triggerController.GetCurrentTrackBPM();
        noteValue = triggerController.GetTrigger(triggerNumber).noteValue;
        pitch = source.pitch;
    
        travelTime = (((60f / (BPM / noteValue)) / pitch) /2f);

        if (isAtPlayer)
        {
            elapsedTime += Time.deltaTime;
            percentageComplete = elapsedTime / travelTime;
            transform.position = BezierCurve.QuadraticBezierCurve(startPosition, endPosition, controlPoint, curve.Evaluate(percentageComplete));
            
            angle -= rotateSpeed;
            vinylDisc.transform.eulerAngles = new Vector3(0, 0, angle);
            if (percentageComplete >= 1)
            {
                elapsedTime = 0;
                percentageComplete = 0;
                controlPoint = CalculateControlPoint(endPosition, playerPosition, false);

                isAtPlayer = false;
                isHalfWay = true;
            }
        }
        else if (isHalfWay)
        {
            elapsedTime += Time.deltaTime;
            percentageComplete = elapsedTime / travelTime;
            transform.position = BezierCurve.QuadraticBezierCurve(endPosition, playerPosition, controlPoint, curve.Evaluate(percentageComplete));
          
            angle -= rotateSpeed;
            vinylDisc.transform.eulerAngles = new Vector3(0, 0, angle);
            if (percentageComplete >= 1)
            {
                Destroy(gameObject);
            }
        }
    }


    private Vector2 CalculateControlPoint(Vector2 startPosition, Vector2 endPosition, bool aboveLine)
    {
        Vector2 controlPoint = startPosition - (startPosition + endPosition).normalized * 0.5f;
        float sideMultiplier = aboveLine ? -1f : 1f;
        controlPoint -= new Vector2(0, sideMultiplier * controlPointOffSet);

        return controlPoint;
    }



    public void Attack() 
    {
        isAtPlayer = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DealDamage(other);
    }

}
