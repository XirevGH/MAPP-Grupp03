using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerSkates : UtilityItem
{
    public void UpgradeMovementSpeed()
    {
        player.IncreaseMovementSpeed(GetStatPercentIncrease());
    }
}
