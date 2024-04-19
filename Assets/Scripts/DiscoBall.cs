using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DiscoBall : ProjectileWeapon
{
    private Vector2 startPosition;
    private GameObject player;
    [SerializeField] private float throwDistance, throwHeight, increment;
    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        startPosition = player.transform.position;

        transform.position = new Vector2(startPosition.x - 3, startPosition.y);
        increment = 0;
    }
    protected override void Update()
    {
        
        transform.position = new Vector2(startPosition.x + 3 + increment, (-Mathf.Pow((throwDistance * startPosition.x + increment), 2) + startPosition.y + 3));
        increment += 0.01f;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Enemy"))
        {
            DealDamage(other);
        }

    }


   
}
