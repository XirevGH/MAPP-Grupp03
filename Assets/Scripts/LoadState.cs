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
            JsonUtility
            string json = r.ReadToEnd();
            List<Item> items = JsonSerializer.DeserializeObject<List<Item>>(json);
        }
    }
}
}
