using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerSkates : Utility
{
    public PlayerMovement playerMovement;
    public void UpgradeMovementSpeed()
    {
        playerMovement.IncreaseMovementSpeed(GetStatPercentIncrease());
    }
}
