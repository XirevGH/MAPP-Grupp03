using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DiscoBallParabola : ProjectileWeapon
{
    private Vector2 startPosition, endPosition;
    private GameObject player;
    [SerializeField] private float throwDistance, throwHeight, throwTime, increment;
    private float percentageComplete, elapsedTime;
    public float gravity, height, angle, velocity, speed;
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        startPosition = player.transform.position;
        increment = 0;

        transform.position = new Vector2(startPosition.x, startPosition.y);
    }

    void FixedUpdate()
    {
        transform.position = new Vector2(startPosition.x + (velocity * increment) * Mathf.Cos(angle), startPosition.y + (velocity * increment) * Mathf.Sin(angle) - Mathf.Pow((gravity * increment), 2));
        increment = increment + speed;
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
        // Do nothing
    }
}
