using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerSkates : UtilityItem
{
    public PlayerMovement playerMovement;
    public void UpgradeMovementSpeed()
    {
        playerMovement.IncreaseMovementSpeed(GetStatPercentIncrease());
    }
}
