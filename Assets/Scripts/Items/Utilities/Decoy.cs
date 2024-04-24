using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoy : MonoBehaviour
{
    public Vector2 landingSpot;
    private Vector2 startPosition, controlPoint;
    [SerializeField] float travelTime, controlPointOffSet, decoyHealth;
    private float elapsedTime, percentageComplete;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        controlPoint = CalculateControlPoint();
        
        //Debug.Log(controlPoint);
    }
    private void FixedUpdate()
    {
        elapsedTime += Time.deltaTime;
        percentageComplete = elapsedTime / travelTime;
        transform.position = BezierCurve.QuadraticBezierCurve(startPosition, landingSpot, controlPoint, percentageComplete);
       
    }

    Vector2 CalculateControlPoint()
    {
        Vector2 controlPoint = startPosition - (startPosition + landingSpot).normalized * 0.5f;

        controlPoint -= new Vector2(0, -controlPointOffSet);

        return controlPoint;
    }

    private void OnTriggerEnter2D(Collider2D other)
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
