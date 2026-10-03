using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VinylDisc : Projectile
{
    [SerializeField] private float travelTime, BPM, noteValue, pitch, travelDistance, rotateSpeed, angle, controlPointOffSet;
    [SerializeField] private GameObject vinylDisc;
    [SerializeField] private AnimationCurve curve;

    public float elapsedTime, percentageComplete, lerpElapsedTime;
    public bool attack, isAtPlayer, isHalfWay;

    private Vector3 startPosition, endPosition, playerPosition, controlPoint;
    private Quaternion aimingArrowRotation;
    private AudioSource source;
    public Color orange;
    public Color pink;

    void Awake()
    {
        SetTravelTime();

        if (Random.Range(0, 2) == 0)
        {
            vinylDisc.GetComponent<SpriteRenderer>().color = orange;
        }
        else
        {
            vinylDisc.GetComponent<SpriteRenderer>().color = pink;
        }

        aimingArrowRotation = GameObject.FindGameObjectWithTag("AimingArrow").transform.rotation;
        startPosition = transform.position;

        endPosition = new Vector3(
            Mathf.Cos(Mathf.Deg2Rad * aimingArrowRotation.eulerAngles.z) * travelDistance + startPosition.x,
            Mathf.Sin(Mathf.Deg2Rad * aimingArrowRotation.eulerAngles.z) * travelDistance + startPosition.y,
            startPosition.z);

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
        SetTravelTime();

        // Phase 1: Arc outward along quadratic bezier curve towards calculated end position
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

        // Phase 2: Arc back to player's dynamic position
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

    private void SetTravelTime()
    {
        var playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerPosition = playerObj.transform.position;
        }

        if (SoundManager.Instance != null && TriggerController.Instance != null && VinylDiscController.Instance != null)
        {
            BPM = SoundManager.Instance.GetCurrentBPM();
            noteValue = TriggerController.Instance.GetTrigger(VinylDiscController.Instance.BaseItemData.BeatNumber).noteValue;
            source = SoundManager.Instance.transform.GetChild(0).GetComponent<AudioSource>();
            if (source != null)
            {
                pitch = source.pitch;
            }

            if (BPM > 0 && noteValue > 0 && pitch > 0)
            {
                travelTime = (((60f / (BPM / noteValue)) / pitch) / 2f);
            }
        }
    }
}