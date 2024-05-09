using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
public class XPDrop : MonoBehaviour
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
    
    
    public int XP;

    private GameObject player;

    public float speed = 20f;
    private bool move = false;

    
    public XPDrop Initialize(int initialXP, GameObject player)
    {   move = false;
        this.player = player;
        this.XP = initialXP;

        SetXpType();
        return this;  
    }

    /*public enum(){

        biggXpDrop, mediumXpDrop, smallXpDrop 
    }*/
    void FixedUpdate()
{
    float requiredDistance = 2.7f;
    float stopDistance = 0.3f;
    float distanceSquared = (player.transform.position - transform.position).sqrMagnitude;
    
    if (distanceSquared < requiredDistance * requiredDistance && !move){
        MoveToPlayer();
    }
    if (move && player != null)
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        if (distanceSquared < stopDistance * stopDistance){
            player.GetComponent<Player>().AddXP(XP);
            XPDropPool.Instance.ReturnXPDrop(this);
        }
    } 
}

    public void MoveToPlayer()
    {   
        move = true;
    }
    public void AddXP(int xpToAdd)
    {
        XP += xpToAdd;
        SetXpType();
    }

    public void ResetXP()
    {
        XP = 0;
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

    
    public void Deactivate()
    {   
        move = false;
        ResetXP();
        gameObject.SetActive(false);
    }

    
}