using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DiscoBallParabola : ProjectileWeapon
{
    private Vector2 startPosition, endPosition;
    private GameObject player;
    [SerializeField] private float throwDistance, throwHeight, throwTime, incremnt;
    private float percentageComplete, elapsedTime;
    public float gravity, height, angle, velocity, speed;
    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        startPosition = player.transform.position;
        incremnt = 0;

        transform.position = new Vector2(startPosition.x, startPosition.y);
    }

    void FixedUpdate()
    {
        transform.position = new Vector2(startPosition.x + (velocity * incremnt) * Mathf.Cos(angle), startPosition.y + (velocity * incremnt) * Mathf.Sin(angle) - Mathf.Pow((gravity * incremnt), 2));

        incremnt = incremnt + speed;


    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Enemy"))
        {
            DealDamage(other);
        }

    }


   
}
