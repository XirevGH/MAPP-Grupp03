using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMagnet : MonoBehaviour
{
    private GameObject player;
    private GameController gameController;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
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
