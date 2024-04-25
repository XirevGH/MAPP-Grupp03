using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoy : MonoBehaviour
{
    public Vector2 endPosition;
    private Vector2 startPosition, controlPoint;
    [SerializeField] float travelTime, controlPointOffSet, decoyHealth;
    private float elapsedTime, percentageComplete;

    void Start()
    {
        startPosition = transform.position;
        controlPoint = BezierCurve.CalculateControlPoint(startPosition, endPosition, controlPointOffSet, true);

        //Debug.Log(controlPoint);
    }
    private void FixedUpdate()
    {
        elapsedTime += Time.deltaTime;
        percentageComplete = elapsedTime / travelTime;
        transform.position = BezierCurve.QuadraticBezierCurve(startPosition, endPosition, controlPoint, percentageComplete);
       
    }

  

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }    
      
    }

    private  void TakeDamage(int damageAmount)
    {
        decoyHealth -= damageAmount;
        if (decoyHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public void SetHealth(float health)
    {
        decoyHealth = health;
    }

}
