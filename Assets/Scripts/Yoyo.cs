using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Yoyo : Weapon
{
    [SerializeField] private float rotateSpeed;
    public float angle, superModeTime, elapsedTime, percentageComplete, superModeMultiplier;
    private bool superMode;

    private void Start()
    {
        superMode = false;
    }
    private void FixedUpdate()
    {
        if (angle == 360)
        { 
         angle = 0;
        }
        this.transform.eulerAngles = new Vector3(0, 0, angle);

       
        if (superMode)
        {

            angle += rotateSpeed * superModeMultiplier;
            elapsedTime += Time.deltaTime;
            percentageComplete = elapsedTime / superModeTime;
            if (Mathf.FloorToInt(percentageComplete) == 1)
            {
                percentageComplete = 0;
                elapsedTime = 0;
                superMode = false;
            }
        }
        else
        {
            angle += rotateSpeed;
            superMode = false;
        }
        Debug.Log(superMode + "Supermode");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Enemy"))
        {
            DealDamage(other);
        }
       
    }

    public override void Attack()
    {
        superMode = true;
        StartCooldown();
    }
}
