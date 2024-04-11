using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static UnityEditor.Progress;

public class LoadState : MonoBehaviour
{
    public void LoadJson()
    {
        using (StreamReader r = new StreamReader("file.json"))
        {
            string json = r.ReadToEnd();
        }
    }
}
}
