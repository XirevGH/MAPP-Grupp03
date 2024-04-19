using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class YoyoController : MonoBehaviour
{
    public GameObject yoyo;
    private int childCount;

    // Start is called before the first frame update
    void Start()
    {
        childCount = transform.childCount;
        //PutChildInList();
        SetYoyoPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (childCount < transform.childCount)
        {
            //PutChildInList();

            SetYoyoPosition();
            childCount = transform.childCount;
        }
        //yoyoList.ToString();
    }

    private void SetYoyoPosition()
    {
        int totalYoyo = transform.childCount;
        float angle = 360f / totalYoyo;
        float nextAngle = 0;

        for (int i = 0; i < totalYoyo; i++)
        {
           transform.GetChild(i).GetComponent<Yoyo>().angle = nextAngle;
            transform.GetChild(i).GetComponent<Yoyo>().ResetSuperMode();
            transform.GetChild(i).GetComponent<Yoyo>().StartCooldown();

            nextAngle += angle;
        }
      
    }

    public void Upgrade()
    {

        Instantiate(yoyo, transform.position, Quaternion.identity, transform);
  

    }
}
