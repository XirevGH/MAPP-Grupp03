using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class YoyoController : MonoBehaviour
{
    public GameObject[] yoyoList;
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
        }
        //yoyoList.ToString();
    }

    private void SetYoyoPosition()
    {
        int totalYoyo = yoyoList.Length;
        float angle = 360f / totalYoyo;
        float nextAngle = 0;

        for (int i = 0; i < yoyoList.Length; i++)
        {
            yoyoList[i].GetComponent<Yoyo>().angle = nextAngle;
            nextAngle += angle;
        }
      
    }

    //private void PutChildInList()
    //{
        
    //    for (int i = 0; i < transform.childCount-1; i++)
    //    {
    //        yoyoList.Add(transform.GetChild(i).transform);
    //    }

    //}

    public void Upgrade()
    { 
    
    
    
    }
}
