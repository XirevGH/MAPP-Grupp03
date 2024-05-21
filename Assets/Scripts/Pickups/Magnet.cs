using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Magnet : Pickup
{   
    public AudioClip sound;

    private void Start()
    {
    }
    protected override void IndividualPickupAction(){
        XPDropPool.Instance.ActivateXpToPlayer();
        SoundManager.Instance.PlaySFX(sound, 1);
    }

}