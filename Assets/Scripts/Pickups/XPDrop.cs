using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class XPDrop : MonoBehaviour
{
    
    private int XP;

    private GameObject player;


    static private GameController gameController;
    public float speed = 20f;
    public bool bossDrop;
    private bool move = false;

    
    public XPDrop Initialize(int initialXP, GameObject player)
    {
        this.player = player;
        this.XP = initialXP;
        return this;  
    }
    private void Awake()
    {   
        if(gameController == null){
            gameController = FindObjectOfType<GameController>();
        }
        gameController.AddXpObject(this);
        
    }
    void FixedUpdate()
    {   if(Vector3.Distance(player.transform.position, transform.position) > 33 && move != true){
            gameController.MergeAndRemove(this);
             
        }
        if(Vector3.Distance(player.transform.position, transform.position) < 2.7 && move != true){
            MoveToPlayer();
             
        }
        if (move && player != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            if(Vector3.Distance(player.transform.position, transform.position) < 0.3){
                player.GetComponent<Player>().AddXP(XP);
                gameController.RemoveXp(this);
                
                
            }
        } 
       
    }

    public void MoveToPlayer()
    {
        move = true;
    }
    
    public void SetXP(int xp)
    {
        XP = xp;
    }

    public void AddXP(int xpToAdd)
    {
        XP += xpToAdd;
    }

    public void ResetXP()
    {
        XP = 0;
    }
    public int GetXpValue()
    {
        return XP;
    }
    

    
    public void Deactivate()
    {   move = false;
        ResetXP();
        gameObject.SetActive(false);
    }

    
}