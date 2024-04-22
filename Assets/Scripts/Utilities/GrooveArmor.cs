using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrooveArmor : UtilityItem
{
    public void UpgradeHealth() 
    { 
        player.IncreaseHealth(GetStatPercentIncrease());
    }
}
