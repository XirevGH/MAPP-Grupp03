using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DecoyController : Utility
{
    public float decoyHealth, throwDistance;
    [SerializeField] private GameObject decoy;

    public DynamicJoystick dynamicJoystick;
    private Vector2 playerDirection, throwigDirection, playerPosition;

    void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    void Update()
    {
        playerDirection = new Vector2 (dynamicJoystick.Horizontal, dynamicJoystick.Vertical).normalized;
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

    }

    private Vector2 FindLandingSpot()
    {
        throwigDirection = -playerDirection;

        return playerPosition + (throwDistance * throwigDirection);
    }

    public void Throw()
    {
        GameObject newDecoy = Instantiate(decoy, transform.position, Quaternion.identity);
        newDecoy.GetComponent<Decoy>().landingSpot = FindLandingSpot();
        newDecoy.GetComponent<Decoy>().SetHealth(1);

    }
}
