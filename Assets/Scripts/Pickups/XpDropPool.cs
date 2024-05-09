using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class XPDropPool : MonoBehaviour
{
    public static XPDropPool Instance { get; private set; }

    [SerializeField] private GameObject xpDropPrefab;
    
    public int xpDropMax;
    public int xpSaved;

    public Queue<XPDrop> activeBiggXpDrops = new Queue<XPDrop>();
    public Queue<XPDrop> activeMediumXpDrops = new Queue<XPDrop>();
    public Queue<XPDrop> activeSmallXpDrops = new Queue<XPDrop>();
    public Queue<XPDrop> storedXpDrops = new Queue<XPDrop>();
    
    

    private void Awake()
    {
        Instance = this;
        InitializePool();
    }

    private void InitializePool()
{
    for (int i = 0; i < xpDropMax; i++)  
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
    if (storedXpDrops.Count == 0)
    {
        for (int i = 0; i < Mathf.Min(5, activeSmallXpDrops.Count); i++)  
        {
            RecycleXp(GetOldestAndSmallestActiveXp());
            
        }
        CheckForXpUpdates();
    }   
    if (storedXpDrops.Count > 0)
    {
        XPDrop xpDrop = storedXpDrops.Dequeue();
        return xpDrop;
    }
    return null;  
}



    public void ReturnXPDrop(XPDrop xpDrop)
    {
        xpDrop.Deactivate();
        storedXpDrops.Enqueue(xpDrop);
    }
    public void RecycleXp(XPDrop toMerge){
        if(toMerge){
            xpSaved += toMerge.GetXpValue();
            ReturnXPDrop(toMerge);
            
            
        }
        
    }   
     

    public void ActivateXpToPlayer(){
        foreach (XPDrop xpDrop in activeBiggXpDrops)
        {
            xpDrop.MoveToPlayer();
        }
        
        foreach (XPDrop xpDrop in activeMediumXpDrops)
        {
            xpDrop.MoveToPlayer();
        }
        foreach (XPDrop xpDrop in activeSmallXpDrops)
        {
            xpDrop.MoveToPlayer();
        }
        
    }
   private XPDrop GetOldestAndSmallestActiveXp()
    {
        if (activeSmallXpDrops.Count > 0) return activeSmallXpDrops.Dequeue();
        if (activeMediumXpDrops.Count > 0) return activeMediumXpDrops.Dequeue();
        return null;
    }
    
    public void SetAsBiggXp(XPDrop xpDrop){
        activeBiggXpDrops.Enqueue(xpDrop);
        
    }
    public void SetAsMediumXp(XPDrop xpDrop){
        activeMediumXpDrops.Enqueue(xpDrop);
        
    }

    public void SetAsSmallXp(XPDrop xpDrop){
        activeSmallXpDrops.Enqueue(xpDrop);
        
    }
    public void CheckForXpUpdates()
    {
        XPDrop xpDrop = GetOldestAndSmallestActiveXp();
        if (xpDrop != null)
        {
            int xpNeeded = xpDrop.NeededXpForNextLevel();
            if (xpNeeded <= xpSaved)
            {
                xpDrop.AddXP(xpNeeded);
                xpSaved -= xpNeeded;
            }
            else
            {
                xpDrop.SetXpType();
            }
        }
    }


    

    
}

