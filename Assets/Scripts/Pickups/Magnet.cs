using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Magnet : Pickup
{   

    private void Start()
    {
    }
    protected override void IndividualPickupAction(){
        XPDropPool.Instance.ActivateXpToPlayer();
        SoundManager.Instance.PlaySFX(pickupSound, 1);
    }

}