using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DecoyController : Utility
{
    public float decoyHealth, throwDistance, throwDelayTime;
    [SerializeField] private GameObject decoy;
    [SerializeField] protected int amountOfDecoy;

    public DynamicJoystick dynamicJoystick;
    private Vector2 playerDirection, throwigDirection, playerPosition;

    void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    void Update()
    {
        playerDirection = new Vector2(dynamicJoystick.Horizontal, dynamicJoystick.Vertical).normalized;
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

    }

    private Vector2 FindLandingSpot()
    {
        throwigDirection = -playerDirection;

        return playerPosition + (throwDistance * throwigDirection);
    }

    public void Throw()
    {
            StartCoroutine("ThrowDelay");
    }

     private IEnumerator ThrowDelay()
     {
        for (int i = 0; i < amountOfDecoy; i++)
        {
            yield return new WaitForSeconds(throwDelayTime);
            
            GameObject newDecoy = Instantiate(decoy, transform.position, Quaternion.identity);
            newDecoy.GetComponent<Decoy>().endPosition = FindLandingSpot();
            newDecoy.GetComponent<Decoy>().SetHealth(1);
        }  
        
     }

    public void IncreaseDecoyCount()
    {

    }

    public void IncreaseDecoyhealth()
    {
    
    }


    protected override void CreateUpgradeOptions()
    {
        upgradeOptions.Add("IncreaseDecoyhealth");
        upgradeOptions.Add("IncreaseDecoyCount");
    }
}
