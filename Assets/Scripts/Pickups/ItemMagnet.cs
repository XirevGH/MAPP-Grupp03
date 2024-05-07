using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMagnet : MonoBehaviour
{
    
    private GameController gameController;

    private void Awake()
    {   
    
        
        
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        gameController.AddMagnet(this);
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            HashSet<XPDrop> allXp = gameController.GetXPDropObjects();
            foreach(XPDrop xp in allXp){
                if(xp != null)
                {
                    xp.GetComponent<XPDrop>().MoveToPlayer();
                }
            }
            gameController.RemoveMagnet(this);
            Destroy(gameObject);
        }
    }
}
