using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DecoyController : Utility
{
    public float decoyHealth, throwDistance, throwDelayTime;
    [SerializeField] private GameObject decoy;
    [SerializeField] private int amountOfDecoy;
    [SerializeField] private int decoyIncreasePerUpgrade;
    [SerializeField] private int decoyHealthIncreasePerUpgrade;



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
        if (gameObject.activeSelf)
        {
            StartCoroutine("ThrowDelay");
        }
    }

     private IEnumerator ThrowDelay()
     {

        for (int i = 0; i < amountOfDecoy; i++)
        {
            GameObject newDecoy = Instantiate(decoy, transform.position, Quaternion.identity);
            newDecoy.GetComponent<Decoy>().endPosition = FindLandingSpot();
            newDecoy.GetComponent<Decoy>().SetHealth(decoyHealth); 
            yield return new WaitForSeconds(throwDelayTime);
        }  
        
     }

    public void IncreaseDecoyAmount()
    {
        amountOfDecoy += decoyIncreasePerUpgrade;
    }

    public void IncreaseDecoyHealth()
    {
        decoyHealth += decoyIncreasePerUpgrade;
    }

    public int GetDecoyHealthIncreasePerUpgrade()
    {
        return decoyHealthIncreasePerUpgrade;
    }
    public int GetDecoyIncreasePerUpgrade()
    {
        return decoyIncreasePerUpgrade;
    }

    protected override void CreateUpgradeOptions()
    {
        upgradeOptions.Add("IncreaseDecoyHealth");
        upgradeOptions.Add("IncreaseDecoyAmount");
    }
}
