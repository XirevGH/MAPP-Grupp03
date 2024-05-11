using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
public class XPDrop : Pickup
{   [SerializeField] private SpriteRenderer spriteRenderer;
    
    [SerializeField] private Sprite biggXpDropSprite; 
    public Vector3 biggXpSize;
    public int biggMaxXpValue;

    [SerializeField] private Sprite mediumXpDropSprite; 
    public Vector3 mediumXpSize;
    public int mediumMaxXpValue;

    [SerializeField] private Sprite smallXpDropSprite;
    public Vector3 smallXpSize;
    public int smallMaxXpValue;
    
    [SerializeField] private int XP;


    
    public XPDrop Initialize(int initialXP)
    {   move = false;
        XP = initialXP;

        SetXpType();
        return this;  
    }

    
    

    public new void MoveToPlayer()
    {   
        move = true;
    }
    public void AddXP(int xpToAdd)
    {
        XP += xpToAdd;
        SetXpType();
    }
    public int GetXpValue()
    {
        return XP;
    }
    

    public void SetXpType()
    {
        if (XP <= smallMaxXpValue) {
            SetXpToSmallXp();
        } else if (XP <= mediumMaxXpValue) {
            SetXpToMediumXp();
        } else if (XP <= biggMaxXpValue) {
            SetXpToBiggXp();
        } else {
            Debug.LogWarning("XPDrop was too big.");
        }
    }
    
    public void SetXpToBiggXp(){
        XPDropPool.Instance.SetAsBiggXp(this);
        spriteRenderer.sprite = biggXpDropSprite;
        transform.localScale = biggXpSize;
    }
    public void SetXpToMediumXp(){
        XPDropPool.Instance.SetAsMediumXp(this);
        spriteRenderer.sprite = mediumXpDropSprite;
        transform.localScale = mediumXpSize;
    }

    public void SetXpToSmallXp(){
        XPDropPool.Instance.SetAsSmallXp(this); 
        spriteRenderer.sprite = smallXpDropSprite;
        transform.localScale = smallXpSize;
    }
    public int NeededXpForNextLevel()
    {
        //if (XP < smallMaxXpValue) return smallMaxXpValue - XP;
        if (XP < mediumMaxXpValue) return mediumMaxXpValue - XP;
        if (XP < biggMaxXpValue) return biggMaxXpValue - XP;
        return 0;  
    }

    
    
    protected override void ResetThis()
    {   XPDropPool.Instance.RemoveFromActiveList(this);
        XP = 0;
    }
    protected override void IndividualPickupAction(){
        player.GetComponent<Player>().AddXP(XP);

    }

    
}