using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private TriggerController triggerController;

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        triggerController = FindObjectOfType<TriggerController>();
        UnityAction action = new UnityAction(Bounce);
        triggerController.SetTrigger(1, action);
    }

    public void Bounce()
    {
        anim.SetTrigger("Beat");
    }
}
