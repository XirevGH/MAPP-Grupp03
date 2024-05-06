using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

public class CameraMovement : MonoBehaviour
{
   
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
       
        UnityAction action = new UnityAction(Bounce);
        TriggerController.SetTrigger(11, action);
    }

    public void Bounce()
    {
        anim.SetTrigger("Beat");
    }
}
