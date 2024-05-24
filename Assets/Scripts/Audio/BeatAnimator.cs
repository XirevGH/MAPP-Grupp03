using UnityEngine;
using UnityEngine.Events;

public class BeatAnimator : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        UnityAction action = new UnityAction(TriggerAnimation);
        TriggerController.Instance.SetTrigger(1, action);
    }

    public void TriggerAnimation()
    {
        anim.SetTrigger("Beat");
    }
}
