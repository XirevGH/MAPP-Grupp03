using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTheBeat : MonoBehaviour
{
    
    public void DestroyFirstChild()
    {
        if (transform.childCount == 0)
        {
            return;
        }
        transform.GetChild(0).gameObject.GetComponent<Beat>().DestroyNote();
    }
}
