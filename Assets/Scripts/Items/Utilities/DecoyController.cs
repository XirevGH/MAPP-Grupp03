using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class DecoyController : Utility
{
    public float decoyHealth, throwDistance, throwDelayTime;
    [SerializeField] private GameObject decoy;
    [SerializeField] private int amountOfDecoy;
    [SerializeField] private int decoyIncreasePerUpgrade;
    [SerializeField] private int decoyHealthIncreasePerUpgrade;

    public int decoyAmountRank;
    public int decoyAmountUpgradeCost;
    
    public int decoyHealthRank;
    public int decoyHealthUpgradeCost;
    

    public DynamicJoystick dynamicJoystick;
    private Vector2 playerDirection, throwingDirection, playerPosition;

    private void Start()
    {
        UnityAction action = new UnityAction(Throw);
        TriggerController.Instance.SetTrigger(15, action);
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Main") {
            if (dynamicJoystick == null)
            {
                DynamicJoystick[] dynamicJoysticks = FindObjectsOfType<DynamicJoystick>();
                foreach (DynamicJoystick joystick in dynamicJoysticks)
                {
                    if (joystick.GetPosition() == "Right")
                    {
                        dynamicJoystick = joystick;
                    }
                }
            }
            playerPosition = player.transform.position;
            playerDirection = new Vector2(dynamicJoystick.Horizontal, dynamicJoystick.Vertical).normalized;
            
        }
    }

    private Vector2 FindLandingSpot()
    {
        throwingDirection = -playerDirection;

        return playerPosition + (throwDistance * throwingDirection);
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
        decoyAmountRank++;
        amountOfDecoy += decoyIncreasePerUpgrade;
    }

    public void IncreaseDecoyHealth()
    {
        decoyHealthRank++;
        decoyHealth += decoyIncreasePerUpgrade;
    }

    public int GetDecoyHealthAmountIncreasePerUpgrade()
    {
        return decoyHealthIncreasePerUpgrade;
    }
    public int GetDecoyAmountIncreasePerUpgrade()
    {
        return decoyIncreasePerUpgrade;
    }

    protected override void CreateUpgradeOptions()
    {
        upgradeOptions.Add("IncreaseDecoyHealth");
        upgradeOptions.Add("IncreaseDecoyAmount");
    }

    public int GetIncreaseDecoyHealthCost()
    {
        return decoyHealthUpgradeCost;
    }

    public int GetIncreaseDecoyAmountCost()
    {
        return decoyAmountUpgradeCost;
    }
}
