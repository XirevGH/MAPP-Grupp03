using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMagnet : MonoBehaviour
{
    private GameObject gameControllerObject;
    private GameController gameController;
    private GameObject player;

    private void Awake(){
        player = GameObject.FindGameObjectWithTag("Player");
        gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            HashSet<XPDrop> allXp = gameController.GetXPDropObjects();
            foreach(XPDrop xp in allXp){
                if(xp != null)
                {
                    xp.GetComponent<XPDrop>().MoveToPlayer(player.transform);
                }
            }
            Destroy(gameObject);
        }
    }
}
