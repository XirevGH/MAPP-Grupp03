using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class XPDrop : MonoBehaviour
{
    [SerializeField] private int XP;

    private GameObject gameControllerObject;
    private GameController gameController;

    private void Awake()
    {
        gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();
        gameController.AddXpObject(this);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<Player>().AddXP(XP);
            gameController.RemoveXpObject(this);
            Destroy(gameObject);
        }
    }
}