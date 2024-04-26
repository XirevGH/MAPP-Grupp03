using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Yoyo : Projectile
{
    [SerializeField] private float rotateSpeed, colliderStartingOffset, colliderSuperModeOffset;
    [SerializeField] private int triggerNumber;
    [SerializeField] private GameObject ball, yoyoString;
    [SerializeField] private Vector3 ballStartPosition, ballSuperModePosition, stringStartPosition, stringSuperModePosition, stringStartScale, stringSuperModeScale;

    private List<string> upgradeOptions;
    public float angle, superModeTime, elapsedTime, percentageComplete, superModeMultiplier;

    private bool superMode;
    private float lerpTime, lerpElapsedTime;

    private float BPM, noteValue, pitch;

    private CircleCollider2D circleColl;

    private void Start()
    {
        upgradeOptions = new List<string> {"SuperMode", "PlusOneYoyo"};
        superMode = false;
        lerpTime = 0;
        circleColl = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        BPM = TriggerController.instance.GetCurrentTrackBPM();
        noteValue = TriggerController.instance.GetTrigger(triggerNumber).noteValue;
        pitch = SoundManager.instance.transform.GetChild(0).GetComponent<AudioSource>().pitch;
       

        rotateSpeed = BPM / 60;
        superModeTime = ((60f / (BPM / noteValue)) / pitch) / 2;
        lerpTime = superModeTime / 5;
    }

    private void FixedUpdate()
    {
        if (Time.timeScale == 0)
        {
            return;
        }

        if (angle == 360)
        { 
         angle = 0;
        }
        transform.eulerAngles = new Vector3(0, 0, angle);
       
        if (superMode)
        {
            SuperMode();
        }
        else
        {
            angle += rotateSpeed;
            superMode = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DealDamage(other);
    }


    public void ActivateSuperMode()
    {
        superMode = true;
    }

    public void SuperMode()
    {
        angle += rotateSpeed * superModeMultiplier;
        elapsedTime += Time.deltaTime;
        percentageComplete = elapsedTime / superModeTime;
        if (percentageComplete < 0.8f)
        {
            ExtendYoyoDistance();
        }
        else if (percentageComplete >= 0.8f)
        {
            ResetYoyoDistance();
        }
        if (Mathf.FloorToInt(percentageComplete) == 1)
        {
            ResetSuperMode();
        }
    }

    private void ExtendYoyoDistance()
    {
        float percentageComplete = elapsedTime / lerpTime;
        ball.transform.localPosition = Vector3.Lerp(ballStartPosition, ballSuperModePosition, percentageComplete);
        yoyoString.transform.localPosition = Vector3.Lerp(stringStartPosition, stringSuperModePosition, percentageComplete);
        yoyoString.transform.localScale = Vector3.Lerp(stringStartScale, stringSuperModeScale, percentageComplete);
        circleColl.offset = new Vector2(Mathf.Lerp(colliderStartingOffset, colliderSuperModeOffset, percentageComplete), 0f);
    }

    private void ResetYoyoDistance()
    {
        lerpElapsedTime += Time.deltaTime;
        float percentageComplete = lerpElapsedTime / lerpTime;
        ball.transform.localPosition = Vector3.Lerp(ballSuperModePosition, ballStartPosition, percentageComplete);
        yoyoString.transform.localPosition = Vector3.Lerp(stringSuperModePosition, stringStartPosition, percentageComplete);
        yoyoString.transform.localScale = Vector3.Lerp(stringSuperModeScale, stringStartScale, percentageComplete);
        circleColl.offset = new Vector2(Mathf.Lerp(colliderSuperModeOffset, colliderStartingOffset, percentageComplete), 0f);
    }

    public void ResetSuperMode()
    {
        percentageComplete = 0;
        elapsedTime = 0;
        lerpElapsedTime = 0;
        superMode = false;
        ball.transform.localPosition = ballStartPosition;
        yoyoString.transform.localPosition = stringStartPosition;
        yoyoString.transform.localScale = stringStartScale;
        if(circleColl != null)
        {
            circleColl.offset = new Vector2(colliderStartingOffset, 0f);
        }
    }
}
