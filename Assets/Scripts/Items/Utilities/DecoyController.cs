using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DecoyController : Utility
{
    public float decoyHealth, throwDistance, throwDelayTime;
    [SerializeField] private GameObject decoy;
    [SerializeField] private int amountOfDecoy;
    [SerializeField] private int decoyIncreasePerUpgrade;
    [SerializeField] private int decoyHealthIncreasePerUpgrade;

    public int decoyAmountRank;
    public int decoyHealthRank;

    public DynamicJoystick dynamicJoystick;
    private Vector2 playerDirection, throwigDirection, playerPosition;

    protected override void Awake()
    {
        base.Awake();
        for (int i = 0; i < decoyAmountRank; i++)
        {
            IncreaseDecoyAmount();
        }
        for (int i = 0; i < decoyHealthRank; i++)
        {
            IncreaseDecoyHealth();
        }
        EndInitialUpgrades();
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Main") {
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            playerDirection = new Vector2(dynamicJoystick.Horizontal, dynamicJoystick.Vertical).normalized;
            
        }
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
            yield return new WaitForSeconds(throwDelayTime);
            
            GameObject newDecoy = Instantiate(decoy, transform.position, Quaternion.identity);
            newDecoy.GetComponent<Decoy>().endPosition = FindLandingSpot();
            newDecoy.GetComponent<Decoy>().SetHealth(decoyHealth);
        }  
        
     }

    public void DecoyAmountUpgradeRank(int rankAmount)
    {
        decoyAmountRank += rankAmount;
    }
    public void DecoyHealthUpgradeRank(int rankAmount)
    {
        decoyAmountRank += rankAmount;
    }

    public void IncreaseDecoyAmount()
    {
        amountOfDecoy += decoyIncreasePerUpgrade;
        if (InitialUpgradesComplete())
        {
            DecoyAmountUpgradeRank(1);
        }
    }

    public void IncreaseDecoyHealth()
    {
        decoyHealth += decoyIncreasePerUpgrade;
        if (InitialUpgradesComplete())
        {
            DecoyHealthUpgradeRank(1);
        }
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
