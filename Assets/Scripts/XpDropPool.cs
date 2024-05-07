using System.Collections.Generic;
using UnityEngine;

public class XPDropPool : MonoBehaviour
{
    public static XPDropPool Instance { get; private set; }

    public GameObject xpDropPrefab;  
    private Queue<XPDrop> xpDrops = new Queue<XPDrop>();
    private int poolSize = 50;  

    private void Awake()
    {
        Instance = this;
        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            XPDrop xpDrop = CreateNewXPDrop();
            xpDrop.gameObject.SetActive(false);
            xpDrops.Enqueue(xpDrop);
        }
    }

    private XPDrop CreateNewXPDrop()
    {
        GameObject obj = Instantiate(xpDropPrefab);
        XPDrop newDrop = obj.GetComponent<XPDrop>();
        return newDrop;
    }

   public XPDrop GetXPDrop()
    {
        if (xpDrops.Count > 0)
        {
            XPDrop xpDrop = xpDrops.Dequeue();
            return xpDrop;
        }
        else
        {
            return CreateNewXPDrop();  // Ensures there's always an XPDrop available
        }
    }

    public void ReturnXPDrop(XPDrop xpDrop)
    {
        xpDrop.Deactivate();
        xpDrops.Enqueue(xpDrop);
    }
}

