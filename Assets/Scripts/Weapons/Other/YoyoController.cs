using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class YoyoController : MonoBehaviour
{
    public GameObject yoyo;

    private int childCount;
    private float cooldown;

    void Start()
    {
        childCount = transform.childCount;
        SetYoyoPosition();
    }

    void Update()
    {
        if (childCount < transform.childCount)
        {
            SetYoyoPosition();
            childCount = transform.childCount;
        }
    }

    public void YoYoSperMode() 
    {
        int totalYoyo = transform.childCount;
        for (int i = 0; i < totalYoyo; i++)
        {
             transform.GetChild(i).GetComponent<Yoyo>().Attack();
          
        }
    }

    private void SetYoyoPosition()
    {
        int totalYoyo = transform.childCount;
        float angle = 360f / totalYoyo;
        float nextAngle = 0;

        for (int i = 0; i < totalYoyo; i++)
        {
            if(i == 0)
            {
                //cooldown = transform.GetChild(i).GetComponent<Yoyo>().cooldownDuration;
            } 
            else
            {
                //transform.GetChild(i).GetComponent<Yoyo>().cooldownDuration = cooldown;
            }
            transform.GetChild(i).GetComponent<Yoyo>().angle = nextAngle;
            transform.GetChild(i).GetComponent<Yoyo>().ResetSuperMode();
            //transform.GetChild(i).GetComponent<Yoyo>().StartCooldown();
            nextAngle += angle;
        }
    }

    public void Upgrade()
    {
        Instantiate(yoyo, transform.position, Quaternion.identity, transform);
    }
}
