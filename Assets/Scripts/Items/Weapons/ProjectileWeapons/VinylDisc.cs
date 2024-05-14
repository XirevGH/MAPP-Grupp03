using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class VinylDisc : Projectile
{
    [SerializeField] private float travelTime, BPM, noteValue, pitch, travelDistance, rotateSpeed, angle, controlPointOffSet;
    [SerializeField] private GameObject vinylDisc;
    [SerializeField] private AnimationCurve curve;

    public float elapsedTime, percentageComplete, lerpElapsedTime;
    public bool attack, isAtPlayer, isHalfWay;

    private Vector3  startPosition, endPosition, playerPosition, controlPoint;
    private Quaternion aimingArrowRotation;
    private AudioSource source;

    void Awake()
    {
        vinylDisc.GetComponent<SpriteRenderer>().color = Color.HSVToRGB(Random.Range(0f, 1f), 0.7f, 1);

        aimingArrowRotation = GameObject.FindGameObjectWithTag("AimingArrow").transform.rotation;
        startPosition = transform.position;

        //find endPosition by using Trigonometry (angle of the aiming arrow and travelDistance).
        endPosition = new Vector3(
        Mathf.Cos(Mathf.Deg2Rad * aimingArrowRotation.eulerAngles.z) * travelDistance + startPosition.x,
        Mathf.Sin(Mathf.Deg2Rad * aimingArrowRotation.eulerAngles.z) * travelDistance + startPosition.y, startPosition.z);

        controlPoint = BezierCurve.CalculateControlPoint(startPosition, endPosition, controlPointOffSet, true);

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
        BPM = SoundManager.Instance.GetCurrentBPM();
        noteValue = TriggerController.Instance.GetTrigger(VinylDiscController.triggerNumber).noteValue;
        source = SoundManager.Instance.transform.GetChild(0).GetComponent<AudioSource>();
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
                controlPoint = BezierCurve.CalculateControlPoint(endPosition, playerPosition, controlPointOffSet, false);

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

    public void Attack() 
    {
        isAtPlayer = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DealDamage(other);
        DestroyWhenMaxPenetration();
    }

}
