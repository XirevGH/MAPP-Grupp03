using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTheBeat : MonoBehaviour
{
    
    public void DestroyFirstChild()
    {
        transform.GetChild(0).gameObject.GetComponent<Beat>().DestroyNote();
    }
}
