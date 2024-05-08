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
            XPDropPool.Instance.ActivateXpToPlayer();
            gameController.RemoveMagnet(this);
            Destroy(gameObject);
        }
    }
}
