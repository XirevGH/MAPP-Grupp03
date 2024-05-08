using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class XPDrop : MonoBehaviour
{
    
    public int XP;

    private GameObject player;

    public float speed = 20f;
    public bool bossDrop;
    private bool move = false;

    
    public XPDrop Initialize(int initialXP, GameObject player)
    {
        this.player = player;
        this.XP = XPDropPool.Instance.AddXpSaved(initialXP);
        return this;  
    }
    void FixedUpdate()
    {
    /*   
    float yDistanceSquared = (player.transform.position.y - transform.position.y) * (player.transform.position.y - transform.position.y);
        float xDistanceSquared = (player.transform.position.x - transform.position.x) * (player.transform.position.x - transform.position.x);

        if ((yDistanceSquared > 225 || xDistanceSquared > 625) && !move) {
            XPDropPool.Instance.MergeAndRemove(this);
             
        }*/
        if(Vector3.Distance(player.transform.position, transform.position) < 2.7 && move != true){
            MoveToPlayer();
             
        }
        if (move && player != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            if(Vector3.Distance(player.transform.position, transform.position) < 0.3){
                player.GetComponent<Player>().AddXP(XP);
                XPDropPool.Instance.RemoveXp(this);
                
                
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