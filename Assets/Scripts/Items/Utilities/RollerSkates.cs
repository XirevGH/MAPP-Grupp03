using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RollerSkates : Utility
{
    public PlayerMovement playerMovement;
    public float percentageIncrease;
    public int movementSpeedRank;
    public int movementSpeedUpgradeCost;


    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().name == "Main") 
        {
            playerMovement.IncreaseMovementSpeed(Mathf.Pow(1 + (percentageIncrease / 100f), movementSpeedRank));
        }
    }

    public float GetMovementSpeedIncreasePercentage()
    {
        return percentageIncrease;
    }

    public void IncreaseMovementSpeed()
    {
        movementSpeedRank++;
        if (SceneManager.GetActiveScene().name == "Main")
        {
            playerMovement.IncreaseMovementSpeed(1 + (percentageIncrease / 100f));
        }
    }
    
    public float GetCurrentMovementSpeedIncrease()
    {
        return (float)Math.Round((Mathf.Pow(1 + (percentageIncrease / 100f), movementSpeedRank) - 1) * 100, 1);
    }

    protected override void CreateUpgradeOptions()
    {
        upgradeOptions.Add("IncreaseMovementSpeed");
    }

    public int GetIncreaseMovementSpeedCost()
    {
        return movementSpeedUpgradeCost;
    }

    public int GetMovementSpeedUpgradeRank()
    {
        return movementSpeedRank;
    }
}
