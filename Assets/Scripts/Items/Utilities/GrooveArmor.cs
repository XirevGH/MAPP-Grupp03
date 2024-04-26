using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrooveArmor : Utility
{
    public void UpgradeHealth() 
    { 
        player.IncreaseMaxHealth(GetStatPercentIncrease());
    }
}
