using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_GetCurrency : MonoBehaviour
{
    public void GetCurrency()
    {
        MetaUpgradeSystem.Instance.AddCurrency(500);
    }
    
}
