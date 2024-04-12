
using System.IO;
using UnityEngine;

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
