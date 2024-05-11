using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup
{
    [SerializeField] private float percentToHeal;

    protected override void IndividualPickupAction(){
        player.RestoreHealth(percentToHeal);
    }
    
}