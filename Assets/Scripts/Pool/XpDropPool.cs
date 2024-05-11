using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class XPDropPool : MonoBehaviour
{
    public static XPDropPool Instance { get; private set; }
     [SerializeField] public int xpSaved;
    
    public LinkedList<XPDrop> activeBiggXpDrops = new LinkedList<XPDrop>();
    public LinkedList<XPDrop> activeMediumXpDrops = new LinkedList<XPDrop>();
    public LinkedList<XPDrop> activeSmallXpDrops = new LinkedList<XPDrop>();
    
    private void Awake() {
        Instance = this;
    }

    

    

    
    public void Recycle(){
        for (int i = 0; i < Mathf.Min(5, activeSmallXpDrops.Count); i++)  
        {
            RecycleXp(GetAndRemoveOldestAndSmallestActiveXp()); 
        }
        CheckForXpUpdates();
    }
    public void RecycleXp(XPDrop toMerge){
        if(toMerge){
            xpSaved += toMerge.GetXpValue();
            toMerge.ReturnToPool();
            
            
        }
        
    } 
    public void RemoveFromActiveList(XPDrop xpDrop){
        if(activeSmallXpDrops.Contains(xpDrop)){
            activeSmallXpDrops.Remove(xpDrop);
        }else if(activeMediumXpDrops.Contains(xpDrop)){
            activeMediumXpDrops.Remove(xpDrop);
        }else if(activeBiggXpDrops.Contains(xpDrop)){
            activeBiggXpDrops.Remove(xpDrop);
        }else{
            Debug.LogWarning("Ter was no XPDrop to remove");
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
   private XPDrop GetAndRemoveOldestAndSmallestActiveXp()
{
    XPDrop result = null;
    if (activeSmallXpDrops.Count > 0) {
        result = activeSmallXpDrops.First.Value;
        activeSmallXpDrops.RemoveFirst();
    } else if (activeMediumXpDrops.Count > 0) {
        result = activeMediumXpDrops.First.Value;
        activeMediumXpDrops.RemoveFirst();
    }
    return result;
}
    
    public void SetAsBiggXp(XPDrop xpDrop){
        activeBiggXpDrops.AddLast(xpDrop);
        
    }
    public void SetAsMediumXp(XPDrop xpDrop){
        activeMediumXpDrops.AddLast(xpDrop);
        
    }

    public void SetAsSmallXp(XPDrop xpDrop){
        activeSmallXpDrops.AddLast(xpDrop);
        
    }
    public void CheckForXpUpdates()
    {
        XPDrop xpDrop = GetAndRemoveOldestAndSmallestActiveXp();
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

