using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Yoyo : Weapon
{
    [SerializeField] private float rotateSpeed;
    public float angle, superModeTime, elapsedTime, percentageComplete, superModeMultiplier;
    public Vector3 ballStartPosition, ballSuperModePosition, stringStartPosition, stringSuperModePosition;
    private bool superMode;
    private GameObject ball, yoyoString;

    private void Start()
    {
        superMode = false;
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
        this.transform.eulerAngles = new Vector3(0, 0, angle);

       
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

        if (other.gameObject.CompareTag("Enemy"))
        {
            DealDamage(other);
        }
       
    }

    public void SuperMode()
    {

        angle += rotateSpeed * superModeMultiplier;
        elapsedTime += Time.deltaTime;
        percentageComplete = elapsedTime / superModeTime;
        if (Mathf.FloorToInt(percentageComplete) == 1)
        {
            ResetSuperMode();
        }

        
    }

    private IEnumerator IncreaseRadiasSuperMode()
    {
        //ball.transform.position = Vector3.Lerp(ballStartPosition, ballSuperModePosition, );

        yield return null;

    }
    public void ResetSuperMode()
    {
        percentageComplete = 0;
        elapsedTime = 0;
        superMode = false;

    }

    public override void Attack()
    {
        superMode = true;
        StartCooldown();
    }
}
