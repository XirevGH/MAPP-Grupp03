using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class XPDropPool : MonoBehaviour
{
    public static XPDropPool Instance { get; private set; }

    public GameObject xpDropPrefab; 
    public int xpDropMax;
    public int xpSaved;
    private Queue<XPDrop> activeXpDrops = new Queue<XPDrop>();
    private Queue<XPDrop> storedXpDrops = new Queue<XPDrop>();
    private int poolSize;  

    private void Awake()
    {
        Instance = this;
        poolSize = xpDropMax;
        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            XPDrop xpDrop = CreateNewXPDrop();
            xpDrop.gameObject.SetActive(false);
            storedXpDrops.Enqueue(xpDrop);
        }
    }

    private XPDrop CreateNewXPDrop()
    {
        GameObject obj = Instantiate(xpDropPrefab, transform);
        XPDrop newDrop = obj.GetComponent<XPDrop>();
        return newDrop;
    }

   public XPDrop GetXPDrop()
    {   
        if (storedXpDrops.Count <= 0){
            XPDrop oldXpDrop = activeXpDrops.Dequeue();
            MergeAndRemove(oldXpDrop);  
        }
        XPDrop xpDrop = storedXpDrops.Dequeue();
        activeXpDrops.Enqueue(xpDrop);
        return xpDrop;
    }



    public void ReturnXPDrop(XPDrop xpDrop)
    {
        xpDrop.Deactivate();
        storedXpDrops.Enqueue(xpDrop);
    }
    public void MergeAndRemove(XPDrop toMerge){
        xpSaved += toMerge.GetXpValue();
        RemoveXp(toMerge);
    }   
    public void RemoveXp(XPDrop toRemove){
        ReturnXPDrop(toRemove);
    } 

    public void ActivateXpToPlayer(){
        foreach (XPDrop xpDrop in activeXpDrops)
        {
            xpDrop.MoveToPlayer();
        }
        
    }


    public int AddXpSaved(int baseXp){
       
        if(xpSaved + baseXp >= 1000){
            xpSaved -= 1000-baseXp;
            //chnage Sprites
            return 1000;
        }
        return baseXp;
    }
}

